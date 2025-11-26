using UnityEngine;
using TMPro;    // Solo si quieres mostrarlo en HUD (opcional)

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;
    private float elapsedTime = 0f;
    public bool isRunning = true;
    [SerializeField] TMP_Text timerText;  // Arrastrar desde el inspector

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            if (timerText != null)
                timerText.text = GetFormattedTime();
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Devuelve tiempo en minutos y segundos
    public string GetFormattedTime()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
