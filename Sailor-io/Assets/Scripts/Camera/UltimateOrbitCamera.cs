using System;
using UnityEngine;

public class UltimateOrbitCamera : MonoBehaviour
{
    private Transform _transform;
    public bool autoRotateOn;
    public bool autoRotateReverse;
    [HideInInspector]
    public int autoRotateReverseValue = 1;
    public float autoRotateSpeed = 0.1f;
    public bool cameraCollision;
    public bool clickToRotate = true;
    public float collisionRadius = 0.25f;
    public float dampeningX = 0.9f;
    public float dampeningY = 0.9f;
    public float distance = 10f;
    private RaycastHit hit;
    public float initialAngleX;
    public float initialAngleY;
    public bool invertAxisX;
    public bool invertAxisY;
    public bool invertAxisZoom;
    [HideInInspector]
    public int invertXValue = 1;
    [HideInInspector]
    public int invertYValue = 1;
    [HideInInspector]
    public int invertZoomValue = 1;
    public string kbPanAxisX = "Horizontal";
    public string kbPanAxisY = "Vertical";
    public bool kbUseZoomAxis;
    public string kbZoomAxisName = string.Empty;
    public bool keyboardControl;
    public bool leftClickToRotate = true;
    public bool limitX;
    public bool limitY = true;
    public float maxDistance = 25f;
    public float maxSpinSpeed = 3f;
    public float minDistance = 5f;
    public string mouseAxisX = "Mouse X";
    public string mouseAxisY = "Mouse Y";
    public string mouseAxisZoom = "Mouse ScrollWheel";
    public bool mouseControl = true;
    private Vector3 position;
    private Ray ray;
    public bool rightClickToRotate;
    public float smoothingZoom = 0.1f;
    public string spinAxis = string.Empty;
    public bool SpinEnabled;
    public KeyCode spinKey = KeyCode.LeftControl;
    private bool spinning;
    private float spinSpeed;
    public bool spinUseAxis;
    public Transform target;
    private float targetDistance = 10f;
    private float x;
    public float xLimitOffset;
    public float xMaxLimit = 60f;
    public float xMinLimit = -60f;
    public float xSpeed = 1f;
    private float xVelocity;
    private float y;
    public float yLimitOffset;
    public float yMaxLimit = 60f;
    public float yMinLimit = -60f;
    public float ySpeed = 1f;
    private float yVelocity;
    public KeyCode zoomInKey = KeyCode.R;
    public KeyCode zoomOutKey = KeyCode.F;
    public float zoomSpeed = 5f;
    private float zoomVelocity;

    private void Start()
    {
        this.targetDistance = this.distance;
        if (this.invertAxisX)
        {
            this.invertXValue = -1;
        }
        else
        {
            this.invertXValue = 1;
        }
        if (this.invertAxisY)
        {
            this.invertYValue = -1;
        }
        else
        {
            this.invertYValue = 1;
        }
        if (this.invertAxisZoom)
        {
            this.invertZoomValue = -1;
        }
        else
        {
            this.invertZoomValue = 1;
        }
        if (this.autoRotateOn)
        {
            this.autoRotateReverseValue = -1;
        }
        else
        {
            this.autoRotateReverseValue = 1;
        }
        this._transform = base.transform;
        if (base.GetComponent<Rigidbody>() != null)
        {
            base.GetComponent<Rigidbody>().freezeRotation = true;
        }
        this.x = this.initialAngleX;
        this.y = this.initialAngleY;
        this._transform.Rotate(new Vector3(0f, this.initialAngleX, 0f), Space.World);
        this._transform.Rotate(new Vector3(this.initialAngleY, 0f, 0f), Space.Self);
        this.position = ((Vector3) (this._transform.rotation * new Vector3(0f, 0f, -this.distance))) + this.target.position;
    }

    private void Update()
    {
        if (this.target != null)
        {
            if (this.autoRotateOn)
            {
                this.xVelocity += (this.autoRotateSpeed * this.autoRotateReverseValue) * Time.deltaTime;
            }
            if (this.mouseControl)
            {
                if ((!this.clickToRotate || (this.leftClickToRotate && Input.GetMouseButton(0))) || (this.rightClickToRotate && Input.GetMouseButton(1)))
                {
                    this.xVelocity += (Input.GetAxis(this.mouseAxisX) * this.xSpeed) * this.invertXValue;
                    this.yVelocity -= (Input.GetAxis(this.mouseAxisY) * this.ySpeed) * this.invertYValue;
                    this.spinning = false;
                }
                this.zoomVelocity -= (Input.GetAxis(this.mouseAxisZoom) * this.zoomSpeed) * this.invertZoomValue;
            }
            if (this.keyboardControl)
            {
                if ((Input.GetAxis(this.kbPanAxisX) != 0f) || (Input.GetAxis(this.kbPanAxisY) != 0f))
                {
                    this.xVelocity -= (Input.GetAxisRaw(this.kbPanAxisX) * (this.xSpeed / 2f)) * this.invertXValue;
                    this.yVelocity += (Input.GetAxisRaw(this.kbPanAxisY) * (this.ySpeed / 2f)) * this.invertYValue;
                    this.spinning = false;
                }
                if (this.kbUseZoomAxis)
                {
                    this.zoomVelocity += (Input.GetAxis(this.kbZoomAxisName) * (this.zoomSpeed / 10f)) * this.invertXValue;
                }
                if (Input.GetKey(this.zoomInKey))
                {
                    this.zoomVelocity -= (this.zoomSpeed / 10f) * this.invertZoomValue;
                }
                if (Input.GetKey(this.zoomOutKey))
                {
                    this.zoomVelocity += (this.zoomSpeed / 10f) * this.invertZoomValue;
                }
            }
            if (this.SpinEnabled && ((this.mouseControl && this.clickToRotate) || this.keyboardControl))
            {
                if ((this.spinUseAxis && (Input.GetAxis(this.spinAxis) != 0f)) || (!this.spinUseAxis && Input.GetKey(this.spinKey)))
                {
                    this.spinning = true;
                    this.spinSpeed = Mathf.Min(this.xVelocity, this.maxSpinSpeed);
                }
                if (this.spinning)
                {
                    this.xVelocity = this.spinSpeed;
                }
            }
            if (this.limitX)
            {
                if ((this.x + this.xVelocity) < (this.xMinLimit + this.xLimitOffset))
                {
                    this.xVelocity = (this.xMinLimit + this.xLimitOffset) - this.x;
                }
                else if ((this.x + this.xVelocity) > (this.xMaxLimit + this.xLimitOffset))
                {
                    this.xVelocity = (this.xMaxLimit + this.xLimitOffset) - this.x;
                }
                this.x += this.xVelocity;
                this._transform.Rotate(new Vector3(0f, this.xVelocity, 0f), Space.World);
            }
            else
            {
                this._transform.Rotate(new Vector3(0f, this.xVelocity, 0f), Space.World);
            }
            if (this.limitY)
            {
                if ((this.y + this.yVelocity) < (this.yMinLimit + this.yLimitOffset))
                {
                    this.yVelocity = (this.yMinLimit + this.yLimitOffset) - this.y;
                }
                else if ((this.y + this.yVelocity) > (this.yMaxLimit + this.yLimitOffset))
                {
                    this.yVelocity = (this.yMaxLimit + this.yLimitOffset) - this.y;
                }
                this.y += this.yVelocity;
                this._transform.Rotate(new Vector3(this.yVelocity, 0f, 0f), Space.Self);
            }
            else
            {
                this._transform.Rotate(new Vector3(this.yVelocity, 0f, 0f), Space.Self);
            }
            if ((this.targetDistance + this.zoomVelocity) < this.minDistance)
            {
                this.zoomVelocity = this.minDistance - this.targetDistance;
            }
            else if ((this.targetDistance + this.zoomVelocity) > this.maxDistance)
            {
                this.zoomVelocity = this.maxDistance - this.targetDistance;
            }
            this.targetDistance += this.zoomVelocity;
            this.distance = Mathf.Lerp(this.distance, this.targetDistance, this.smoothingZoom);
            if (this.cameraCollision)
            {
                Vector3 vector = this._transform.position - this.target.position;
                this.ray = new Ray(this.target.position, vector.normalized);
                if (Physics.SphereCast(this.ray.origin, this.collisionRadius, this.ray.direction, out this.hit, this.distance))
                {
                    this.distance = this.hit.distance;
                }
            }
            this.position = ((Vector3) (this._transform.rotation * new Vector3(0f, 0f, -this.distance))) + this.target.position;
            this._transform.position = this.position;
            if (!this.SpinEnabled || !this.spinning)
            {
                this.xVelocity *= this.dampeningX;
            }
            this.yVelocity *= this.dampeningY;
            this.zoomVelocity = 0f;
        }
        else
        {
            Debug.LogWarning("Orbit Cam - No Target Given");
        }
    }
}
