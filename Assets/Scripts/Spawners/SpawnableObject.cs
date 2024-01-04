using UnityEngine;

public class SpawnableObject : MonoBehaviour
{
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}