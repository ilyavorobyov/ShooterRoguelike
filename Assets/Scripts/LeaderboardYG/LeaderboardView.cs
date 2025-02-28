using System.Collections.Generic;
using UnityEngine;

namespace LeaderboardYG
{
    public class LeaderboardView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private LeaderboardElement _leaderboardElementPrefab;

        private List<LeaderboardElement> _spawnedElements = new List<LeaderboardElement>();

        public void Construct(List<LeaderboardPlayer> leaderboardPlayers)
        {
            Clear();

            foreach (LeaderboardPlayer player in leaderboardPlayers)
            {
                LeaderboardElement leaderboardElementInstance = Instantiate(_leaderboardElementPrefab, _container.transform);
                leaderboardElementInstance.Initialize(player.Name, player.Rank, player.Score);
                _spawnedElements.Add(leaderboardElementInstance);
            }
        }

        private void Clear()
        {
            foreach (var element in _spawnedElements)
            {
                Destroy(element.gameObject);
            }

            _spawnedElements = new List<LeaderboardElement>();
        }
    }
}