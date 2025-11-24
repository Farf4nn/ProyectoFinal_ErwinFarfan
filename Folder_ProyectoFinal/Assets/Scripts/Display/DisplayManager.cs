using UnityEngine;

public class DisplayManager : MonoBehaviour
{

    void Start()
    {
        Debug.Log("Displays: " + Display.displays.Length);

        for(int i = 0; i < Display.displays.Length; i++)
        {
            Display.displays[i].Activate();
        }
    }

}
