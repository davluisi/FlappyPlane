using UnityEngine;

public class Pavimento : MonoBehaviour
{
    Vector2 posIniziale;
    private float velocita;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posIniziale = transform.position;
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
            if (transform.position.x >= -0.76f)
                transform.position = new Vector2(transform.position.x - (velocita * Time.deltaTime), transform.position.y);
            else
                transform.position = posIniziale;  
        }
    }
}
