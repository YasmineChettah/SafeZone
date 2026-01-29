using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    public string playerName = "Player";
    public float startTime;
    public float totalTime;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartRun(string name)
    {
        playerName = string.IsNullOrWhiteSpace(name) ? "Player" : name.Trim();
        startTime = Time.time;
        totalTime = 0f;
    }

    public void FinishRun()
    {
        totalTime = Time.time - startTime;
    }
}
