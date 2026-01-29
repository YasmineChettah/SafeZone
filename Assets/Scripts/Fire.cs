using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Extinction")]
    public float fireHealth = 5f;          // plus c'est haut, plus c'est long à éteindre
    public float extinguishMultiplier = 1f;

    [Header("Refs")]
    public ParticleSystem fireParticles;
    public Light fireLight;

    float startEmission;
    float startLight;

    void Awake()
    {
        if (!fireParticles) fireParticles = GetComponentInChildren<ParticleSystem>();
        if (!fireLight) fireLight = GetComponentInChildren<Light>();

        if (fireParticles)
        {
            var em = fireParticles.emission;
            startEmission = em.rateOverTime.constant;
        }
        if (fireLight) startLight = fireLight.intensity;
    }

    public void Extinguish(float amount)
    {
        fireHealth -= amount * extinguishMultiplier;
        float t = Mathf.Clamp01(fireHealth / 5f); 

       
        if (fireParticles)
        {
            var em = fireParticles.emission;
            em.rateOverTime = startEmission * t;
            if (t <= 0.02f && fireParticles.isPlaying) fireParticles.Stop();
        }

        if (fireLight) fireLight.intensity = startLight * t;

        if (fireHealth <= 0f)
        {
            gameObject.SetActive(false); 
        }

    }
}

