using UnityEngine;

public class GameOverPanelNewRecord : MonoBehaviour
{
    private void OnDisable()
    {
        if(gameObject.activeSelf)
            gameObject.SetActive(false);
    }
}