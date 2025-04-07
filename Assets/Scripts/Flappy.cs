using UnityEngine;

public class Flappy : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject gameover;
    public GameObject restart;
    public GameObject backToMenu;
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
            s1HaSuonato=true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.gameover = true;
        gameover.SetActive(true);
        restart.SetActive(true);
        backToMenu.SetActive(true);
    }
}
