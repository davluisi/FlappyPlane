using UnityEngine;
using UnityEngine.UI;

public class Punti : MonoBehaviour
{
    public static int valorePunti;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        valorePunti = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = valorePunti.ToString();
    }
}
