using UnityEngine;

public class GameOverPanelText : MonoBehaviour
{
    private void OnDisable()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}