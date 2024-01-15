using UnityEngine;

public class EnvironmentElement : MonoBehaviour
{
    [SerializeField] private float _additionalYPosition;

    public Vector3 AdditionalPosition => new Vector3(0, _additionalYPosition, 0);
}