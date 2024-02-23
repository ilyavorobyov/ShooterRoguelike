using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerHealthText))]
public class PlayerHealth : Health
{
    [SerializeField] private AudioSource _healSound;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private AddMaxHealthBooster _addMaxHealthBooster;
    [SerializeField] private AddRegenerationBooster _addRegenerationBooster;
    [SerializeField] private AddVampirismBooster _addVampirismBooster;
    [SerializeField] private FullHealthBooster _fullHealthBooster;

    private PlayerHealthText _playerHealthText;
    private float _startRegenerationPerSecond = 0;
    private float _increaseRegenerationPerSecond = 0.16f;
    private float _currentRegenerationPerSecond;
    private float _startVampirismValue = 0;
    private float _currentVampirismValue;
    private bool _isVampirismEnabled = false;
    private Coroutine _regenerate;

    public event Action PlayerDied;

    private void OnDestroy()
    {
        _fullHealthBooster.CompletelyCured -= OnCompletelyCured;
        _addMaxHealthBooster.MaxHealthAdded -= OnAddMaxHealth;
        _addRegenerationBooster.RegenerationAdded -= OnRegenerationAdded;
        _addVampirismBooster.VampirismAdded -= OnVampirismAdded;
        _gameUI.GameBeguned -= OnReset;
    }

    private void Start()
    {
        _currentVampirismValue = _startVampirismValue;
        _currentRegenerationPerSecond = _startRegenerationPerSecond;
        _playerHealthText = GetComponent<PlayerHealthText>();
        _fullHealthBooster.CompletelyCured += OnCompletelyCured;
        _addMaxHealthBooster.MaxHealthAdded += OnAddMaxHealth;
        _addRegenerationBooster.RegenerationAdded += OnRegenerationAdded;
        _addVampirismBooster.VampirismAdded += OnVampirismAdded;
        _gameUI.GameBeguned += OnReset;
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        _playerHealthText.SetHealthText();
    }

    public override void Add(float addingHealth)
    {
        base.Add(addingHealth);
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
        PlayerDied?.Invoke();
    }

    public void TryHealWithVampirism(float damage)
    {
        if (_isVampirismEnabled)
        {
            Add(damage * _currentVampirismValue);
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
        bool isRegenerate = true;

        while (isRegenerate)
        {
            Add(_currentRegenerationPerSecond);
            yield return waitForSeconds;
        }
    }

    private void OnCompletelyCured()
    {
        Add(CurrentMaxHealth);
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
        IncreaseMax(addedHealth);
        _playerHealthText.SetHealthText();
        _healSound.PlayDelayed(0);
    }
}