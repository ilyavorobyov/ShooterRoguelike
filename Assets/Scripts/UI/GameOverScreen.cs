using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private Button _doubleResultAdButton;
    [SerializeField] private UIElementsAnimation _uiElementsAnimation;

    private float _showButtonsDelay = 0.5f;
    private bool _isAboveZero;
    private Coroutine _showButtons;

    private void OnEnable()
    {
        if (_showButtons != null)
        {
            StopCoroutine(_showButtons);
        }

        _showButtons = StartCoroutine(ShowButtons());
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
        {
            button.gameObject.SetActive(false);
        }

        _doubleResultAdButton.gameObject.SetActive(false);
    }

    public void SetIsGreaterThanZero(bool aboveZero)
    {
        _isAboveZero = aboveZero;
    }

    private IEnumerator ShowButtons()
    {
        var waitForSecondsRealtime = new WaitForSecondsRealtime(_showButtonsDelay);
        yield return waitForSecondsRealtime;

        foreach (var button in _buttons)
        {
            _uiElementsAnimation.Appear(button.gameObject);
        }

        if (_isAboveZero)
        {
            _uiElementsAnimation.Appear(_doubleResultAdButton.gameObject);
        }

        StopCoroutine(_showButtons);
    }
}