using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    [Header("Objetos requeridos")]
    public GameObject[] requiredObjects;

    private bool isOpen = false;

    public void TryOpenDoor()
    {
        if (isOpen) return;

        InventoryManager inv = InventoryManager.Instance;

        if (inv.HasItems(requiredObjects))
        {
            isOpen = true;
            Debug.Log("Puerta abierta.");
        }
        else
        {
            Debug.Log("Faltan objetos para abrir la puerta.");
        }
    }
}
