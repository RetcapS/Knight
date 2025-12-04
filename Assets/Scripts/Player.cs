using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float rotationSpeed = 10f;
    public float jumpForce = 8f;
    public float attackDamage = 30f;
    public float attackRange = 2.5f;
    public LayerMask enemyLayer = 1 << 6;
    
    
    public float battleCryDuration = 0.5f; 
    public float battleCryCooldown = 1f;
    
    public AudioClip jumpSound, attackSound, deathSound;
    public AudioClip battleCrySound; 
    
    private Rigidbody rb;
    private PlayerAnimaton animatorController;
    private Health health;
    private AudioSource audioSource;

    private Vector2 movementInput;
    private bool jumpInput;
    private bool attackInput;
    private bool battleCryInput; 
    private bool isPerformingBattleCry = false; 
    private bool canUseBattleCry = true; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animatorController = GetComponent<PlayerAnimaton>();
        health = GetComponent<Health>();
        audioSource = GetComponent<AudioSource>();
        canUseBattleCry = true; 
    }

    void Update()
    {
        GetInput();
    }

    void FixedUpdate()
    {
        if (isPerformingBattleCry)
            return;
            
        HandleMovement();
    }

    void GetInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        movementInput = new Vector2(h, v).normalized;
        
        jumpInput = Input.GetKeyDown(KeyCode.Space);
        attackInput = Input.GetMouseButtonDown(0);
        battleCryInput = Input.GetKeyDown(KeyCode.R); 
        
        if (attackInput)
        {
            animatorController?.TriggerAttackAnimation();
            PerformAttack();
            PlaySound(attackSound);

        }
        if (jumpInput)
        {
            animatorController?.OnJumpAnimationStart(); 
        }
        
        if (battleCryInput && canUseBattleCry)
        {
            StartCoroutine(PerformBattleCry());
        }
    }


    void HandleMovement()
    {
        Vector3 moveDir = new Vector3(movementInput.x, 0, movementInput.y);
        if (moveDir.sqrMagnitude > 0.01f)
        {
            moveDir.Normalize();

            Quaternion targetRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        }
    }
    
    public void PerformJump()
    {
        if (isPerformingBattleCry) return; 
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        PlaySound(jumpSound);
    }

    IEnumerator PerformBattleCry()
    {
        canUseBattleCry = false; 
        isPerformingBattleCry = true; 
        
        animatorController?.TriggerBattleCryAnimation(); 
        PlaySound(battleCrySound); 
        
        yield return new WaitForSeconds(battleCryDuration); 

        isPerformingBattleCry = false; 
        
        yield return new WaitForSeconds(battleCryCooldown);
        canUseBattleCry = true; 
    }

    public void PerformAttack()
    {
        // if (isPerformingBattleCry) return; 
        RaycastHit hit;
        // Oyuncunun önünde ve biraz yukarıda bir nokta belirliyoruz
        Vector3 attackPoint = transform.position + transform.forward * .2f + Vector3.up * .5f;
        Debug.Log("a");
        // Raycast ile düşman kontrolü
        if (Physics.Raycast(attackPoint, transform.forward, out hit, attackRange, enemyLayer))
        {
            Debug.Log("mköl");
            Health enemyHealth = hit.collider.GetComponent<Health>();
            if (enemyHealth != null)
            {
                // Hasar verme
                enemyHealth.TakeDamage(attackDamage);
                Debug.Log($"Enemy vuruldu! Hasar: {attackDamage}");
            }
        }
        
        Debug.DrawRay(attackPoint, transform.forward * attackRange, Color.red, 0.5f);
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource && clip)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}