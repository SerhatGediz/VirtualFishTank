using UnityEngine;

public class Movement : MonoBehaviour
{
   
    public Vector3 bounds = new Vector3(10, 5, 10); 
    public float moveSpeed = 2f;
    public float waitTime = 0.1f;

    private Vector3 targetPosition;
    private float waitTimer;

    private void Start()
    {
        PickNewTarget();
    }

    private void Update()
    {
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);


        if (Vector3.Distance(transform.position, targetPosition) < 0.0001f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer >= waitTime)
            {
                PickNewTarget();
                waitTimer = 0;
            }
        }
    }

    private void PickNewTarget()
    {
        
        targetPosition = new Vector3
        (
            Random.Range(-bounds.x, bounds.x),
            Random.Range(-bounds.y + 1.5f, bounds.y),
            Random.Range(-bounds.z, bounds.z)
        );
    }
}

