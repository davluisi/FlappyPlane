using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialScene : MonoBehaviour
{
    public void tapToContinue()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
