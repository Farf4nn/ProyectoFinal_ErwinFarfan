using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<string> items = new List<string>();
    [SerializeField] private int maxItems = 10;
    [SerializeField] private TMP_Text contadorText;

    public DoorUnlock door;

    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (contadorText)
            contadorText.text = $"Objetos: {items.Count}/{maxItems}";
    }

    public void AddItem(GameObject itemObject)
    {
        items.Add(itemObject.name);

        Debug.Log("Inventario actual: " + items.Count);

        CheckWinCondition();
    }

    public string GetInventoryText()
    {
        if (items.Count == 0) return "Inventario vacío.";

        string text = $"Objetos encontrados ({items.Count}/{maxItems}):\n";

        foreach (var name in items)
        {
            text += "- " + name + "\n";
        }

        return text;
    }

    private void CheckWinCondition()
    {
        if (items.Count >= maxItems)
        {
            GameTimer.Instance.isRunning = false;

            PlayerPrefs.SetString("FinalTime", GameTimer.Instance.GetFormattedTime());

            if (door != null)
                door.UnlockDoor();

            Debug.Log("¡GANASTE! La puerta está ahora desbloqueada.");
        }
    }
}