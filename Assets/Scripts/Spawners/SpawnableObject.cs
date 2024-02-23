using UnityEngine;

namespace Spawners
{
    public class SpawnableObject : MonoBehaviour
    {
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}