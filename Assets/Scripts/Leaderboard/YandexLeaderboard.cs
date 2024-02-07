using Agava.YandexGames;
using System.Collections.Generic;
using UnityEngine;

public class YandexLeaderboard : MonoBehaviour
{
    [SerializeField] private LeaderboardView _leaderboardView;

    private const string LeaderboardName = "Leaderboard";
    private const string English = "en";
    private const string Russian = "ru";
    private const string Turkish = "tr";

    private readonly List<LeaderboardPlayer> _leaderboardPlayers = new List<LeaderboardPlayer>();

    private string _anonymousName = "";

    public void SetPlayerScore(int score)
    {
        if (PlayerAccount.IsAuthorized == false)
            return;

        Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
        {
            if (result.score < score)
                Leaderboard.SetScore(LeaderboardName, score);
        });
    }

    public void Fill()
    {
        _leaderboardPlayers.Clear();
        SetAnonymousName();

        if (PlayerAccount.IsAuthorized == false)
            return;

        Leaderboard.GetEntries(LeaderboardName, (result) =>
        {
            foreach (var entry in result.entries)
            {
                int rank = entry.rank;
                int score = entry.score;
                string name = entry.player.publicName;

                if (string.IsNullOrEmpty(name))
                    name = _anonymousName;

                _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
            }

            _leaderboardView.ConstructLeaderboard(_leaderboardPlayers);
        });
    }

    private void SetAnonymousName()
    {
        string inRussian = "Имя скрыто";
        string inEnglish = "Name hidden";
        string inTurkish = "Ad gizlendi";
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        switch (languageCode)
        {
            case English:
                _anonymousName = inEnglish;
                break;
            case Russian:
                _anonymousName = inRussian;
                break;
            case Turkish:
                _anonymousName = inTurkish;
                break;
        }
    }
}