using UnityEngine;

[RequireComponent(typeof(ScoreView))]
[RequireComponent(typeof(ScoreSaver))]
public class ScoreCounter : MonoBehaviour
{
    private const string RecordKeyName = "MaxScore";

    private ScoreView _scoreView;
    private ScoreSaver _saver;
    private int _bestScore;
    private int _currentScore = 0;
    private int _dischargeScoreValue = 0;
    private int _scoreMultiplier = 2;

    private void OnEnable()
    {
        RewardedVideoAd.RewardAdDoubleResultViewed += OnDoubleResult;
        EnemyHealth.EnemyDied += OnEnemyDied;
        PlayerHealth.GameOvered += OnGameOver;
        GameUI.GameReseted += OnTryGetBestScore;
    }

    private void OnDisable()
    {
        RewardedVideoAd.RewardAdDoubleResultViewed -= OnDoubleResult;
        EnemyHealth.EnemyDied -= OnEnemyDied;
        PlayerHealth.GameOvered -= OnGameOver;
        GameUI.GameReseted -= OnTryGetBestScore;
    }

    private void Awake()
    {
        _scoreView = GetComponent<ScoreView>();
        _saver = GetComponent<ScoreSaver>();
        OnTryGetBestScore();
    }

    private void OnTryGetBestScore()
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

    private void OnEnemyDied()
    {
        _currentScore++;
        _scoreView.SetCurrentScoreText(_currentScore);
    }

    private void OnGameOver()
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
    }

    private void OnDoubleResult()
    {
        _scoreView.HideTexts();
        _currentScore *= _scoreMultiplier;
        OnGameOver();
    }
}