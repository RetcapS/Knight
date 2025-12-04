using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public LayerMask playerLayer = 1 << 0; 
    
    private Rigidbody rb;
    private Health health;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        health = GetComponent<Health>();
    }

    public void Move(Vector3 direction)
    {
       
    }

    public void Jump()
    {
       
    }

    public void Attack()
    {
        
    }
        void Update()
    {
        
    }
}