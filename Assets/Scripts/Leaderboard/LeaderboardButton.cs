using Agava.YandexGames;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeaderboardButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;
    [SerializeField] private LeaderboardScreen _leaderboardScreen;
    [SerializeField] private AuthorizationRequestScreen _authorizationRequestScreen;

    public void OnLeaderboardButtonClick()
    {
        if (PlayerAccount.IsAuthorized)
        {
            PlayerAccount.RequestPersonalProfileDataPermission();
            _leaderboardScreen.gameObject.SetActive(true);
            _yandexLeaderboard.Fill();
        }

        if (PlayerAccount.IsAuthorized == false)
        {
            _authorizationRequestScreen.gameObject.SetActive(true);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnLeaderboardButtonClick();
    }
}