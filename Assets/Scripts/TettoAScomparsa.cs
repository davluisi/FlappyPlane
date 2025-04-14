using UnityEngine;

public class Tetto : MonoBehaviour
{
    public GameObject tetto;

    // Update is called once per frame
    void Update()
    {
        if(Punti.valorePunti==4) tetto.SetActive(false);
        else tetto.SetActive(true);

    }
}
