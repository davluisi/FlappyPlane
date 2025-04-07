using UnityEngine;

public class Tubi : MonoBehaviour
{
    bool giaContato = false;
    AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.gameover)
        {
            transform.position = new Vector2(transform.position.x - (2f * Time.deltaTime), transform.position.y);

        }
        if (transform.position.x < -6)
            Destroy(gameObject);

        if(!giaContato && transform.position.x < -4.8)
        {
            audioSource.Play();
            giaContato=true;
            Punti.valorePunti += 1;
        }
    }
}
