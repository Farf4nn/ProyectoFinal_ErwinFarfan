using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> items = new List<GameObject>();
    [SerializeField] private int maxItems = 10;
    [SerializeField] private TMPro.TextMeshProUGUI contadorText;

    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (contadorText)
            contadorText.text = $"Objetos: {items.Count}/{maxItems}";
    }

    public void AddItem(GameObject itemObject)
    {
        // Limpia referencias a objetos destruidos
        items.RemoveAll(i => i == null);

        items.Add(itemObject);
        Debug.Log("Inventario actual: " + items.Count);
        CheckWinCondition();
    }

    public bool HasItem(GameObject obj)
    {
        return items.Contains(obj);
    }

    public bool HasItems(GameObject[] requiredObjects)
    {
        foreach (var obj in requiredObjects)
        {
            if (!items.Contains(obj))
                return false;
        }
        return true;
    }

    public string GetInventoryText()
    {
        if (items.Count == 0) return "Inventario vacío.";

        string text = $"Objetos encontrados ({items.Count}/{maxItems}):\n";

        foreach (var g in items)
        {
            if (g != null)
                text += "- " + g.name + "\n";
        }

        return text;
    }

    private void CheckWinCondition()
    {
        if (items.Count >= maxItems)
        {
            GameTimer.Instance.isRunning = false;

            PlayerPrefs.SetString("FinalTime", GameTimer.Instance.GetFormattedTime());

            Debug.Log("GANASTE!");
        }
    }
}
