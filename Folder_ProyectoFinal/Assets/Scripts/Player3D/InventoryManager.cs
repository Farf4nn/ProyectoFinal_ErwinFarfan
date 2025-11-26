using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // IMPORTANTE

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private List<string> items = new List<string>();
    [SerializeField] private int maxItems = 10; // <= CANTIDAD A ENCONTRAR
    [SerializeField] private TMPro.TextMeshProUGUI contadorText; // Texto donde se mostrarán los ítems


    private void Update()
    {
        contadorText.text = "Objetos: " + items.Count + "/10";
    }

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

    public void AddItem(string itemName)
    {
        items.Add(itemName);
        Debug.Log("Inventario actual: " + string.Join(", ", items));

        CheckWinCondition();
    }

    public string GetInventoryText()
    {
        if (items.Count == 0) return "Inventario vacío.";
        return "Objetos encontrados (" + items.Count + "/10):\n" + string.Join("\n", items);
    }

    private void CheckWinCondition()
    {
        if (items.Count >= maxItems)
        {
            Debug.Log("¡HAS GANADO!");

            // Detener el temporizador
            GameTimer.Instance.isRunning = false;

            // Guardar el tiempo final
            PlayerPrefs.SetString("FinalTime", GameTimer.Instance.GetFormattedTime());

            // Cargar escena final
            SceneManager.LoadScene("Win");
        }
    }

}
