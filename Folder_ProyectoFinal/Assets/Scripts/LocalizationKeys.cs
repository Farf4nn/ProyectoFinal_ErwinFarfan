using UnityEngine;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections;

public class LocalizationKeys : MonoBehaviour
{
    [Header("Localization Settings")]
    public string tableCollectionName = "MenuTable"; // Nombre de tu tabla de localización 
    //Second Table Now

    [Header("UI References")]
    [SerializeField] private TMP_Text languageText; // Asigna aquí un Text (o TMP_Text) desde el inspector

    void Start()
    {
        StartCoroutine(PrintAllKeysAndShowLanguage());
    }

    private IEnumerator PrintAllKeysAndShowLanguage()
    {
        // 1 Espera a que el sistema de localización esté completamente inicializado
        yield return LocalizationSettings.InitializationOperation;

        // 2 Obtiene el idioma (Locale) actualmente seleccionado
        Locale currentLocale = LocalizationSettings.SelectedLocale;
        Debug.Log($"Current Locale: {currentLocale.LocaleName} ({currentLocale.Identifier.Code})");

        //  Muestra el idioma actual en tu UI
        if (languageText != null)
        {
            languageText.text = currentLocale.LocaleName; // Ejemplo: "Spanish"
        }

        //  Carga la tabla de localización actual
        AsyncOperationHandle<StringTable> op = LocalizationSettings.StringDatabase.GetTableAsync(tableCollectionName);
        yield return op;

        if (op.Status == AsyncOperationStatus.Succeeded)
        {
            StringTable table = op.Result;
            Debug.Log($"Loaded table: {tableCollectionName} ({table.LocaleIdentifier.Code})");

            // 5 Muestra todas las claves de la tabla
            foreach (var entry in table.SharedData.Entries)
            {
                Debug.Log($"Key: {entry.Key}");
            }

            // 6 Muestra los valores traducidos según el idioma actual
            foreach (var entry in table.Values)
            {
                Debug.Log($"Key: {entry.Key} | Value: {entry.Value}");
            }
        }
        else
        {
            Debug.LogError($"Failed to load table: {tableCollectionName}");
        }
    }
}