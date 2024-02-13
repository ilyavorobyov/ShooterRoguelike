using UnityEngine;

public class WaveInfoText : MonoBehaviour
{
    private float _duration = 2.5f;

    private void OnEnable()
    {
        Invoke(nameof(Hide), _duration);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}