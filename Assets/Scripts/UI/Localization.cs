using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;

namespace UI
{
    public class Localization : MonoBehaviour
    {
        private const string EnglishCode = "English";
        private const string RussianCode = "Russian";
        private const string TurkishCode = "Turkish";
        private const string English = "en";
        private const string Russian = "ru";
        private const string Turkish = "tr";

        [SerializeField] private LeanLocalization _leanLanguage;

        private void Awake()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
    ChangeLanguage();
#endif
        }

        private void ChangeLanguage()
        {
            string languageCode = YandexGamesSdk.Environment.i18n.lang;

            switch (languageCode)
            {
                case English:
                    _leanLanguage.SetCurrentLanguage(EnglishCode);
                    break;
                case Russian:
                    _leanLanguage.SetCurrentLanguage(RussianCode);
                    break;
                case Turkish:
                    _leanLanguage.SetCurrentLanguage(TurkishCode);
                    break;
            }
        }
    }
}