using UnityEngine;

public class MuteManager : MonoBehaviour 
{
    private bool isMuted;
    public GameObject soundsOn;
    public GameObject soundsOff;

    void Start()
    {
        isMuted = PlayerPrefs.GetInt("MUTED") == 1;
        AudioListener.pause = isMuted;
        soundsOn.SetActive(!isMuted);
        soundsOff.SetActive(isMuted);
    }

    public void MutePressed()
    {
        isMuted = !isMuted;
        AudioListener.pause = isMuted;
        soundsOn.SetActive(!isMuted);
        soundsOff.SetActive(isMuted);
        PlayerPrefs.SetInt("MUTED", isMuted ? 1 : 0);
        
    }
}
