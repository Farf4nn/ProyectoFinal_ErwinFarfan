using UnityEngine;

public class LightEffect : MonoBehaviour
{
    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    [SerializeField] private float speed;
    private Light _light;

    void Start()
    {
        _light = GetComponent<Light>();
    }

    void Update()
    {
        _light.intensity = Mathf.Lerp(minIntensity, maxIntensity, Mathf.PingPong(Time.time * speed, 1));
    }
}
