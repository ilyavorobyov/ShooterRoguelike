using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Booster : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _isSelectedOnce;

    private bool _isChosenBefore = false;

    public static event Action BoosterSelected;

    public bool IsAvailable => MayShown();

    private void OnEnable()
    {
        GameUI.GameReseted += OnReset;
    }

    private void OnDisable()
    {
        GameUI.GameReseted -= OnReset;
    }

    public abstract void Activate();

    private bool MayShown()
    {
        if (_isSelectedOnce && _isChosenBefore)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Activate();
        _isChosenBefore = true;
        BoosterSelected?.Invoke();
    }

    private void OnReset()
    {
        _isChosenBefore = false;
    }
}