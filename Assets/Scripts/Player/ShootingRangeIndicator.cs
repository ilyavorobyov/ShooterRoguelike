using UnityEngine;

namespace Player
{
    public class ShootingRangeIndicator : MonoBehaviour
    {
        [SerializeField] private Transform _player;

        private float _yPosition = 1f;

        private void LateUpdate()
        {
            transform.position = new Vector3(
                _player.position.x,
                _yPosition,
                _player.position.z);
        }
    }
}