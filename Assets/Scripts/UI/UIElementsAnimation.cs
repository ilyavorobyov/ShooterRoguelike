using DG.Tweening;
using UnityEngine;

public class UIElementsAnimation : MonoBehaviour
{
    private float _effectDuration = 0.2f;

    public void Appear(GameObject uiElement)
    {
        uiElement.gameObject.transform.localScale = Vector3.zero;
        uiElement.gameObject.SetActive(true);
        uiElement.transform.DOScale(Vector3.one, _effectDuration).SetLoops(1, LoopType.Yoyo).
            SetUpdate(true);
    }

    public void Disappear(GameObject uiElement)
    {
        uiElement.transform.DOScale(Vector3.zero, _effectDuration).SetLoops(1, LoopType.Yoyo).
            SetUpdate(true).OnComplete(() => uiElement.gameObject.SetActive(false));
    }
}