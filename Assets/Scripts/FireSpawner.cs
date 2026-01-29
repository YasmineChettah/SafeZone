using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class FireSpawner : MonoBehaviour
{
    [Header("Fire Spawning")]
    public GameObject firePrefab;
    public Transform[] spawnPoints;

    [Header("Events")]
    public UnityEvent onFireStarted;
    public UnityEvent onAllFiresOut;

    [Header("Countdown")]
    public float countdownSeconds = 15f;
    public TMP_Text timerText;

    [Header("Alarm")]
    public AudioSource alarmSource;

    private GameObject[] spawned;

    void Start()
    {
        StartCoroutine(BeginSequence());
    }

    IEnumerator BeginSequence()
    {
        // Compte à rebours
        float t = countdownSeconds;
        while (t > 0f)
        {
            if (timerText != null)
                timerText.text = $"Feu dans {Mathf.CeilToInt(t)}";

            yield return null;
            t -= Time.deltaTime;
        }

        // Go
        if (timerText != null)
            timerText.text = "FEU !";

        // Alarme
        if (alarmSource != null)
            alarmSource.Play();

        // Event: le feu démarre
        onFireStarted?.Invoke();

        // Spawn des feux
        spawned = new GameObject[spawnPoints.Length];
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawned[i] = Instantiate(firePrefab, spawnPoints[i].position, spawnPoints[i].rotation);
        }

        // Cache le texte après 1s
        yield return new WaitForSeconds(1f);
        if (timerText != null)
            timerText.gameObject.SetActive(false);

        // Surveille la fin (tous feux éteints)
        StartCoroutine(StopAlarmWhenAllFiresOut());
    }

    IEnumerator StopAlarmWhenAllFiresOut()
    {
        if (spawned == null) yield break;

        while (true)
        {
            bool anyActive = false;

            foreach (var go in spawned)
            {
                if (go != null && go.activeInHierarchy)
                {
                    anyActive = true;
                    break;
                }
            }

            if (!anyActive)
            {
                // Arrêt alarme (si assignée)
                if (alarmSource != null)
                    alarmSource.Stop();

                // Event: tous les feux sont éteints
                onAllFiresOut?.Invoke();

                yield break;
            }

            yield return new WaitForSeconds(0.25f);
        }
    }
}
