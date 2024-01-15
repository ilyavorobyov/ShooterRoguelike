using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerHealthText : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthViewText;

    private PlayerHealth _health;
    private int _digitsNumber = 0;

    private void Start()
    {
        _health = GetComponent<PlayerHealth>();
        SetHealthText();
    }

    public void SetHealthText()
    {
        _healthViewText.text = Math.Round(_health.CurrentHealth, _digitsNumber) + "\\" + _health.CurrentMaxHealth;
    }
}