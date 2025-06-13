using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using static BoidSimulationControl;

public class Boid : MonoBehaviour
{
    private GameObject targetObject;
    public Rigidbody rigidBody;

    public float speedMax = 2;
    public float accelMax = 3;
    public float speed = 1;

    

    private void Start()
    {
        targetObject = GameObject.Find("target");
        rigidBody = GetComponent<Rigidbody>();

        
    }

    private void FixedUpdate()
    {
        Vector3 toTarget = targetObject.transform.position - transform.position;

        Vector3 toTargetNormalized = toTarget.normalized;

        Vector3 acceleration = toTargetNormalized * accelMax;

        rigidBody.linearVelocity += acceleration * Time.fixedDeltaTime;

        transform.forward = rigidBody.linearVelocity;

        float speed = rigidBody.linearVelocity.magnitude;

        if (speed > speedMax)
        {
            rigidBody.linearVelocity = rigidBody.linearVelocity * speedMax / speed;
        }

    }

    private void Update()
    {
        Debug.DrawRay(transform.position, rigidBody.linearVelocity, Color.red);
    }


    public Vector3 Seek(Vector3 target, float acceleration)
    {
        Vector3 toTarget = target - transform.position;

        Vector3 toTargetNormalized = toTarget.normalized;

        Vector3 accel = toTargetNormalized * acceleration;

        return accel;
    }

    public Vector3 Pursue(Vector3 targetPosition, float magnitude)
    {
       
        Rigidbody targetRb = targetObject.GetComponent<Rigidbody>();

      
        Vector3 targetVelocity = targetRb != null ? targetRb.linearVelocity : Vector3.zero;

     
        Vector3 toTarget = targetPosition - transform.position;

        
        float predictionTime = toTarget.magnitude / (speed + 0.01f);

       
        Vector3 futurePosition = targetPosition + targetVelocity * predictionTime;

      
        Vector3 desiredDirection = (futurePosition - transform.position).normalized;

        Vector3 acceleration = desiredDirection * magnitude;

        return acceleration;
    }

    public Vector3 SeekNearestFood(float radius, float magnitude)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, radius);
        GameObject nearestFood = null;
        float minDistance = Mathf.Infinity;

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Food"))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestFood = hit.gameObject;
                }
            }
        }

        if (nearestFood != null)
        {
            return Seek(nearestFood.transform.position, magnitude);
        }

        return Vector3.zero;
    }

    public Vector3 AvoidObstacles(float avoidStrength, float whiskerLength)
    {
        Vector3 avoidance = Vector3.zero;

        Vector3[] directions = new Vector3[]
        {
        transform.forward,                             // Orta whisker
        Quaternion.AngleAxis(30, transform.up) * transform.forward, // Sağ whisker
        Quaternion.AngleAxis(-30, transform.up) * transform.forward // Sol whisker
        };

        foreach (Vector3 dir in directions)
        {
            Ray ray = new Ray(transform.position, dir);
            if (Physics.Raycast(ray, out RaycastHit hit, whiskerLength))
            {
                if (hit.collider.CompareTag("Obstacle"))
                {
                    Vector3 awayFromObstacle = (transform.position - hit.point).normalized;
                    avoidance += awayFromObstacle * avoidStrength;
                    Debug.DrawRay(transform.position, dir * whiskerLength, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(transform.position, dir * whiskerLength, Color.green);
            }
        }

        return avoidance;
    }


}













