using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;

    private float _minHealth = 0;

    public float MaxHealth => _health;
    public float CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log(CurrentHealth);

        if (CurrentHealth <= _minHealth)
        {
            gameObject.SetActive(false);
        }
    }

    public void AddHealth(int addingHealth)
    {
        if (CurrentHealth + addingHealth <= MaxHealth)
        {
            CurrentHealth += addingHealth;
        }
    }
}