using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerHealthText))]
public class PlayerHealth : Health
{
    private PlayerHealthText _playerHealthText;
    private float _startRegenerationPerSecond = 0;
    private float _increaseRegenerationPerSecond = 0.3f;
    private float _currentRegenerationPerSecond;
    private float _startVampirismValue = 0;
    private float _currentVampirismValue;
    private bool _isVampirismEnabled = false;
    private Coroutine _regenerate;

    public static Action GameOver;

    private void OnDestroy()
    {
        FullHealthBooster.CompletelyCured -= OnCompletelyCured;
        AddMaxHealthBooster.AddMaxHealth -= OnAddMaxHealth;
        AddRegenerationBooster.AddRegeneration -= OnAddRegeneration;
        AddVampirismBooster.AddedVampirism -= OnAddedVampirism;
    }

    private void Start()
    {
        _currentVampirismValue = _startVampirismValue;
        _currentRegenerationPerSecond = _startRegenerationPerSecond;
        _playerHealthText = GetComponent<PlayerHealthText>();
        FullHealthBooster.CompletelyCured += OnCompletelyCured;
        AddMaxHealthBooster.AddMaxHealth += OnAddMaxHealth;
        AddRegenerationBooster.AddRegeneration += OnAddRegeneration;
        AddVampirismBooster.AddedVampirism += OnAddedVampirism;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        _playerHealthText.SetHealthText();
    }

    public override void AddHealth(float addingHealth)
    {
        base.AddHealth(addingHealth);
        _playerHealthText.SetHealthText();
    }

    public override void OnReset()
    {
        base.OnReset();
        _playerHealthText.SetHealthText();
        _currentRegenerationPerSecond = _startRegenerationPerSecond;
        StopRegenerate();
        _isVampirismEnabled = false;
        _currentVampirismValue = _startVampirismValue;
    }

    public override void Die()
    {
        GameOver?.Invoke();
    }

    public void TryHealWithVampirism(float damage)
    {
        if (_isVampirismEnabled)
        {
            AddHealth(damage * _currentVampirismValue);
        }
    }

    private void OnCompletelyCured()
    {
        AddHealth(CurrentMaxHealth);
    }

    private void OnAddedVampirism()
    {
        float addedVampirismValue = 0.05f;
        _isVampirismEnabled = true;
        _currentVampirismValue += addedVampirismValue;
    }

    private void OnAddRegeneration()
    {
        _currentRegenerationPerSecond += _increaseRegenerationPerSecond;
        StopRegenerate();
        _regenerate = StartCoroutine(Regenerate());
    }

    private void StopRegenerate()
    {
        if (_regenerate != null)
        {
            StopCoroutine(_regenerate);
        }
    }

    private void OnAddMaxHealth()
    {
        IncreaseMaxHealth();
        _playerHealthText.SetHealthText();
    }

    private IEnumerator Regenerate()
    {
        int iterationTime = 1;
        var waitForSeconds = new WaitForSeconds(iterationTime);
        bool _isRegenerate = true;

        while (_isRegenerate)
        {
            AddHealth(_currentRegenerationPerSecond);
            yield return waitForSeconds;
        }
    }
}