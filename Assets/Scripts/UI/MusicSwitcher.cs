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
        GameUI.GameBegun += (delegate { OnMusicSwitch(false); });
        GameUI.GoneMenu += (delegate { OnMusicSwitch(true); });
    }

    private void OnDisable()
    {
        GameUI.GameBegun -= (delegate { OnMusicSwitch(false); });
        GameUI.GoneMenu -= (delegate { OnMusicSwitch(true); });
    }

    private void OnMusicSwitch(bool onMenu)
    {
        if (onMenu)
        {
            _gameMusic.Stop();
            _menuMusic.PlayDelayed(0);
        }
        else
        {
            _menuMusic.Stop();
            _gameMusic.PlayDelayed(0);
        }
    }
}