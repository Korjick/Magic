using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        return new Vector3(position.x - Screen.width * 1f / 2,
            position.y - Screen.height * 1f / 2,
            camera.nearClipPlane);
    }
}
