using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   
    public Transform target; 

    public Vector3 offset = new Vector3(0, 5, -8); 

    public float smoothSpeed = 11f; 

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

  
        Vector3 desiredPos = target.position + offset;
    
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);
        
        transform.position = smoothedPosition;
        
        transform.LookAt(target);
    }
}