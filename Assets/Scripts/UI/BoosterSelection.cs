using System;
using System.Collections.Generic;
using Boosters;
using Player;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class BoosterSelection : MonoBehaviour
    {
        [SerializeField] private RectTransform[] _spawnPoints;
        [SerializeField] private BoosterSelectionScreen _boosterSelectionScreen;
        [SerializeField] private Button _pauseButton;
        [SerializeField] private Backpack _backpack;
        [SerializeField] private List<Booster> _boosters;

        public event Action BoosterSelected;

        private void Awake()
        {
            InitBoosters();
        }

        private void OnEnable()
        {
            _backpack.TokenBroughted += OnTokenBroughted;

            foreach (var booster in _boosters)
            {
                booster.BoosterSelected += OnBoosterSelected;
            }
        }

        private void OnDisable()
        {
            _backpack.TokenBroughted -= OnTokenBroughted;

            foreach (var booster in _boosters)
            {
                booster.BoosterSelected -= OnBoosterSelected;
            }
        }

        private void InitBoosters()
        {
            foreach (var booster in _boosters)
            {
                booster.gameObject.SetActive(false);
            }
        }

        private void ShowBoosters()
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                int boosterNumber = Random.Range(0, _boosters.Count);

                while (_boosters[boosterNumber].gameObject.activeSelf &&
                    !_boosters[boosterNumber].IsAvailable)
                {
                    boosterNumber = Random.Range(0, _boosters.Count);
                }

                _boosters[boosterNumber].gameObject.SetActive(true);
                _boosters[boosterNumber].gameObject.transform.position =
                    _spawnPoints[i].transform.position;
            }
        }

        private void OnTokenBroughted()
        {
            _boosterSelectionScreen.gameObject.SetActive(true);
            _pauseButton.gameObject.SetActive(false);
            ShowBoosters();
            Time.timeScale = 0;
        }

        private void OnBoosterSelected()
        {
            _boosterSelectionScreen.gameObject.SetActive(false);
            _pauseButton.gameObject.SetActive(true);
            Time.timeScale = 1;

            foreach (Booster booster in _boosters)
            {
                booster.gameObject.SetActive(false);
            }

            BoosterSelected?.Invoke();
        }
    }
}