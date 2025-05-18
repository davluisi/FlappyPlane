using Firebase.Auth;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Logout()
    {
        // Effettua il logout da Firebase
        FirebaseAuth.DefaultInstance.SignOut();

        // Torna alla scena di login (scena 1)
        SceneManager.LoadScene(1);
    }
}
