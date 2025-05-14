using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public void login()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
