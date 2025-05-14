using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System.Threading;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField loginEmailInput;
    public TMP_InputField loginPasswordInput;
    public TMP_InputField registerEmailInput;
    public TMP_InputField registerPasswordInput;
    public TMP_InputField resetEmailInput;

    public TextMeshProUGUI feedbackText;

    private FirebaseAuth auth;
    private FirebaseUser user;
    private bool loginSuccess = false;


    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            auth = FirebaseAuth.DefaultInstance;
        });
    }

    void Update()
    {
        if (loginSuccess)
        {
            loginSuccess = false; // Evita di farlo due volte
            ShowFeedback("Login effettuato! Benvenuto, " + user.Email);
            SceneManager.LoadSceneAsync(2);
        }
    }


    public void Login()
    {
        string email = loginEmailInput.text;
        string password = loginPasswordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                UnityEngine.Debug.Log("Login fallito.");
                loginSuccess = false;
            }
            else
            {
                user = task.Result.User;
                loginSuccess = true;
            }
        });
    }
    public void Register()
    {
        string email = registerEmailInput.text;
        string password = registerPasswordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                ShowFeedback("Registrazione fallita: " + task.Exception?.Message);
            }
            else
            {
                ShowFeedback("Registrazione avvenuta! Puoi ora accedere.");
            }
        });
    }

    public void SendPasswordReset()
    {
        string email = resetEmailInput.text;

        auth.SendPasswordResetEmailAsync(email).ContinueWith(task => {
            if (task.IsCanceled || task.IsFaulted)
            {
                ShowFeedback("Errore invio reset: " + task.Exception?.Message);
            }
            else
            {
                ShowFeedback("Email di recupero inviata.");
            }
        });
    }

    void ShowFeedback(string message)
    {
        Debug.Log(message);
        feedbackText.text = message;
    }
}
