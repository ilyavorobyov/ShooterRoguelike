using Agava.YandexGames;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace LeaderboardYG
{
    public class LeaderboardButton : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private YandexLeaderboard _yandexLeaderboard;
        [SerializeField] private LeaderboardScreen _leaderboardScreen;
        [SerializeField] private AuthorizationRequestScreen _authorizationRequestScreen;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnLeaderboardButtonClick();
        }

        private void OnLeaderboardButtonClick()
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
}