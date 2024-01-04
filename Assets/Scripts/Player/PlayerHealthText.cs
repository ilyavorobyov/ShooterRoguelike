using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerHealthText : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthViewText;

    private PlayerHealth _health;

    private void Start()
    {
        _health = GetComponent<PlayerHealth>();
        SetHealthText();
    }

    public void SetHealthText()
    {
        _healthViewText.text = _health.CurrentHealth + "\\" + _health.MaxHealth;
    }
}