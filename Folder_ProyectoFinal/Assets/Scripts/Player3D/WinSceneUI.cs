using UnityEngine;
using TMPro;

public class WinSceneUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalTimeText;

    private void Start()
    {
        string time = PlayerPrefs.GetString("FinalTime", "00:00");
        finalTimeText.text = "Tiempo Final: " + time;
    }
}
