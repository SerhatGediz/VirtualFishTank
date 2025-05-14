using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BoidSimulationControl : MonoBehaviour
{
    public GameObject boidPrefab = null;
    public int numBoidsToSpawn = 10;
    public List<Boid> boids = null;
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
        boids = new List<Boid>();

        for (int i = 0; i < numBoidsToSpawn; i++)
        {
            GameObject newBoid = Instantiate(boidPrefab, new Vector3(Random.Range(-0.7f, 0.7f), Random.Range(0, 0.7f), Random.Range(-0.4f, 0.4f)), Random.rotation);
            boids.Add(newBoid.GetComponent<Boid>());
        }

    }
}
