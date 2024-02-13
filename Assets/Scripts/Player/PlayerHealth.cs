using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerHealthText))]
public class PlayerHealth : Health
{
    [SerializeField] private AudioSource _healSound;

    private PlayerHealthText _playerHealthText;
    private float _startRegenerationPerSecond = 0;
    private float _increaseRegenerationPerSecond = 0.16f;
    private float _currentRegenerationPerSecond;
    private float _startVampirismValue = 0;
    private float _currentVampirismValue;
    private bool _isVampirismEnabled = false;
    private Coroutine _regenerate;

    public static event Action GameOvered;

    private void OnDestroy()
    {
        FullHealthBooster.CompletelyCured -= OnCompletelyCured;
        AddMaxHealthBooster.MaxHealthAdded -= OnAddMaxHealth;
        AddRegenerationBooster.RegenerationAdded -= OnRegenerationAdded;
        AddVampirismBooster.VampirismAdded -= OnVampirismAdded;
    }

    private void Start()
    {
        _currentVampirismValue = _startVampirismValue;
        _currentRegenerationPerSecond = _startRegenerationPerSecond;
        _playerHealthText = GetComponent<PlayerHealthText>();
        FullHealthBooster.CompletelyCured += OnCompletelyCured;
        AddMaxHealthBooster.MaxHealthAdded += OnAddMaxHealth;
        AddRegenerationBooster.RegenerationAdded += OnRegenerationAdded;
        AddVampirismBooster.VampirismAdded += OnVampirismAdded;
    }

    public override void TakeDamage(float damage)
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
        GameOvered?.Invoke();
    }

    public void TryHealWithVampirism(float damage)
    {
        if (_isVampirismEnabled)
        {
            AddHealth(damage * _currentVampirismValue);
        }
    }

    private void StopRegenerate()
    {
        if (_regenerate != null)
        {
            StopCoroutine(_regenerate);
        }
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

    private void OnCompletelyCured()
    {
        AddHealth(CurrentMaxHealth);
        _healSound.PlayDelayed(0);
    }

    private void OnVampirismAdded(float addedVampirismValue)
    {
        _isVampirismEnabled = true;
        _currentVampirismValue += addedVampirismValue;
    }

    private void OnRegenerationAdded()
    {
        _currentRegenerationPerSecond += _increaseRegenerationPerSecond;
        StopRegenerate();
        _regenerate = StartCoroutine(Regenerate());
    }

    private void OnAddMaxHealth(int addedHealth)
    {
        IncreaseMaxHealth(addedHealth);
        _playerHealthText.SetHealthText();
        _healSound.PlayDelayed(0);
    }
}