using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public bool showGizmos;

    public float turnMaximumSpeed = 1f;
    public float fowardMotionMaximumSpeed = 1f;
    public float sideMotionMaximumSpeed = 1f;
    public float jumpMaximumSpeed = 1f;

    public float probeDistanceGround = 0.1f;

    public ForceMode movementMode;
    public ForceMode rotationMode;
    public ForceMode jumpMode;

    public bool _grounded;
    public LayerMask _groundableMask;

    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;

    // Use this for initialization
    void Start () {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateGrounded();

        ProcessInputs();
    }

    private void UpdateGrounded()
    {
        var feetPositionOffset = Vector3.down * (_collider.height / 2f - 0.01f);
        var groundProbeRay = new Ray(transform.position + feetPositionOffset, Vector3.down);

        if (Physics.Raycast(groundProbeRay, probeDistanceGround, _groundableMask))
        {
            _grounded = true;
        }
        else
        {
            _grounded = false;
            Debug.DrawRay(groundProbeRay.origin, groundProbeRay.direction, Color.blue);
        }        
    }

    private void ProcessInputs()
    {
        var forwardInput = Input.GetAxis("ForwardMotion");
        var sideInput = Input.GetAxis("SideMotion");
        var turnInput = Input.GetAxis("Turn");
        var tiltInput = Input.GetAxis("Tilt");

        if (Input.GetButton("Jump"))
        {
            Jump();
        }

        if (Input.GetButton("Use"))
        {
            Use();
        }

        Turn(turnInput * turnMaximumSpeed);

        var forwardMotion = forwardInput * fowardMotionMaximumSpeed;
        var sideMotion = sideInput * sideMotionMaximumSpeed;

        if (!_grounded)
        {
            forwardMotion /= 2f;
            sideMotion /= 2f;
        }

        Move(forwardMotion, sideMotion);
        ApplyGravity();
    }

    private void ApplyGravity()
    {
        _rigidbody.MovePosition(_rigidbody.position + Vector3.down * 0.8f * Time.deltaTime);
    }

    private void Move(float forwardMotion, float sideMotion)
    {
        if (!_grounded)
        {
            var forwardMovement = transform.forward * forwardMotion;
            var sideMovement = transform.right * sideMotion;
            var effectiveForce = (forwardMovement + sideMovement);

            _rigidbody.MovePosition(_rigidbody.position + effectiveForce * Time.deltaTime);
        }

        //_rigidbody.AddForce(effectiveForce, movementMode);
    }

    private void Turn(float amount)
    {


        //_rigidbody.AddTorque(Vector3.up * amount, rotationMode);
    }

    private void Jump()
    {
        if (_grounded)
        {
            var effectiveForce = Vector3.up * jumpMaximumSpeed;
            _rigidbody.AddForce(effectiveForce, jumpMode);
        }
    }

    private void Use()
    {
    }
}
