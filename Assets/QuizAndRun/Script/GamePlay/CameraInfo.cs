using UnityEngine;

public static  class CameraInfo
{
    private static Vector2 cameraSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    public static Vector2 GetCameraSize
    {
        get
        {
            
            return cameraSize;
        }

    }

    public static Vector2 GetCameraPosition
    {
        get
        {
            return Camera.main.transform.position;
        }
    }
}
