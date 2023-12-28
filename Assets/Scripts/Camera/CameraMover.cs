using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Vector3 _offSetPosition = new Vector3 (4, 9f, 7f);

    private void Update()
    {
        transform.position = _player.transform.position + _offSetPosition;
    }
}