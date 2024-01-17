using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScorePlate _scorePlate;
    [SerializeField] private TMP_Text _recordText;
    [SerializeField] private TMP_Text _currentScoreText;
    [SerializeField] private TMP_Text _gameOverPanelNewRecordText;
    [SerializeField] private GameOverPanelNewRecord _gameOverPanelNewRecord;

    public void ShowPlate(int score)
    {
        _scorePlate.gameObject.SetActive(true);
        SetRecordText(score);
    }

    public void SetRecordText(int score)
    {
        _recordText.text = score.ToString();
    }

    public void SetCurrentScoreText(int currentScore)
    {
        _currentScoreText.text = currentScore.ToString();
    }

    public void SetGameOverPanelText(int score)
    {
        _gameOverPanelNewRecord.gameObject.SetActive(true);
        _gameOverPanelNewRecordText.text = score.ToString();
    }
}