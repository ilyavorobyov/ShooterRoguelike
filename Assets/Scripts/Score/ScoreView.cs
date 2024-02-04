using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private ScorePlate _scorePlate;
    [SerializeField] private TMP_Text _recordText;
    [SerializeField] private TMP_Text _currentScoreText;

    [SerializeField] private TMP_Text _gameOverPanelScoreValueText;
    [SerializeField] private GameOverPanelText _gameOverNewRecord;
    [SerializeField] private GameOverPanelText _gameOverResult;

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

    public void SetGameOverPanelText(int score, bool isBestScore)
    {
        if(isBestScore)
        {
            _gameOverNewRecord.gameObject.SetActive(true);
        }
        else
        {
            _gameOverResult.gameObject.SetActive(true);
        }

        _gameOverPanelScoreValueText.text = score.ToString();
    }

    public void HideTexts()
    {
        _gameOverNewRecord.gameObject.SetActive(false);
        _gameOverResult.gameObject.SetActive(false);
    }
}