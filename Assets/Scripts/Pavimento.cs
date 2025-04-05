using UnityEngine;

public class Pavimento : MonoBehaviour
{
    Vector2 posIniziale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posIniziale = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.gameover)
        {
            if (transform.position.x >= -0.76f)
                transform.position = new Vector2(transform.position.x - (2f * Time.deltaTime), transform.position.y);
            else
                transform.position = posIniziale;
        }
       
    }
}
