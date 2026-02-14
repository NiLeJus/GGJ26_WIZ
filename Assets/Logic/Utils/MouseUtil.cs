using UnityEngine;

public class MouseUtil 
{
    private static Camera camera = Camera.main;
    
    public static Vector3 GetMousePositionInWorldSpace(float zValue = 0f)
    {
        // This creates a plane that is perfectly flat at the given Z-depth.
        // This is the correct way to get mouse positions in a 2.5D world
        // without introducing Z-depth errors from the camera's angle.
        Plane dragPlane = new(Vector3.forward, new Vector3(0, 0, zValue));
        
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (dragPlane.Raycast(ray, out float distance))
        {
            return ray.GetPoint(distance);
        }
        Debug.LogWarning("Mouse position could not be calculated.");
        return Vector3.zero;
    }
}
