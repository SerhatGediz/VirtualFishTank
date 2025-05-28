using UnityEngine;


public class MouseRayDrawer : MonoBehaviour
{
    void Update()
    {
        // mouse screen position
        Vector3 mousePosition = Input.mousePosition;

        // Create ray from main camera
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        // Draw Ray with yellow line on screen
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.blue);
    }

    
}