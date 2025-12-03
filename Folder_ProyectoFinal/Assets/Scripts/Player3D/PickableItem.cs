using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [Header("Prefab que se guardará en el inventario")]
    public GameObject itemPrefab;

    public void PickUp()
    {
        CollectItem();
    }

    private void CollectItem()
    {
        Debug.Log("Objeto recogido: " + itemPrefab.name);
        InventoryManager.Instance.AddItem(itemPrefab);
        Destroy(gameObject);
    }
}
