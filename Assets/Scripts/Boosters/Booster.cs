using System;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Boosters
{
    public abstract class Booster : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private bool _isSelectedOnce;
        [SerializeField] private GameUI _gameUI;

        private bool _isChosenBefore = false;

        public event Action BoosterSelected;

        public bool IsAvailable => !_isSelectedOnce || !_isChosenBefore;

        public void OnPointerClick(PointerEventData eventData)
        {
            Activate();
            _isChosenBefore = true;
            BoosterSelected?.Invoke();
        }

        private void Awake()
        {
            _gameUI.GameReseted += OnReset;
        }

        private void OnDestroy()
        {
            _gameUI.GameReseted -= OnReset;
        }

        public abstract void Activate();

        private void OnReset()
        {
            _isChosenBefore = false;
        }
    }
}