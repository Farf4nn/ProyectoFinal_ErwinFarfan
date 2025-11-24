using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections;

public class LanguageSelector : MonoBehaviour
{
    private bool active = false;

    public void ChangeLanguage(string languageCode)
    {
        if (active) return; // evita doble ejecución
        StartCoroutine(SetLanguage(languageCode));
    }

    private IEnumerator SetLanguage(string languageCode)
    {
        active = true;

        // Espera a que Localization se inicialice
        yield return LocalizationSettings.InitializationOperation;

        // Busca el idioma por código
        var selectedLocale = LocalizationSettings.AvailableLocales.GetLocale(languageCode);

        if (selectedLocale != null)
        {
            LocalizationSettings.SelectedLocale = selectedLocale;
            Debug.Log("Idioma cambiado a: " + selectedLocale.LocaleName);
        }
        else
        {
            Debug.LogWarning("No se encontró el idioma: " + languageCode);
        }

        active = false;
    }
}