using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Threading.Tasks;

public class GameOver : MonoBehaviour
{
    public Text recordText;
    private FirebaseAuth auth;
    private DatabaseReference dbReference;

    async void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        dbReference = FirebaseDatabase.GetInstance("https://myflappy-2105-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
        await ControllaEVisualizzaRecordAsync();
    }

    private async Task ControllaEVisualizzaRecordAsync()
    {
        int punteggioCorrente = Punti.valorePunti;
        FirebaseUser user = auth.CurrentUser;

        if (user != null)
        {
            string userId = user.UserId;
            DatabaseReference userRef = dbReference.Child("users").Child(userId).Child("highscore");

            DataSnapshot snapshot = await userRef.GetValueAsync();

            if (snapshot.Exists && int.TryParse(snapshot.Value.ToString(), out int recordSalvato))
            {
                if (punteggioCorrente > recordSalvato)
                {
                    await userRef.SetValueAsync(punteggioCorrente);
                    recordText.text = "New Record !";
                }
                else
                {
                    recordText.text = "Record: " + recordSalvato;
                }
            }
            else
            {
                // Primo punteggio per questo utente
                await userRef.SetValueAsync(punteggioCorrente);
                recordText.text = "New Record !";
            }

            Debug.Log("Punteggio attuale: " + punteggioCorrente);
            Debug.Log("UID utente: " + user.UserId);
        }
        else
        {
            recordText.text = "Errore: utente non loggato.";
            Debug.LogError("FirebaseAuth.CurrentUser è null.");
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync(2);
    }
}
