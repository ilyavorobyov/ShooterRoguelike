using System.Collections;
using TMPro;
using UnityEngine;

public class WaveSlider : MonoBehaviour
{
    [SerializeField] private TMP_Text _nextWaveText;
    [SerializeField] private UnityEngine.UI.Slider _smoothWaveSlider;
    [SerializeField] private float _handleSpeed;

    private int _deadEnemies = 0;
    private Coroutine _changeValue;

    private void OnEnable()
    {
        EnemyHealth.Died += OnEnemyDied;
    }

    private void OnDisable()
    {
        EnemyHealth.Died -= OnEnemyDied;
    }

    public void SetValues(int enemiesNumber, int currentWaveNumber)
    {
        _deadEnemies = 0;
        _smoothWaveSlider.maxValue = enemiesNumber;
        _smoothWaveSlider.value = _deadEnemies;
        int nextWaveNumber = ++currentWaveNumber;
        _nextWaveText.text = nextWaveNumber.ToString();
    }

    private void ChangeSliderValue()
    {
        if (_changeValue != null)
            StopCoroutine(_changeValue);

        _changeValue = StartCoroutine(ChangeValue());
    }

    private IEnumerator ChangeValue()
    {
        var waitForFixedUpdate = new WaitForFixedUpdate();
        bool isChangeSliderValue = true;

        while (isChangeSliderValue)
        {
            _smoothWaveSlider.value = Mathf.MoveTowards
                (_smoothWaveSlider.value, _deadEnemies, _handleSpeed * Time.deltaTime);

            if (_smoothWaveSlider.value == _deadEnemies)
            {
                isChangeSliderValue = false;
                StopCoroutine(_changeValue);
            }

            yield return waitForFixedUpdate;
        }
    }

    private void OnEnemyDied()
    {
        _deadEnemies++;
        ChangeSliderValue();
    }
}