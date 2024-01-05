using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _startPosition = new Vector3(0,1,0);

    private void OnEnable()
    {
        GameUI.GameStateReset += OnReset;
    }

    private void OnDisable()
    {
        GameUI.GameStateReset -= OnReset;
    }

    private void OnReset()
    {
        transform.position = _startPosition;
    }
}