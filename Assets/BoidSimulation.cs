using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BoidSimulationControl : MonoBehaviour
{
    public GameObject boidPrefab = null;
    public int numBoidsToSpawn = 10;
    public List<Boid> boids = null;
    private GameObject targetObject;
    public enum ControlMode
    {
        Seek,
        Pursue,
        Food,
        Obstancle,
    }
    public ControlMode controlMode = ControlMode.Seek;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            controlMode = ControlMode.Seek;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            controlMode = ControlMode.Pursue;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            controlMode = ControlMode.Food;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            controlMode = ControlMode.Obstancle;
        }
    }
    private void Start()
    {
        targetObject = GameObject.Find("target");


        boids = new List<Boid>();

        for (int i = 0; i < numBoidsToSpawn; i++)
        {
            GameObject newBoid = Instantiate(boidPrefab, new Vector3(Random.Range(-0.7f, 0.7f), Random.Range(0, 0.7f), Random.Range(-0.4f, 0.4f)), Random.rotation);
            boids.Add(newBoid.GetComponent<Boid>());
            newBoid.GetComponent<Renderer>().material.SetColor("_BaseColor", Random.ColorHSV(0, 1, 0.5f, 1, 0.5f, 1));
        }

    }
    

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(transform.position);
        RaycastHit hitInfo;
        bool didHit = Physics.Raycast(ray, out hitInfo, 100);

        if ((didHit))
        {
            targetObject.transform.position = hitInfo.point;
        }
    }
    public Vector3 Seek(Vector3 target, float acceleration)
    {
        Vector3 toTarget = target - transform.position;

        Vector3 toTargetNormalized = toTarget.normalized;

        Vector3 accel = toTargetNormalized * acceleration;

        return accel;
    }
    

}
