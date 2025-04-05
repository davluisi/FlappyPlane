using UnityEngine;

public class Tubi : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    }
}
