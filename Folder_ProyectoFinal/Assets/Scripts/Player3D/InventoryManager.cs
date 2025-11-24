using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<string> items = new List<string>();

    public static InventoryManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // LO SACA DE CUALQUIER PADRE PARA QUE NO SEA DESTRUIDO
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }


    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log("Inventario actual: " + string.Join(", ", items));
    }
}
