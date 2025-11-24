using Unity.Mathematics;
using UnityEngine;

//public class PlayerCamera : MonoBehaviour
//{
//    [SerializeField] private float sensibility = 100;
//
//    public Transform Player;
//    public float HorizontalRotation = 0;
//    public float VerticalRotation = 0;
//    void Start()
//    {
//        Cursor.visible = false;
//        Cursor.lockState = CursorLockMode.Locked;
//    }
//    void Update()
//    {
//        float ValorX = Input.GetAxis("Mouse X") * sensibility * Time.deltaTime;
//        float ValorY = Input.GetAxis("Mouse Y") * sensibility * Time.deltaTime;
//
//        HorizontalRotation += ValorX;
//        VerticalRotation -= ValorY;
//
//        HorizontalRotation = Mathf.Clamp(HorizontalRotation, -80, 80);
//
//        transform.localRotation = Quaternion.Euler(VerticalRotation, 0f, 0f);
//
//        Player.Rotate(Vector3.up * ValorX);
//    }
//}
