using Agava.YandexGames;
using LeaderboardYG;
using UnityEngine;

namespace Score
{
    public class ScoreSaver : MonoBehaviour
    {
        [SerializeField] private YandexLeaderboard _yandexLeaderboard;

        public void SaveNewBestResult(string saveKey, int result)
        {
            PlayerPrefs.SetInt(saveKey, result);

            if (PlayerAccount.IsAuthorized)
            {
                _yandexLeaderboard.SetPlayerScore(result);
            }
        }
    }
}