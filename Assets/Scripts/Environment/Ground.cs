using UnityEngine;
using Random = UnityEngine.Random;

namespace Environment
{
    public class Ground : MonoBehaviour
    {
        [SerializeField] private EnvironmentSpawnPoint[] _environmentSpawnPoints;
        [SerializeField] private EnvironmentElement[] _environmentElements;

        private void Awake()
        {
            int elementIndex;

            for (int i = 0; i < _environmentSpawnPoints.Length; i++)
            {
                elementIndex = Random.Range(0, _environmentElements.Length);
                Vector3 spawnPosition = _environmentSpawnPoints[i].transform.position +
                    _environmentElements[elementIndex].AdditionalPosition;
                EnvironmentElement environmentElement = Instantiate(
                    _environmentElements[elementIndex],
                    spawnPosition,
                    Quaternion.identity);
                environmentElement.transform.SetParent(transform);
            }
        }
    }
}