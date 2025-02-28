using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.MechanicsTraining
{
    public class WalkingTraining : MonoBehaviour
    {
        [SerializeField] private WalkingTrainingText _mobileTrainingTexts;
        [SerializeField] private WalkingTrainingText _desktopTrainingTexts;
        [SerializeField] private Image _mobileTrainingImage;
        [SerializeField] private Image _desktopTrainingImage;

        private void OnEnable()
        {
            if (YandexGame.EnvironmentData.isMobile)
            {
                _mobileTrainingTexts.gameObject.SetActive(true);
                _mobileTrainingImage.gameObject.SetActive(true);
            }
            else
            {
                _desktopTrainingImage.gameObject.SetActive(true);
                _desktopTrainingTexts.gameObject.SetActive(true);
            }
        }
    }
}