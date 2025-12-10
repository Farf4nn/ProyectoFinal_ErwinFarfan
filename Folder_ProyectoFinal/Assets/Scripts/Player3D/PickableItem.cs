using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [Header("Prefab que se guardará en el inventario (solo referencia visual si quieres)")]
    public GameObject itemPrefab;

    private string itemID;

    private void Awake()
    {
        // Se obtiene el ID del prefab
        ItemID idComponent = itemPrefab.GetComponent<ItemID>();

        if (idComponent != null)
            itemID = idComponent.itemID;
        else
            Debug.LogError("El prefab no tiene ItemID asignado: " + itemPrefab.name);
    }

    public void PickUp()
    {
        CollectItem();
    }

    private void CollectItem()
    {
        if (string.IsNullOrEmpty(itemID))
        {
            Debug.LogError("ItemID no configurado en: " + gameObject.name);
            return;
        }

        Debug.Log("Objeto recogido: " + itemID);

        InventoryManager.Instance.AddItem(itemID);

        Destroy(gameObject);
    }
}

