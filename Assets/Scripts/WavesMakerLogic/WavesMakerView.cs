using TMPro;
using UnityEngine;

namespace WavesMaker
{
    public class WavesMakerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _currentWaveText;
        [SerializeField] private TMP_Text _currentWaveNumberText;
        [SerializeField] private TMP_Text _waveDefeatedText;

        public void ShowWaveText(int currentWaveNumber)
        {
            _currentWaveText.gameObject.SetActive(true);
            _currentWaveNumberText.gameObject.SetActive(true);
            _currentWaveNumberText.text = currentWaveNumber.ToString();
        }

        public void ShowWaveDefeatedText()
        {
            _waveDefeatedText.gameObject.SetActive(true);
        }
    }
}