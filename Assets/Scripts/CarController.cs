using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wheel
{
    public Transform transform;
    public WheelCollider collider;
    
    // 记录初始位置
    [HideInInspector]
    public Quaternion steerlessRotation;

    public void Setup() => steerlessRotation = transform.rotation;
}

[System.Serializable]
public class Axle
{
    public Wheel leftWheel;
    public Wheel rightWheel;
    public bool motor; // 此车轮是否连接到电机？
    public bool steering; // 此车轮是否施加转向角？
}

public class CarController : MonoBehaviour
{
    public List<Axle> axles; // 关于每个轴的信息
    public float maxMotorTorque; // 电机可对车轮施加的最大扭矩
    public float maxSteeringAngle; // 车轮的最大转向角

    // Start is called before the first frame update
    void Start()
    {
        foreach (Axle axle in axles) {
            axle.leftWheel.Setup();
            axle.rightWheel.Setup();
        }
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
     
    public void ApplyLocalPositionToVisuals(Wheel wheel)
    {
        Vector3 position;
        Quaternion rotation;
        wheel.collider.GetWorldPose(out position, out rotation);
     
        wheel.transform.position = position;
        wheel.transform.rotation = rotation;
        wheel.transform.Rotate(wheel.steerlessRotation.eulerAngles, Space.Self);
    }
     
    public void FixedUpdate()
    {
        float motor = maxMotorTorque * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
     
        foreach (Axle axle in axles) {
            if (axle.steering) {
                axle.leftWheel.collider.steerAngle = steering;
                axle.rightWheel.collider.steerAngle = steering;
            }
            if (axle.motor) {
                axle.leftWheel.collider.motorTorque = motor;
                axle.rightWheel.collider.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axle.leftWheel);
            ApplyLocalPositionToVisuals(axle.rightWheel);
        }
    }
}