using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FindGameManager : MonoBehaviour
{
    public static FindGameManager Instance { get; private set; }

    [Header("Goal")]
    public int totalToFind = 4;
    public string nextSceneName = "Scene2";

    [Header("UI")]
    public TMP_Text timerText;
    public TMP_Text foundText;

    float startTime;
    int foundCount = 0;
    bool finished = false;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        startTime = Time.time;
        UpdateUI();
    }

    void Update()
    {
        if (finished) return;

        float elapsed = Time.time - startTime;
        if (timerText != null)
            timerText.text = $"Temps: {elapsed:F1}s";
    }

    public void OnItemFound(FindableItem item)
    {
        if (finished) return;

        foundCount++;
        UpdateUI();

        if (foundCount >= totalToFind)
        {
            finished = true;
            float finalTime = Time.time - startTime;

            // Sauvegarde du temps pour la scène 2
            PlayerPrefs.SetFloat("FindTime", finalTime);
            PlayerPrefs.Save();

            // Charge scène 2
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void UpdateUI()
    {
        if (foundText != null)
            foundText.text = $"Objets trouvés: {foundCount}/{totalToFind}";
    }
}
