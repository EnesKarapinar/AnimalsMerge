using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class LocalizationManager : MonoBehaviour
{
    [SerializeField] private Button _trButton;
    [SerializeField] private Button _enButton;
    [SerializeField] private Button _jaButton;
    [SerializeField] private Button _zhButton;

    private void Awake()
    {
        _trButton.onClick.AddListener(() => SetLanguage(LanguageType.Turkish, _trButton));
        _enButton.onClick.AddListener(() => SetLanguage(LanguageType.English, _enButton));
        _jaButton.onClick.AddListener(() => SetLanguage(LanguageType.Japanese, _jaButton));
        _zhButton.onClick.AddListener(() => SetLanguage(LanguageType.Chinese, _zhButton));

        UpdateButtonOpacityOnStart();
    }

    public void SetLanguage(LanguageType LanguageType, Button clickedButton)
    {
        foreach (var locale in LocalizationSettings.AvailableLocales.Locales)
        {
            if (locale.LocaleName.Equals(LanguageType.ToString()))
            {
                LocalizationSettings.SelectedLocale = locale;
                Debug.Log("Language sset to: " + locale.LocaleName);
                break;
            }

            Debug.Log("Locale not found for: " + LanguageType.ToString());

            UpdateButtonOpacity(clickedButton);
        }
    }

    private void UpdateButtonOpacity(Button activeButton)
    {
        // Tüm butonlarý kontrol et
        var buttons = new Button[] { _trButton, _enButton, _jaButton, _zhButton };

        foreach (var button in buttons)
        {
            var buttonImage = button.GetComponent<Image>();
            if (buttonImage != null)
            {
                var color = buttonImage.color;
                color.a = button == activeButton ? 0.5f : 1f;
                buttonImage.color = color;
            }
        }
    }

    private void UpdateButtonOpacityOnStart()
    {
        var selectedLocale = LocalizationSettings.SelectedLocale;

        // Baþlangýçta seçili olan dili kontrol et
        if (selectedLocale != null)
        {
            switch (selectedLocale.LocaleName)
            {
                case "Turkish":
                    UpdateButtonOpacity(_trButton);
                    break;
                case "English":
                    UpdateButtonOpacity(_enButton);
                    break;
                case "Japanese":
                    UpdateButtonOpacity(_jaButton);
                    break;
                case "Chinese":
                    UpdateButtonOpacity(_zhButton);
                    break;
                default:
                    Debug.LogWarning("Unknown locale at startup: " + selectedLocale.LocaleName);
                    break;
            }
        }
    }
}
