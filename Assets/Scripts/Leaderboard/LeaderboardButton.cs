using Agava.YandexGames;
using UnityEngine;
using UnityEngine.EventSystems;

public class LeaderboardButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;
    [SerializeField] private LeaderboardScreen _leaderboardScreen;
    [SerializeField] private AuthorizationRequestScreen _authorizationRequestScreen;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnLeaderboardButtonClick();
    }

    public void OnLeaderboardButtonClick()
    {
        if (PlayerAccount.IsAuthorized)
        {
            _leaderboardScreen.gameObject.SetActive(true);
            _yandexLeaderboard.Fill();
        }
        else
        {
            _authorizationRequestScreen.gameObject.SetActive(true);
        }
    }
}