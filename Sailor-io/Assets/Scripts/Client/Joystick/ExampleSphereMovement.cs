using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleSphereMovement : MonoBehaviour
{
    public VirtualJoystick joystick;
    public float speed = 1f;
    public float rotationSpeed = 30f;

    private void Update()
    {
        Vector3 direction = transform.localRotation * joystick.InputDirection;
        transform.position += direction * speed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x, Camera.main.transform.localRotation.eulerAngles.y, transform.rotation.eulerAngles.z));
    }
}
