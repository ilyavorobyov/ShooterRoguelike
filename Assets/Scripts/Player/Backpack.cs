using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    [SerializeField] private DisplayedBullet _displayedBulletSample;
    [SerializeField] private Token _displayedTokenSample;

    private List<DisplayedBullet> _displayedBullets = new List<DisplayedBullet>();
    private Vector3 _currentPosition = new Vector3(0f, 0f, 0f);
    private Vector3 _additionPosition = new Vector3(0f, 0.28f, 0f);
    private Vector3 _addedObjectsRotation = new Vector3(0f, 0f, 90f);
    private Token _currentToken;
    private bool _isHaveToken; 

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
        if( _displayedBullets.Count > 0 )
        {
            DisplayedBullet currentBullet = _displayedBullets[_displayedBullets.Count - 1];
            _displayedBullets.RemoveAt(_displayedBullets.Count - 1);
            Destroy(currentBullet.gameObject);
            _currentPosition -= _additionPosition;
        }
    }

    public void AddToken()
    {
        _isHaveToken = true;
        _currentToken = Instantiate(_displayedTokenSample, transform.position + 
            _currentPosition, Quaternion.identity, gameObject.transform);
        _currentToken.transform.localRotation = Quaternion.Euler(_addedObjectsRotation);
        _currentPosition += _additionPosition;
    }

    public void RemoveToken()
    {
        if(_isHaveToken)
        {
            Destroy(_currentToken.gameObject);
            _currentPosition -= _additionPosition;
            _isHaveToken = false;
        }
    }
}