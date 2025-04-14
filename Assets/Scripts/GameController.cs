using UnityEngine;

public class GameController : MonoBehaviour
{
    float spawnTimer;
    float spawnRate;
    public GameObject tubo;
    public static bool gameover;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameover = false;
        spawnRate = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnTimer -= spawnRate;
                Vector2 spawnPos = new Vector2(2f, Random.Range(-1f, 2f));
                Instantiate(tubo, spawnPos, Quaternion.identity);
                if (Punti.valorePunti > 0 && (int)Punti.valorePunti % 10 == 0)
                {
                    bool giaCambiato = false;
                    if (spawnRate == 2f && !giaCambiato)
                    {
                        spawnRate = 3f;
                        giaCambiato = true;
                    }
                    if (spawnRate == 3f && !giaCambiato)
                    {
                        spawnRate = 2f;
                        giaCambiato = true;
                    }
                }
            }
        }
        
    }
}
