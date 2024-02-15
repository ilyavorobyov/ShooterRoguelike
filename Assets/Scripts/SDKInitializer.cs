using System.Collections;
using Agava.YandexGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class SDKInitializer : MonoBehaviour
{
    private const string GameSceneName = "GameScene";

    private void Awake()
    {
        YandexGamesSdk.CallbackLogging = true;
    }

    private IEnumerator Start()
    {
        yield return YandexGamesSdk.Initialize(OnInitialized);
    }

    private void OnInitialized()
    {
        SceneManager.LoadScene(GameSceneName);
    }
}