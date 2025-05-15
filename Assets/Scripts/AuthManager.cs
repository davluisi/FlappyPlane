using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System.Collections;
using System;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;
    public TMP_InputField registerEmailInput;
    public TMP_InputField registerPasswordInput;
    public TMP_InputField resetEmailInput;

    public TextMeshProUGUI feedbackText;

    public static FirebaseAuth auth;
    private static AuthManager instance;
    private FirebaseUser user;
    private bool loginSuccess = false;

    Color myGreen = new Color(0f, 0.6f, 0.2f); // Verde scuro
    Color myRed = new Color(0.8f, 0f, 0f);     // Rosso acceso

    void Start()
    {
        // Singleton pattern per evitare duplicati tra scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeFirebase();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                Debug.Log("Firebase inizializzato correttamente.");
            }
            else
            {
                Debug.LogError("Impossibile inizializzare Firebase: " + dependencyStatus);
            }
        });
    }

    void Update()
    {
        if (loginSuccess)
        {
            loginSuccess = false; // Evita di farlo due volte
            ShowFeedback("Welcome", myGreen);
            SceneManager.LoadSceneAsync(2);
        }
    }

    public async void Login()
    {
        Debug.Log("Bottone Login premuto");
        string email = loginEmailInput.text;
        string password = loginPasswordInput.text;

        try
        {
            if (auth == null)
            {
                Debug.LogError("FirebaseAuth è NULL!");
                ShowFeedback("Errore interno: Firebase non inizializzato.", myRed);
                return;
            }

            var result = await auth.SignInWithEmailAndPasswordAsync(email, password);
            user = result.User;
            loginSuccess = true;
        }
        catch (Exception e)
        {
            ShowFeedback("Login failed", myRed);
        }
    }


    public async void Register()
    {
        string email = registerEmailInput.text;
        string password = registerPasswordInput.text;

        try
        {
            var result = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            user = result.User;
            ShowFeedback("Registered", myGreen);
        }
        catch (Exception e)
        {
            ShowFeedback("Registration failed", myRed);
        }
    }


    public async void SendPasswordReset()
    {
        string email = resetEmailInput.text;

        if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
        {
            ShowFeedback("Invalid email", myRed);
            return;
        }

        try
        {
            await auth.SendPasswordResetEmailAsync(email);
            ShowFeedback("Email sent", Color.black);
        }
        catch (FirebaseException firebaseEx)
        {
            // Puoi analizzare l'eccezione per vedere il messaggio di errore specifico
            ShowFeedback("Errore", myRed);
        }
        catch (Exception e)
        {
            // Catch altre eccezioni generiche
            ShowFeedback("Errore", myRed);
        }
    }

    // Metodo per validare la sintassi dell'email
    private bool IsValidEmail(string email)
    {
        try
        {
            var mailAddress = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }



    private IEnumerator ClearFeedbackAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        feedbackText.text = "";
    }

    private void ShowFeedback(string message, Color color)
    {
        feedbackText.color = color;
        feedbackText.text = message;
        StartCoroutine(ClearFeedbackAfterDelay(2f)); // sparisce dopo 2 secondi
    }

}

