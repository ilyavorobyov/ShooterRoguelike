using UnityEngine;

public class ScoreSaver : MonoBehaviour
{
    [SerializeField] private YandexLeaderboard _yandexLeaderboard;

    public void SaveNewBestResult(string saveKey, int result)
    {
        PlayerPrefs.SetInt(saveKey, result);
        _yandexLeaderboard.SetPlayerScore(result);
    }
}