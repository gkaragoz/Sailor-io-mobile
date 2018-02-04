using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShipController : ShipEntity {

    public bool rotatingRight, rotatingLeft;

    private float _rotationY, _rotationZ;

    void FixedUpdate() {
        transform.Translate(Vector3.forward * MovementSpeed * Time.deltaTime);

        if (rotatingRight) {
            _rotationY += Time.deltaTime * RotationSpeed;
            if (_rotationZ > -15f)
                _rotationZ -= Time.deltaTime * SlopeSpeed;
            transform.rotation = Quaternion.Euler(0, _rotationY, _rotationZ);

        } else if (rotatingLeft) {
            _rotationY -= Time.deltaTime * RotationSpeed;
            if (_rotationZ < 15f)
                _rotationZ += Time.deltaTime * SlopeSpeed;
            transform.rotation = Quaternion.Euler(0, _rotationY, _rotationZ);
        } else {
            if (_rotationZ > 0) {
                _rotationZ -= Time.deltaTime * SlopeSpeed;
            } else if (_rotationZ < 0) {
                _rotationZ += Time.deltaTime * SlopeSpeed;
            }
            transform.rotation = Quaternion.Euler(0, _rotationY, _rotationZ);
        }
    }

}
