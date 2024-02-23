using UnityEngine;

[RequireComponent(typeof(ScoreView))]
[RequireComponent(typeof(ScoreSaver))]
public class ScoreCounter : MonoBehaviour
{
    private const string RecordKeyName = "MaxScore";

    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private PlayerHealth _playerHealth;
    [SerializeField] private RewardedVideoAd _rewardedVideoAd;

    private ScoreView _scoreView;
    private ScoreSaver _saver;
    private int _bestScore;
    private int _currentScore = 0;
    private int _dischargeScoreValue = 0;
    private int _scoreMultiplier = 2;

    private void OnEnable()
    {
        _rewardedVideoAd.RewardAdDoubleResultViewed += OnDoubleResult;
        _playerHealth.PlayerDied += OnPlayerDied;
        _gameUI.GameReseted += OnGameReseted;
    }

    private void OnDisable()
    {
        _rewardedVideoAd.RewardAdDoubleResultViewed -= OnDoubleResult;
        _playerHealth.PlayerDied -= OnPlayerDied;
        _gameUI.GameReseted -= OnGameReseted;
    }

    private void Awake()
    {
        _scoreView = GetComponent<ScoreView>();
        _saver = GetComponent<ScoreSaver>();
        OnGameReseted();
    }

    public void DetectEnemyDeath()
    {
        _currentScore++;
        _scoreView.SetCurrentScoreText(_currentScore);
    }

    private void OnGameReseted()
    {
        if (PlayerPrefs.HasKey(RecordKeyName))
        {
            _bestScore = PlayerPrefs.GetInt(RecordKeyName);

            if (_bestScore > 0)
            {
                _scoreView.ShowPlate(_bestScore);
            }
        }

        _currentScore = _dischargeScoreValue;
        _scoreView.SetCurrentScoreText(_currentScore);
    }

    private void OnPlayerDied()
    {
        bool isBestScore = _currentScore > _bestScore;

        if (isBestScore)
        {
            _saver.SaveNewBestResult(RecordKeyName, _currentScore);
            _scoreView.SetRecordText(_currentScore);
            _scoreView.SetGameOverPanelText(_currentScore, isBestScore);
        }
        else
        {
            _scoreView.SetGameOverPanelText(_currentScore, isBestScore);
        }

        _gameOverScreen.SetIsGreaterThanZero(_currentScore > 0);
    }

    private void OnDoubleResult()
    {
        _scoreView.HideTexts();
        _currentScore *= _scoreMultiplier;
        OnPlayerDied();
    }
}