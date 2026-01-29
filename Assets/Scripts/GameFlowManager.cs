using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameFlowManager : MonoBehaviour
{
    [Header("Death rule")]
    public float deathDelaySeconds = 10f;
    public string winSceneName = "Scene_Win";
    public string deathSceneName = "Scene_Death";

    [Header("Red flash UI")]
    public Image redFlashImage;
    public float blinkSpeed = 6f;      // vitesse du clignotement
    public float blinkDuration = 2f;   // durée du clignotement avant de changer de scène

    bool hasExited = false;
    Coroutine deathRoutine;

    // Appelé quand le feu démarre
    public void OnFireStarted()
    {
        if (deathRoutine != null) StopCoroutine(deathRoutine);
        deathRoutine = StartCoroutine(DeathCountdown());
    }

    // Appelé quand le joueur sort par la porte
    public void PlayerExited()
    {
        if (hasExited) return;
        hasExited = true;

        if (deathRoutine != null) StopCoroutine(deathRoutine);
        StopRedFlash();

        SceneManager.LoadScene(winSceneName);
    }

    IEnumerator DeathCountdown()
    {
        hasExited = false;
        StopRedFlash();

        // Timer invisible de 10s
        float t = deathDelaySeconds;
        while (t > 0f)
        {
            if (hasExited) yield break;
            t -= Time.deltaTime;
            yield return null;
        }

        // Mort: clignotement rouge
        yield return StartCoroutine(BlinkRed(blinkDuration));

        // Charger la scène de mort
        SceneManager.LoadScene(deathSceneName);
    }

    IEnumerator BlinkRed(float duration)
    {
        if (redFlashImage == null) yield break;

        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            // alpha oscillant 0..0.8
            float a = (Mathf.Sin(Time.time * blinkSpeed) * 0.5f + 0.5f) * 0.8f;
            SetRedAlpha(a);
            yield return null;
        }

        SetRedAlpha(0f);
    }

    void StopRedFlash()
    {
        SetRedAlpha(0f);
    }

    void SetRedAlpha(float a)
    {
        if (redFlashImage == null) return;
        var c = redFlashImage.color;
        c.a = a;
        redFlashImage.color = c;
    }
}
