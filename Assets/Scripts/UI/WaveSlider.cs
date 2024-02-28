using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class WaveSlider : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nextWaveText;
        [SerializeField] private UnityEngine.UI.Slider _smoothWaveSlider;
        [SerializeField] private float _handleSpeed;

        private int _deadEnemies = 0;
        private Coroutine _changeValue;

        public void SetValues(int enemiesNumber, int currentWaveNumber)
        {
            _deadEnemies = 0;
            _smoothWaveSlider.maxValue = enemiesNumber;
            _smoothWaveSlider.value = _deadEnemies;
            int nextWaveNumber = ++currentWaveNumber;
            _nextWaveText.text = nextWaveNumber.ToString();
        }

        public void DetectEnemyDeath()
        {
            _deadEnemies++;
            ChangeSliderValue();
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
                float maxDelta = _handleSpeed * Time.deltaTime;
                _smoothWaveSlider.value = Mathf.MoveTowards(
                    _smoothWaveSlider.value,
                    _deadEnemies,
                    maxDelta);

                if (_smoothWaveSlider.value == _deadEnemies)
                {
                    isChangeSliderValue = false;
                    StopCoroutine(_changeValue);
                }

                yield return waitForFixedUpdate;
            }
        }
    }
}