using UnityEngine;

public class PickableItem : MonoBehaviour
{
    [SerializeField] private string itemName = "Item";
    public string ItemName => itemName;

    public void PickUp()
    {
        Debug.Log("Recogido: " + itemName);
        Destroy(gameObject);
    }
}
