using UnityEngine;

public class ScoreSaver : MonoBehaviour
{
    public void SaveNewBestResult(string saveKey, int result)
    {
        PlayerPrefs.SetInt(saveKey, result);
    }
}