using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class RotateItself : MonoBehaviour {

    public bool rotateX = true;
    public bool rotateY = true;
    public bool rotateZ = true;

    [Range(5f, 30f)]
    public float speed = 5f;
    public bool clockwise = false;

    private void Awake() {
        if (UnityEngine.Random.Range(0, 1f) <= 0.5f)
            clockwise = true;

        speed = UnityEngine.Random.Range(5f, 30f);
    }

    private void Update() {
        transform.Rotate(new Vector3(speed * Convert.ToInt16(rotateX),
                                     speed * Convert.ToInt16(rotateY), 
                                     speed * Convert.ToInt16(rotateZ)) * Time.deltaTime);
    }

}
