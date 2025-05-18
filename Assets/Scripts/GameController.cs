using UnityEngine;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    float spawnTimer;
    float spawnRate;
    float spawnRate1 = 3f;
    float spawnRate2 = 2.2f;
    float spawnRate3 = 1.8f;
    float spawnRate4 = 1.5f;
    float spawnRate5 = 1.3f;
    public static float velocita;
    public GameObject tubo;
    public static bool gameover;
    public GameObject sfondoBase;
    public GameObject sfondoScuro;
    public GameObject sfondoGlitch;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {       
        gameover = false;
        spawnRate = spawnRate1;
        velocita = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameover)
        {
            if (Punti.valorePunti >= 24)
                spawnRate = spawnRate2;
            if (Punti.valorePunti >= 49)
                spawnRate = spawnRate3;
            if (Punti.valorePunti >= 74)
                spawnRate = spawnRate4;
            if (Punti.valorePunti >= 99)
                spawnRate = spawnRate5;
            if (Punti.valorePunti >= 100 && !sfondoGlitch.activeSelf)
            {
                sfondoBase.SetActive(false);
                sfondoScuro.SetActive(true);
            }
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnRate)
            {
                spawnTimer -= spawnRate;
                Vector2 spawnPos = new Vector2(2f, Random.Range(-1f, 2f));
                Instantiate(tubo, spawnPos, Quaternion.identity);
            }
        }
    }
}
