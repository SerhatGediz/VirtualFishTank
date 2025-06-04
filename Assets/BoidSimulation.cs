using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BoidSimulationControl : MonoBehaviour
{
    public GameObject boidPrefab = null;
    public int numBoidsToSpawn = 10;
    public List<Boid> boids = null;
    private GameObject targetObject;
    public GameObject foodPrefab;
    private List<GameObject> foodList = new List<GameObject>();


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

        switch(controlMode)
        {
            case ControlMode.Seek:

            for (int i = 0; i < boids.Count; i++)
            {
               
                Vector3 accel = boids[i].Seek(targetObject.transform.position, boids[i].accelMax);

               
                if (Input.GetMouseButton(0))
                {
                    boids[i].rigidBody.linearVelocity += accel * Time.fixedDeltaTime;
                    Debug.DrawRay(boids[i].transform.position, accel, Color.green);
                }
                
                else if (Input.GetMouseButton(1))
                {
                    boids[i].rigidBody.linearVelocity -= accel * Time.fixedDeltaTime;
                    Debug.DrawRay(boids[i].transform.position, -accel, Color.red);

                    
                }
               



            }
                break;



            case ControlMode.Pursue:
        
            for (int i = 0; i < boids.Count; i++)
            {
              
                Vector3 accel = boids[i].Pursue(targetObject.transform.position, boids[i].accelMax);

                if (Input.GetMouseButton(0))
                {
                   
                    boids[i].rigidBody.linearVelocity += accel * Time.fixedDeltaTime;
                    Debug.DrawRay(boids[i].transform.position, accel, Color.blue);
                }
            }
                    break;
        

         case ControlMode.Food:

        
            for (int i = 0; i < boids.Count; i++)
            {
                Vector3 accel = boids[i].SeekNearestFood(10f, boids[i].accelMax); 
                boids[i].rigidBody.linearVelocity += accel * Time.fixedDeltaTime;
                Debug.DrawRay(boids[i].transform.position, accel, Color.magenta);
            }
                break;
        }
       if (controlMode == ControlMode.Food && Input.GetMouseButtonDown(0))
           {
             SpawnFood();
           }

    }





 

    public void SpawnFood()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Vector3 spawnPos = hit.point;
            Instantiate(foodPrefab, spawnPos, Quaternion.identity);
        }
    }

}




