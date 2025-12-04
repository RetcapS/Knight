using UnityEngine;

public class PlayerAnimaton : MonoBehaviour
{
    public float animationSpeedMultiplier = 1f;
    public float movementThreshold = 0.1f;
    public float jumpHeightThreshold = 0.5f;
    public bool showDebug = true;

    private Animator animator;
    private Rigidbody rb;
    private Player playerController;
    private Health health;

    private Vector2 movementInput;
    private float currentSpeed;
    private float verticalVelocity;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<Player>();
        health = GetComponent<Health>();
    }

    void Update()
    {
        HandleMovementAnimation();
        HandleJumpAnimation();
        
    }

    void HandleMovementAnimation()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movementInput = new Vector2(horizontal, vertical);
        currentSpeed = movementInput.magnitude;
        
        animator.SetFloat("Speed", currentSpeed, 0.1f, Time.deltaTime);
        animator.SetFloat("Horizontal", movementInput.x);
        animator.SetFloat("Vertical", movementInput.y);
    }

        void HandleJumpAnimation()
        {        
            verticalVelocity = rb.linearVelocity.y;
            if (verticalVelocity < -jumpHeightThreshold)
            {
                animator.SetBool("IsFalling", true);
            }
            else
            {
                animator.SetBool("IsFalling", false);
            }
        }

    

        public void TriggerAttackAnimation()
        {
            animator.SetTrigger("Attack");
        }

    public void TriggerBattleCryAnimation()
    {
        animator.SetTrigger("BattleCry");
    }

    public void TriggerDeathAnimation()
    {
        animator.SetTrigger("Death");
        if (rb != null) rb.isKinematic = true;
    }

    public void OnJumpAnimationStart()
    {
        
        playerController?.PerformJump();
        animator.SetTrigger("Jump"); 
    }

    public void OnDeathAnimationEnd()
    {
        if (health != null) health.DestroySelf();
    }
}