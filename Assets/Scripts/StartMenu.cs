using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public TMP_Text nameText;          
    public string firstSceneName = "Scene1";

    void Start()
    {
        SetRandomName();
    }

    public void SetRandomName()
    {
        int n = Random.Range(1000, 9999);
        string generated = "Player" + n;

        if (nameText != null)
            nameText.text = generated;
    }

    public void OnStartClicked()
    {
        if (GameSession.Instance == null)
        {
            Debug.LogError("GameSession manquant !");
            return;
        }
        Debug.Log("Start cliqué ! Scene = " + firstSceneName);

        string chosen = nameText != null ? nameText.text : "Player";
        GameSession.Instance.StartRun(chosen);

        SceneManager.LoadScene(firstSceneName);
    }
}
