using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [Header("IDs de los objetos requeridos (10 en total)")]
    [SerializeField] private List<string> expectedItemIDs = new List<string>();

    [Header("UI")]
    [SerializeField] private TMP_Text contadorText;

    [Header("Referencias")]
    public DoorUnlock door;

    private Dictionary<string, bool> itemsDict = new Dictionary<string, bool>();
    private int collected = 0;
    private int maxItems => expectedItemIDs.Count;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        itemsDict.Clear();

        foreach (string id in expectedItemIDs)
        {
            if (!itemsDict.ContainsKey(id))
                itemsDict.Add(id, false);
        }
    }

    private void Update()
    {
        if (contadorText)
            contadorText.text = $"Objetos: {collected}/{maxItems}";
    }

    // Llamado por PickableItem
    public void AddItem(string itemID)
    {
        if (!itemsDict.ContainsKey(itemID))
        {
            Debug.LogWarning("El ID no existe en expectedItemIDs: " + itemID);
            return;
        }

        if (itemsDict[itemID])
        {
            Debug.Log("Objeto ya recogido: " + itemID);
            return;
        }

        itemsDict[itemID] = true;
        collected++;

        Debug.Log($"Objeto añadido al inventario: {itemID} ({collected}/{maxItems})");

        CheckWinCondition();
    }

    public string GetInventoryText()
    {
        string text = $"Objetos encontrados ({collected}/{maxItems}):\n";

        foreach (var kv in itemsDict)
        {
            if (kv.Value)
                text += "- " + kv.Key + "\n";
        }

        return text;
    }

    private void CheckWinCondition()
    {
        if (collected >= maxItems)
        {
            Debug.Log("¡Todos los objetos fueron recogidos!");

            if (GameTimer.Instance != null)
                GameTimer.Instance.isRunning = false;

            if (GameTimer.Instance != null)
                PlayerPrefs.SetString("FinalTime", GameTimer.Instance.GetFormattedTime());

            if (door != null)
                door.UnlockDoor();
        }
    }
}