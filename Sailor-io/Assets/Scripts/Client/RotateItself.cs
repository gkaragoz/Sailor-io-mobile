using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItself : MonoBehaviour {

    [Range(5f, 30f)]
    public float speed = 5f;
    public bool clockwise = false;

    private void Awake() {
        if (Random.Range(0, 1f) <= 0.5f)
            clockwise = true;

        speed = Random.Range(5f, 30f);
    }

    private void Update() {
        transform.Rotate(new Vector3(speed, speed, speed) * Time.deltaTime);
    }

}
