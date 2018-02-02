using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : PlayerEntity {

    private void Update() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 dir = new Vector3(horizontal, 0f, vertical);
        transform.Translate(dir * MovementSpeed * Time.deltaTime);

        //if (Input.GetKey(KeyCode.Q)) {
        //    transform.Rotate(Vector3.up * -35f * Time.deltaTime);
        //}

        //if (Input.GetKey(KeyCode.E)) {
        //    transform.Rotate(Vector3.up * 35f * Time.deltaTime);
        //}
    }

}
