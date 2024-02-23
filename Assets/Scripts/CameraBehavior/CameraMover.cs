using Player;
using UnityEngine;

namespace CameraBehavior
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private PlayerCharacter _player;

        private Vector3 _offSetPosition = new Vector3(-0.8f, 11.5f, 5.6f);

        private void Update()
        {
            transform.position = _player.transform.position + _offSetPosition;
        }
    }
}