using Unity.VisualScripting;
using UnityEngine;
using static BoidSimulationControl;

public class Boid : MonoBehaviour
{
    private GameObject targetObject;
    private Rigidbody rigidBody;

    public float speed = 2;
    public float accelMax = 3;

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
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, rigidBody.linearVelocity, Color.red);
    }

    
   
}



   









