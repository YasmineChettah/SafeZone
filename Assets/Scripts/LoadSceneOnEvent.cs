using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnEvent : MonoBehaviour
{
    public string sceneName;
    public bool finishRunBeforeLoad = false;

    public void Load()
    {
        if (finishRunBeforeLoad && GameSession.Instance != null)
            GameSession.Instance.FinishRun();

        SceneManager.LoadScene(sceneName);
    }
}
