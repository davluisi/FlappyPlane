using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void ClickRestart()
    {
        GameController.gameover = false;
        SceneManager.LoadScene(3);
    }
}
