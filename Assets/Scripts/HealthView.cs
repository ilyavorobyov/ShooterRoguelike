using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _bar;
    [SerializeField] private Health _health;

    private float _maxHealth;
    private float _currentHealth;

    private void Awake()
    {
        _maxHealth = _health.MaxHealth;
        _currentHealth = _health.CurrentHealth;
    }

    public void SetInfo()
    {
        _currentHealth = _health.CurrentHealth;
        _bar.fillAmount = _currentHealth / _maxHealth;
    }
}