using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerControls controls;
    private Vector2 moveInput;
    private InventoryManager inventory; // Referencia al inventario

    [Header("References")]
    [SerializeField] private CinemachineCamera playerCamera;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float interactionDistance = 3f; // Rango del raycast

    [Header("GroundCheck")]
    [SerializeField] Transform groundCheck;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private GameObject interactText;
    [SerializeField] private LayerMask interactionMask;

    private bool isGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();

        // Bloquea el cursor en el centro de la pantalla para la funcionalidad del raycast
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Start()
    {
        inventory = InventoryManager.Instance;

        if (inventory == null)
        {
            Debug.LogError("InventoryManager no existe en la escena.");
        }
    }

    private void Update()
    {
        CheckForInteractable();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        GroundChech();
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Jump.performed += OnJump;
        // Asigna la acción de "Interactuar" (debes crearla en tu Input Action Asset)
        controls.Player.Interact.performed += OnInteract;
    }
    private void OnDisable()
    {
        controls.Disable();
        controls.Player.Move.performed -= OnMovePerformed;
        controls.Player.Move.canceled -= OnMoveCanceled;
        controls.Player.Jump.performed -= OnJump;
        controls.Player.Interact.performed -= OnInteract;
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        // Este método se llama cuando presionas el botón de interactuar
        TryPickUpObject();
    }

    private void MovePlayer()
    {
        if (playerCamera == null) return;

        Vector3 cameraForward = Vector3.ProjectOnPlane(playerCamera.transform.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(playerCamera.transform.right, Vector3.up).normalized;
        Vector3 moveDirection = cameraForward * moveInput.y + cameraRight * moveInput.x;

        rb.AddForce(moveDirection * moveSpeed, ForceMode.Impulse);

        Vector3 horizontalVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        if (horizontalVelocity.magnitude > maxSpeed)
        {
            Vector3 limitedVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitedVelocity.x, rb.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGround)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void GroundChech()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    // Lógica del Raycast y Recogida
    private void CheckForInteractable()
    {
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, interactionDistance, interactionMask))
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionDistance, Color.green);
            interactText.SetActive(true);
        }
        else
        {
            Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * interactionDistance, Color.red);
            interactText.SetActive(false);
        }
    }

    private void TryPickUpObject()
    {
        RaycastHit hit;
        // Dispara un rayo desde el centro de la cámara hacia adelante
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactionDistance))
        {
            // Comprueba si el objeto golpeado tiene el script PickableItem
            PickableItem item = hit.collider.GetComponent<PickableItem>();

            if (item != null)
            {
                // Si lo tiene, lo añadimos al inventario y lo "recogemos" (destruimos en este caso simple)
                if (inventory != null)
                {
                    inventory.AddItem(item.ItemName);
                    item.PickUp();
                }
            }
        }
    }
}
