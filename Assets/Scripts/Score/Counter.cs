using UnityEngine;

[RequireComponent (typeof(ScoreView))]
[RequireComponent (typeof(ScoreSaver))]
public class Counter : MonoBehaviour
{
    private const string RecordKeyName = "MaxScore";

    private ScoreView _scoreView;
    private ScoreSaver _saver;
    private int _bestScore;
    private int _currentScore = 0;
    private int _dischargeScoreValue = 0;

    private void OnEnable()
    {
        EnemyHealth.EnemyDied += OnEnemyDied;
        PlayerHealth.GameOvered += OnGameOver;
        GameUI.GameReseted += OnTryGetBestScore;
    }

    private void OnDisable()
    {
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
        if(_currentScore > _bestScore)
        {
            _saver.SaveNewBestResult(RecordKeyName, _currentScore);
            _scoreView.SetRecordText(_currentScore);
            _scoreView.SetGameOverPanelText(_currentScore);
        }
    }
}