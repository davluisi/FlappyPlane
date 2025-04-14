using UnityEngine;

public class Tubi : MonoBehaviour
{
    bool giaContato = false;
    AudioSource audioSource;
    private float velocita; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        velocita = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.gameover)
        {
            if (Punti.valorePunti > 0 && (int)Punti.valorePunti % 10 == 0)
            {
                bool giaCambiato = false;
                if (velocita == 2f && !giaCambiato)
                {
                    velocita = 3f;
                    giaCambiato = true;
                }
                if (velocita == 3f && !giaCambiato)
                {
                    velocita = 2f;
                    giaCambiato = true;
                }
            }      
            transform.position = new Vector2(transform.position.x - (velocita * Time.deltaTime), transform.position.y);
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
