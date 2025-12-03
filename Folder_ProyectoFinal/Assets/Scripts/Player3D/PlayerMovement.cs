using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private PlayerControls controls;
    private Vector2 moveInput;
    private InventoryManager inventory;

    [Header("References")]
    [SerializeField] private CinemachineCamera playerCamera;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float interactionDistance = 3f;

    [Header("GroundCheck")]
    [SerializeField] Transform groundCheck;
    [SerializeField] private float groundDistance = 0.2f;
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private GameObject interactText;
    [SerializeField] private LayerMask interactionMask;

    private bool isGround;

    private bool inventoryOpen = false;
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private TextMeshPro inventoryText;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        controls = new PlayerControls();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        inventory = InventoryManager.Instance;
    }

    private void Update()
    {
        CheckForInteractable();
        GroundCheck();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void OnEnable()
    {
        controls.Enable();

        controls.Player.Move.performed += OnMovePerformed;
        controls.Player.Move.canceled += OnMoveCanceled;
        controls.Player.Interact.performed += OnInteract;
        controls.Player.Inventory.performed += OnInventoryToggle;
    }

    private void OnDisable()
    {
        controls.Disable();

        controls.Player.Move.performed -= OnMovePerformed;
        controls.Player.Move.canceled -= OnMoveCanceled;
        controls.Player.Interact.performed -= OnInteract;
        controls.Player.Inventory.performed -= OnInventoryToggle;
    }

    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext ctx)
    {
        moveInput = Vector2.zero;
    }

    private void MovePlayer()
    {
        if (playerCamera == null) return;

        Vector3 fwd = Vector3.ProjectOnPlane(playerCamera.transform.forward, Vector3.up).normalized;
        Vector3 right = Vector3.ProjectOnPlane(playerCamera.transform.right, Vector3.up).normalized;

        Vector3 direction = fwd * moveInput.y + right * moveInput.x;
        direction.Normalize();

        Vector3 newVel = direction * moveSpeed;
        newVel.y = rb.linearVelocity.y;

        rb.linearVelocity = newVel;
    }

    private void GroundCheck()
    {
        isGround = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    }

    private void CheckForInteractable()
    {
        if (Physics.Raycast(playerCamera.transform.position,
                            playerCamera.transform.forward,
                            interactionDistance,
                            interactionMask))
        {
            interactText.SetActive(true);
        }
        else
        {
            interactText.SetActive(false);
        }
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    {
        TryPickUpOrInteract();
    }

    private void TryPickUpOrInteract()
    {
        RaycastHit hit;

        if (Physics.Raycast(playerCamera.transform.position,
                            playerCamera.transform.forward,
                            out hit,
                            interactionDistance,
                            interactionMask))
        {
            // OBJETO RECOGIBLE
            PickableItem item = hit.collider.GetComponent<PickableItem>();
            if (item != null)
            {
                item.PickUp();
                return;
            }

            // PUERTA
            DoorUnlock door = hit.collider.GetComponent<DoorUnlock>();
            if (door != null)
            {
                door.TryOpenDoor();
                return;
            }
        }
    }

    private void OnInventoryToggle(InputAction.CallbackContext context)
    {
        inventoryOpen = !inventoryOpen;

        inventoryUI.SetActive(inventoryOpen);

        if (inventoryOpen)
        {
            inventoryText.text = inventory.GetInventoryText();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}