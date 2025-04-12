using UnityEngine;

public class MuteManager : MonoBehaviour 
{
    private bool isMuted;
    public GameObject on;
    public GameObject off;

    void Start()
    {
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
        AudioListener.pause = isMuted;
        off.SetActive(!isMuted);
        on.SetActive(isMuted);
    }

    public void MutePressed()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        off.SetActive(!isMuted);
        on.SetActive(isMuted);
        PlayerPrefs.SetInt("MUTED", isMuted ? 1 : 0);
    }
}
