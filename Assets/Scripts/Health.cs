using UnityEngine;

public abstract class Health : MonoBehaviour
{
    [SerializeField] private int _health;

    private float _minHealth = 0;
    private float _currentHealth;


    private void Awake()
    {
        _currentHealth = _health;
    }

    private void OnEnable()
    {
        GameUI.GameStateReset += OnReset;
    }

    private void OnDisable()
    {
        GameUI.GameStateReset -= OnReset;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if (_currentHealth <= _minHealth)
            Die();
    }

    public void AddHealth(int addingHealth)
    {
        if (_currentHealth + addingHealth <= _health)
        {
            _currentHealth += addingHealth;
        }
    }

    private void OnReset()
    {
        _currentHealth = _health;
    }

    public abstract void Die();
}