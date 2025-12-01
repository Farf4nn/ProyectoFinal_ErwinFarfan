using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickableItem : MonoBehaviour
{
    [Header("Prefab que se guardará en el inventario")]
    public GameObject itemPrefab;

    // Este método sirve para el InputSystem (si lo quieres usar también)
    public void PickUp(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            CollectItem();
        }
    }

    // Este método es llamado desde el raycast sin parámetros
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
