using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorUnlock : MonoBehaviour
{
    private bool isUnlocked = false;

    [SerializeField] private string winSceneName = "WinScene";

    public void UnlockDoor()
    {
        isUnlocked = true;
        Debug.Log("La puerta ahora ESTÁ DESBLOQUEADA.");
    }

    public void TryOpenDoor()
    {
        if (!isUnlocked)
        {
            Debug.Log("La puerta está cerrada todavía.");
            return;
        }

        Debug.Log("Puerta abierta. Cargando escena de victoria…");
        SceneManager.LoadScene(winSceneName);
    }
}
