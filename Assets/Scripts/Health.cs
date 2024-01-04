using UnityEngine;

[RequireComponent (typeof(HealthView))]
public abstract class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private HealthView _healthView;
    private float _currentHealth;
    private float _minHealth = 0;

    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    private void Awake()
    {
        _healthView = GetComponent<HealthView>();
    }

    private void OnEnable()
    {
        GameUI.GameStateReset += OnReset;
        _currentHealth = _maxHealth;
        _healthView.SetInfo();
    }

    private void OnDisable()
    {
        GameUI.GameStateReset -= OnReset;
    }

    public virtual void TakeDamage(int damage)
    {
        if(damage > 0)
        {
            _currentHealth -= damage;
            _healthView.SetInfo();

            if (_currentHealth <= _minHealth)
                Die();
        }
    }

    public virtual void AddHealth(int addingHealth)
    {
        if (_currentHealth + addingHealth <= _maxHealth)
        {
            _currentHealth += addingHealth;
            _healthView.SetInfo();
        }
        else
        {
            _currentHealth = _maxHealth;
        }

        _healthView.SetInfo();
    }

    public virtual void OnReset()
    {
        _currentHealth = _maxHealth;
        _healthView.SetInfo();
    }

    public abstract void Die();
}