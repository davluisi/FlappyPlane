using UnityEngine;
using UnityEngine.UI;

public class Flappy : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject GameOverMenu;
    public GameObject sfondo;
    public GameObject sfondoGlitch;
    public AudioClip[] audioClips;
    AudioSource audioSource;
    private bool s1HaSuonato = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameController.gameover)
        {
            audioSource.PlayOneShot(audioClips[0]);
            rb.linearVelocity = new Vector2(0f, 7f);
        }
        if (GameController.gameover && !s1HaSuonato)
        {
            audioSource.PlayOneShot(audioClips[1]);
            s1HaSuonato = true;
        }
        if (transform.position.y >= 6)
        {
            sfondo.SetActive(false);
            sfondoGlitch.SetActive(true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.gameover = true;
        GameOverMenu.SetActive(true);
    }
}