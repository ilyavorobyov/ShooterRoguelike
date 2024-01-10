using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Booster : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool _isSelectedOnce;

    private bool _isChosenBefore = false;

    public static Action BoosterSelected;

    private void OnEnable()
    {
        GameUI.GameStateReset += OnReset;
    }

    private void OnDisable()
    {
        GameUI.GameStateReset -= OnReset;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Activate();
        _isChosenBefore = true;
        BoosterSelected?.Invoke();
    }

    public abstract void Activate();

    public bool IsCanBeShow()
    {
        if (_isSelectedOnce && _isChosenBefore)
            return false;
        else
            return true;
    }

    private void OnReset()
    {
        _isChosenBefore = false;
    }
}