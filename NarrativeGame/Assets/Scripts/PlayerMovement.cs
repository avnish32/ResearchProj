using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movementAction, objectInteractAction;


    [SerializeField]
    private float movementSpeed = 250f;

    private IInteractable currentInteractable;
    private Rigidbody2D rb;
    private GameController gameController;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentInteractable = null;
        movementAction.asset.Enable();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objectInteractAction.action.WasReleasedThisFrame() && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    private void FixedUpdate()
    {
        if (!gameController.CanPlayerMoveOrInteract())
        {
            return;
        }
        rb.velocity = movementAction.action.ReadValue<Vector2>() * movementSpeed * Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
                currentInteractable.OnPlayerEnteredToInteract();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.OnPlayerExited();
                currentInteractable = null;
            }
        }
    }
}
