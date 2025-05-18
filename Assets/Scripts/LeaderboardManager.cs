using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class LeaderboardManager : MonoBehaviour
{
    public GameObject leaderboardPanel;
    public Text leaderboardText;

    private DatabaseReference dbReference;

    async void Start()
    {
        dbReference = FirebaseDatabase.GetInstance("https://myflappy-2105-default-rtdb.europe-west1.firebasedatabase.app/").RootReference;
    }

    public async void ShowLeaderboard()
    {
        var data = await dbReference.Child("users").OrderByChild("highscore").LimitToLast(10).GetValueAsync();

        if (data == null || !data.Exists)
        {
            leaderboardText.text = "Nessun dato disponibile.";
            leaderboardPanel.SetActive(true);
            return;
        }

        List<(string username, int score)> scores = new List<(string, int)>();

        foreach (DataSnapshot child in data.Children)
        {
            string username = child.Child("username").Value?.ToString();
            bool parsed = int.TryParse(child.Child("highscore").Value?.ToString(), out int score);

            if (!string.IsNullOrEmpty(username) && parsed)
                scores.Add((username, score));
        }

        // Ordina dal punteggio più alto a quello più basso
        scores = scores.OrderByDescending(x => x.score).ToList();

        string leaderboardString = "CLASSIFICA GLOBALE\n\n";
        for (int i = 0; i < scores.Count; i++)
        {
            leaderboardString += $"{i + 1}. {scores[i].username} - {scores[i].score}\n";
        }

        leaderboardText.text = leaderboardString;
        leaderboardPanel.SetActive(true);
    }

    public void CloseLeaderboard()
    {
        leaderboardPanel.SetActive(false);
    }
}
