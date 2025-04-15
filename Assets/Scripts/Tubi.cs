using UnityEngine;

public class Tubi : MonoBehaviour
{
    bool giaContato;
    AudioSource audioPunto;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        giaContato = false;
        audioPunto = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!giaContato && transform.position.x < -4.5)
        {
            giaContato=true;
            Punti.valorePunti += 1;
            audioPunto.Play();
        }
        if (!GameController.gameover)
            transform.position = new Vector2(transform.position.x - (GameController.velocita * Time.deltaTime), transform.position.y);
        if (transform.position.x < -6)
            Destroy(gameObject);
    }
}
