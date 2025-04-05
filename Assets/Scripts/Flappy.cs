using UnityEngine;

public class Flappy : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject gameover;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !GameController.gameover)
        {
            rb.linearVelocity = new Vector2(0f, 7f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.gameover = true;
        gameover.SetActive(true);
    }
}
