using UnityEngine;

namespace UI
{
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
}