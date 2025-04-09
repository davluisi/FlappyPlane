using UnityEngine;

public class MuteManager : MonoBehaviour 
{
    private bool isMuted;
    public GameObject mute;
    public GameObject unmute;

    void Start()
    {
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
        AudioListener.pause = isMuted;
        mute.SetActive(!isMuted);
        unmute.SetActive(isMuted);
    }

    public void MutePressed()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        mute.SetActive(!isMuted);
        unmute.SetActive(isMuted);
        PlayerPrefs.SetInt("MUTED", isMuted ? 1 : 0);
    }
}
