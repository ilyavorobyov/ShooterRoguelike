using UnityEngine;

namespace Ground
{
    public class Rotator : MonoBehaviour
    {
        private Vector3 _rotationDirection = new Vector3(0, 3, 0);

        private void Update()
        {
            transform.Rotate(_rotationDirection * Time.deltaTime, Space.Self);
        }
    }
}