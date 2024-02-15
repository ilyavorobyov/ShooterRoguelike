using UnityEngine;

public class MusicSwitcher : MonoBehaviour
{
    [SerializeField] private AudioSource _gameMusic;
    [SerializeField] private AudioSource _menuMusic;

    private void Start()
    {
        _menuMusic.PlayDelayed(0);
    }

    private void OnEnable()
    {
        GameUI.GameBeguned += OnGameBeguned;
        GameUI.MenuWented += OnMenuWented;
        PlayerHealth.GameOvered += OnGameOver;
    }

    private void OnDisable()
    {
        GameUI.GameBeguned -= OnGameBeguned;
        GameUI.MenuWented -= OnMenuWented;
        PlayerHealth.GameOvered -= OnGameOver;
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

    private void OnGameOver()
    {
        _gameMusic.Stop();
    }
}