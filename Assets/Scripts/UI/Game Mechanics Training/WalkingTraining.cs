using UnityEngine;
using Agava.WebUtility;
using UnityEngine.UI;

public class WalkingTraining : MonoBehaviour
{
    [SerializeField] private WalkingTrainingText _mobileTrainingTexts;
    [SerializeField] private WalkingTrainingText _desktopTrainingTexts;
    [SerializeField] private Image _mobileTrainingImage;
    [SerializeField] private Image _desktopTrainingImage;

    private bool _isMobile;

    private void Awake()
    {
        _isMobile = Device.IsMobile;
    }

    private void OnEnable()
    {
        if (_isMobile)
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