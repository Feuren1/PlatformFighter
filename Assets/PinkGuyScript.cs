using UnityEngine;

public class PinkGuyScript : MonoBehaviour
{
    // Inspector variables
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float standingHeight = 1f;
    public float crouchHeight = 0.5f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayerMask;
    public Animator animator;

    // Private variables
    [SerializeField] private bool isCrouching = false;
    [SerializeField] private bool isGrounded = false;
    private Rigidbody2D rb;
    private PlayerControls playerControls;

    private void Awake()
    {
        groundLayerMask = LayerMask.GetMask("StageGround");
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCrouch();
        ResetRotation();
        IsGrounded();
        HandleMove();
    }

    private void HandleMovement()
    {
        // Get horizontal input from left stick
        Vector2 moveDirection = playerControls.Gameplay.Move.ReadValue<Vector2>();

        // Apply movement to character
        Vector2 moveVelocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;
    }
    private void HandleMove()
    {
        bool jabbedAlready = false;
        if (IsGrounded())
        {
            if (playerControls.Gameplay.Jab.triggered && jabbedAlready==false)
            {
                Debug.Log("Jabbed");
                jabbedAlready = true;
                //animator.SetTrigger("Jab");
            }

            if (playerControls.Gameplay.Uptilt.triggered)
            {
                Debug.Log("Uptilted");
                //animator.SetTrigger("Uptilt");
            }
            if (playerControls.Gameplay.Ftilt.triggered)
            {
                Debug.Log("Ftilted");
                //animator.SetTrigger("Ftilt");
            }
            if (playerControls.Gameplay.Dtilt.triggered)
            {
                Debug.Log("Dtilted");
                //animator.SetTrigger("Dtilt");
            }
            
        }
        else
        {
            if (playerControls.Gameplay.Nair.triggered)
            {
                Debug.Log("Naired");
                //animator.SetTrigger("Uptilt");
            }
            if (playerControls.Gameplay.Upair.triggered)
            {
                Debug.Log("Upaired");
                //animator.SetTrigger("Ftilt");
            }
            if (playerControls.Gameplay.Dair.triggered)
            {
                Debug.Log("Daired");
                //animator.SetTrigger("Dtilt");
            }
            if (playerControls.Gameplay.Fair.triggered)
            {
                Debug.Log("Faired");
                //animator.SetTrigger("Dtilt");
            }

        }
    }
    private void HandleCrouch()
    {
        // Crouch input
        if (playerControls.Gameplay.Crouch.triggered)
        {
            if (!isCrouching)
            {
                // Check if character is grounded before crouching
                if (IsGrounded())
                {
                    // Crouch
                    
                    isCrouching = true;
                }
            }
            else
            {
                // Stand up

                isCrouching = false;
            }
        }
    }

    private void HandleJump()
    {
        // Jump input
        if (playerControls.Gameplay.Jump.triggered && (IsGrounded()))
        {
            // Jump
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void ResetRotation()
    {
        // Set rotation to zero
        rb.rotation = 0f;
    }

    // Check if character is grounded
    private bool IsGrounded()
    {
        if (rb.IsTouchingLayers(Physics2D.AllLayers))
        {
            isGrounded = true;
            return true;
        }
        else { 
            isGrounded = false;
            return false;
        }
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayerMask);
        //Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, Color.green);
        //if(hit.collider != null)
        //{
        //    isGrounded = true;
        //    return true;
        //}
        //else return false;
    }
}