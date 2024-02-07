using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Vector3 _offSetPosition = new Vector3(-0.8f, 11.5f, 5.6f);

    private void Update()
    {
        transform.position = _player.transform.position + _offSetPosition;
    }
}