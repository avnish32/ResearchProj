using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private InputActionReference movementAction;


    [SerializeField]
    private float movementSpeed = 250f;

    private IInteractable currentInteractable;
    private Rigidbody2D rb;

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
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }

        
    }

    private void FixedUpdate()
    {
        rb.velocity = movementAction.action.ReadValue<Vector2>() * movementSpeed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = interactable;
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision != null)
        {
            IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                currentInteractable = null;
            }
        }
    }
}
