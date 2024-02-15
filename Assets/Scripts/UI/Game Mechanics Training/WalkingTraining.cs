using Agava.WebUtility;
using UnityEngine;
using UnityEngine.UI;

public class WalkingTraining : MonoBehaviour
{
    [SerializeField] private WalkingTrainingText _mobileTrainingTexts;
    [SerializeField] private WalkingTrainingText _desktopTrainingTexts;
    [SerializeField] private Image _mobileTrainingImage;
    [SerializeField] private Image _desktopTrainingImage;

    private void OnEnable()
    {
        if (Device.IsMobile)
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