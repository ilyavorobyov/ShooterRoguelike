using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    [SerializeField] private AudioSource _gameMusic;
    [SerializeField] private AudioSource _menuMusic;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private PlayerHealth _playerHealth;

    private void Start()
    {
        _menuMusic.PlayDelayed(0);
    }

    private void OnEnable()
    {
        _gameUI.GameBeguned += OnGameBeguned;
        _gameUI.MenuWented += OnMenuWented;
        _playerHealth.PlayerDied += OnPlayerDied;
    }

    private void OnDisable()
    {
        _gameUI.GameBeguned -= OnGameBeguned;
        _gameUI.MenuWented -= OnMenuWented;
        _playerHealth.PlayerDied -= OnPlayerDied;
    }

    private void OnGameBeguned()
    {
        _menuMusic.Stop();
        _gameMusic.PlayDelayed(0);
    }

    private void OnMenuWented()
    {
        _gameMusic.Stop();
        _menuMusic.PlayDelayed(0);
    }

    private void OnPlayerDied()
    {
        _gameMusic.Stop();
    }
}