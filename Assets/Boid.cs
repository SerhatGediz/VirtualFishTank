using UnityEngine;

public class Boid : MonoBehaviour
{
   private GameObject targetObject;
   private  Rigidbody rigidBody;

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






    //private void Awake()
    //{
    //    rigidBody = GetComponent<Rigidbody>();
    //    rigidBody.linearVelocity = Random.insideUnitSphere;
    //}

    //public void Update()
    //{
    //    AlignToVelocity();
    //}

    //public void AlignToVelocity()
    //{
    //    transform.forward = Vector3.RotateTowards(transform.forward, rigidBody.linearVelocity.normalized, Mathf.Deg2Rad * 1800 * Time.deltaTime, 100);
    //}
}



