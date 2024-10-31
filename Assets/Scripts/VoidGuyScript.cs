using UnityEngine;

public class VoidGuyScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float standingHeight = 1f;
    public float crouchHeight = 0.5f;
    public float groundCheckDistance = 0.1f;
    public float fastFallSpeed = 5f;
    public LayerMask groundLayerMask;
    public Animator animator;

    [SerializeField] private bool isCrouching = false;
    [SerializeField] private bool isGrounded = false;
    private Rigidbody2D rb;
    private PlayerControls playerControls; 
    private AttacksPinkGuy attacksPinkGuy;


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
        HandleMove();
        HandleMovement();
        HandleJump();
        HandleCrouch();
        ResetRotation();
        IsGrounded();
        
        HandleFastfall();
        
    }

    private void HandleMovement()
    {
        Vector2 moveDirection = playerControls.Gameplay.Move.ReadValue<Vector2>();
        Vector2 moveVelocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);
        rb.velocity = moveVelocity;
    }
    private void HandleMove()
    {

        if (IsGrounded())
        {
            if (playerControls.Gameplay.Jab.triggered)
            {
                animator.SetTrigger("Jab");
                

                //animator.SetTrigger("Jab");
            }

            if (playerControls.Gameplay.Uptilt.triggered)
            {   
                attacksPinkGuy.UpTilt();
                Debug.Log("Uptilted");
                //animator.SetTrigger("Uptilt");
            }
            if (playerControls.Gameplay.Ftilt.triggered)
            {
                attacksPinkGuy.FTilt();
                Debug.Log("Ftilted");
                //animator.SetTrigger("Ftilt");
            }
            if (playerControls.Gameplay.Dtilt.triggered)
            {
                attacksPinkGuy.DTilt();
                Debug.Log("Dtilted");
                //animator.SetTrigger("Dtilt");
            }
            
        }
        else
        {
            if (playerControls.Gameplay.Nair.triggered)
            {
                attacksPinkGuy.NAir();
                Debug.Log("Naired");
                //animator.SetTrigger("Uptilt");
            }
            if (playerControls.Gameplay.Upair.triggered)
            {
                attacksPinkGuy.UpAir();
                Debug.Log("Upaired");
                //animator.SetTrigger("Ftilt");
            }
            if (playerControls.Gameplay.Dair.triggered)
            {
                attacksPinkGuy.DAir();
                Debug.Log("Daired");
                //animator.SetTrigger("Dtilt");
            }
            if (playerControls.Gameplay.Fair.triggered)
            {
                attacksPinkGuy.DTilt();
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

    private void HandleFastfall()
    {
    // Check if character is not grounded
    if (!IsGrounded())
    {
        // Check if fastfall input is triggered and character is at the apex of the jump
         if (playerControls.Gameplay.Fastfall.triggered && rb.velocity.y < 0.0001f)
        { 
            Debug.Log("Fastfalling");
            rb.velocity = new Vector2(rb.velocity.x, -fastFallSpeed); // Set negative velocity on y-axis for fast falling
        }
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
        //needs to be changed works for all layers but should be doent with physics2D.raycast....
        if (rb.IsTouchingLayers(Physics2D.AllLayers))
        {
            isGrounded = true;
            return true;
        }
        else { 
            isGrounded = false;
            return false;
        }
    }
}