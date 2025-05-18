using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;
    public TMP_InputField registerEmailInput;
    public TMP_InputField registerUsernameInput;
    public TMP_InputField registerPasswordInput;
    public TMP_InputField resetEmailInput;

    public TextMeshProUGUI feedbackText;

    public GameObject loginMenu;
    public GameObject registerMenu;

    public static FirebaseAuth auth;
    private static AuthManager instance;
    private FirebaseUser user;
    private bool loginSuccess = false;
    private string usernameLoaded;

    Color myGreen = new Color(0f, 0.6f, 0.2f); // Verde scuro
    Color myRed = new Color(0.8f, 0f, 0f);     // Rosso acceso

    void Start()
    {
        // Singleton pattern per evitare duplicati tra scene
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
            InitializeFirebase();
            // Precompila i campi se esistono dati salvati
            if (PlayerPrefs.HasKey("savedEmail"))
                loginEmailInput.text = PlayerPrefs.GetString("savedEmail");

            if (PlayerPrefs.HasKey("savedPassword"))
                loginPasswordInput.text = PlayerPrefs.GetString("savedPassword");
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
            ShowFeedback("Welcome " + usernameLoaded, myGreen);
            SceneManager.LoadSceneAsync(2);
        }
    }


    public async void Login()
    {
        string email = loginEmailInput.text;
        string password = loginPasswordInput.text;

        try
        {
            if (auth == null)
            {
                Debug.LogError("FirebaseAuth ? NULL!");
                ShowFeedback("Errore interno: Firebase non inizializzato.", myRed);
                return;
            }

            var result = await auth.SignInWithEmailAndPasswordAsync(email, password);
            user = result.User;

            // Salva email e password nei PlayerPrefs
            PlayerPrefs.SetString("savedEmail", email);
            PlayerPrefs.SetString("savedPassword", password);
            PlayerPrefs.Save();

            // Recupera lo username dal database
            DatabaseReference dbRef = FirebaseDatabase.GetInstance("https://myflappy-2105-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
            DataSnapshot snapshot = await dbRef.Child("users").Child(user.UserId).Child("username").GetValueAsync();

            usernameLoaded = snapshot.Exists ? snapshot.Value.ToString() : "User";

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
        string username = registerUsernameInput.text;

        if (string.IsNullOrEmpty(username))
        {
            ShowFeedback("Username richiesto", myRed);
            return;
        }

        try
        {
            var result = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            user = result.User;
            // Salva lo username nel Realtime Database
            DatabaseReference dbRef = FirebaseDatabase.GetInstance("https://myflappy-2105-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
            await dbRef.Child("users").Child(user.UserId).Child("username").SetValueAsync(username);
            ShowFeedback("Registered", myGreen);
            registerMenu.SetActive(false);
            loginMenu.SetActive(true);
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
