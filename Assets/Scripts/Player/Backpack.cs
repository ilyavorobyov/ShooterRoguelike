using System;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private DisplayedBullet _displayedBulletSample;
    [SerializeField] private Token _displayedTokenSample;

    private List<DisplayedBullet> _displayedBullets = new List<DisplayedBullet>();
    private Vector3 _startCurrentPosition = Vector3.zero;
    private Vector3 _currentPosition;
    private Vector3 _additionPosition = new Vector3(0f, 0.28f, 0f);
    private Vector3 _addedObjectsRotation = new Vector3(0f, 0f, 90f);
    private Token _currentToken;
    private bool _isHaveToken;

    public static event Action TokenBroughted;

    private void Awake()
    {
        _currentPosition = _startCurrentPosition;
    }

    private void OnEnable()
    {
        GameUI.GameReseted += OnReset;
    }

    private void OnDisable()
    {
        GameUI.GameReseted -= OnReset;
    }

    public void AddBullet()
    {
        DisplayedBullet displayedBullet = Instantiate(_displayedBulletSample,
            transform.position + _currentPosition, Quaternion.identity,
            gameObject.transform);
        displayedBullet.transform.localRotation = Quaternion.Euler(_addedObjectsRotation);
        _displayedBullets.Add(displayedBullet);
        _currentPosition += _additionPosition;
    }

    public void RemoveBullet()
    {
        if (_displayedBullets.Count > 0)
        {
            DisplayedBullet currentBullet = _displayedBullets[_displayedBullets.Count - 1];
            _displayedBullets.RemoveAt(_displayedBullets.Count - 1);
            Destroy(currentBullet.gameObject);
            _currentPosition -= _additionPosition;
        }
    }

    public void AddToken()
    {
        if (!_isHaveToken)
        {
            _isHaveToken = true;
            _currentToken = Instantiate(_displayedTokenSample, transform.position +
                _currentPosition, Quaternion.identity, gameObject.transform);
            _currentToken.transform.localRotation = Quaternion.Euler(_addedObjectsRotation);
            _currentPosition += _additionPosition;
        }
    }

    public void RemoveToken()
    {
        if (_isHaveToken)
        {
            _isHaveToken = false;
            Destroy(_currentToken.gameObject);
            _currentPosition -= _additionPosition;
            TokenBroughted?.Invoke();
        }
    }

    private void OnReset()
    {
        _currentPosition = _startCurrentPosition;

        foreach (DisplayedBullet displayedBullet in _displayedBullets)
        {
            Destroy(displayedBullet.gameObject);
        }

        _displayedBullets.Clear();

        if (_currentToken != null)
        {
            Destroy(_currentToken.gameObject);
            _isHaveToken = false;
        }
    }
}