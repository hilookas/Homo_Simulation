using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // 摄像机跟随的对象
    public Transform followTarget;
    // The speed with which the camera will be following.
    public float smoothing = 5f;
    // 偏移量
    Vector3 offset;

    void Start()
    {
        // 计算偏移量
        offset = transform.position - followTarget.position;
    }

    void LateUpdate()
    {
        Vector3 targetCamPos = followTarget.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}

// public class CameraController : MonoBehaviour
// {
//     public Transform followTarget;
//     public float distance;
//     public float height;
//     float x;
//     float z;

//     void Start()
//     {
//         x = transform.eulerAngles.x;
//         z = transform.eulerAngles.z;
//     }

//     void LateUpdate()
//     {
//         // ROTATE CAMERA:
//         Quaternion rotation = Quaternion.Euler(x, followTarget.eulerAngles.y, z);
//         transform.rotation = rotation;

//         // POSITION CAMERA:
//         Vector3 position = followTarget.position - rotation * Vector3.forward * distance + new Vector3(0, height, 0);
//         transform.position = position;
//     }
// }