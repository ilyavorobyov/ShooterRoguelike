using UnityEngine;

public class FrameRate : MonoBehaviour
{
    private int _targetFrameRate = 120;

    private void Awake()
    {
        Application.targetFrameRate = _targetFrameRate;
    }
}