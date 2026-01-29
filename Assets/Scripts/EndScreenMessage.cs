using UnityEngine;
using TMPro;

public class EndScreenMessage : MonoBehaviour
{
    public TMP_Text titleText;
    public TMP_Text messageText;

    void Start()
    {
        if (GameSession.Instance == null)
        {
            if (titleText) titleText.text = "Fin";
            if (messageText) messageText.text = "Aucune session trouvée.";
            return;
        }

        string name = GameSession.Instance.playerName;
        float t = GameSession.Instance.totalTime;

        string rank = GetRank(t);
        string msg = GetPersonalMessage(name, t);

        if (titleText) titleText.text = $"Bravo {name} !";
        if (messageText) messageText.text = $"Rang: {rank}\n\n{msg}";
    }

    string GetRank(float seconds)
    {
        if (seconds < 20f) return "S";
        if (seconds < 40f) return "A";
        if (seconds < 60f) return "B";
        if (seconds < 90f) return "C";
        return "D";
    }

    string GetPersonalMessage(string name, float seconds)
    {
        if (seconds < 20f)
            return "Réflexes incroyables. Tu as géré la situation comme un pro !";
        if (seconds < 40f)
            return "Très bon sang-froid. Décisions rapides et efficaces !";
        if (seconds < 60f)
            return "Bien joué ! Tu as gardé le contrôle malgré la pression.";
        if (seconds < 90f)
            return "Tu t’en es sorti, mais attention : chaque seconde compte !";
        return "Ouf… tu as survécu. La prochaine fois, sois plus rapide et plus prudent.";
    }
}
