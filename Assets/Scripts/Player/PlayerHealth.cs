using System;
using UnityEngine;

[RequireComponent(typeof(PlayerHealthText))]
public class PlayerHealth : Health
{
    private PlayerHealthText _playerHealthText;

    public static Action GameOver;

    private void Start()
    {
        _playerHealthText = GetComponent<PlayerHealthText>();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        _playerHealthText.SetHealthText();
    }

    public override void AddHealth(int addingHealth)
    {
        base.AddHealth(addingHealth);
        _playerHealthText.SetHealthText();
    }

    public override void OnReset()
    {
        base.OnReset();
        _playerHealthText.SetHealthText();
    }

    public override void Die()
    {
        GameOver?.Invoke();
    }
}