using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public abstract class ParticleSystemEffect : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    protected void OnPlay(Vector3 targetPosition)
    {
        _particleSystem.Play();
        transform.position = targetPosition;
    }
}