using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Camera cam;
    public GameObject indicator;
    public LayerMask lookable;
    public float speed = 1.0f;
    public float acceleration = 50;
    public float maxStamina = 10;
    private float stamina;
    public float sprintSpeed = 2;
    private bool running;
    public bool headBob = true;
    public float bobDist = 1;
    public float bobInterval = .25f;
    private float headTargetPos = 0;
    private bool headBobbing = false;
    private Vector3 move;
    private Vector3 localMove;
    private float bobTimer = 0;
    private float oldHeadHeight = 0;
    private float walkingSpeed;
    private float walkingBobInterval;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cam = Camera.main;
        walkingSpeed = speed;
        walkingBobInterval = bobInterval;
    }

    // Update is called once per frame
    void Update()
    {
        DoMovement();
        DoIndicator();
    }

    private void DoIndicator()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 100, lookable))
        {
            indicator.transform.position = hit.point;
        }
    }

    private void DoMovement()
    {
        float mouseUp = Input.GetAxis("Mouse Y");
        float mouseRight = Input.GetAxis("Mouse X");

        bobInterval = walkingBobInterval / (speed / walkingSpeed);

        cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles + new Vector3(mouseUp, 0, 0));
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, mouseRight, 0));

        if (headBob)
        {
            headBobbing = (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);
            if (!headBobbing)
            {
                if (headTargetPos != 0)
                {
                    bobTimer = 0;
                    oldHeadHeight = cam.transform.localPosition.y;
                }
                headTargetPos = 0;
            }
            else if (cam.transform.localPosition.y == headTargetPos)
            {
                oldHeadHeight = headTargetPos;
                if (headTargetPos == 0)
                    headTargetPos = -bobDist;
                else
                    headTargetPos = -headTargetPos;
                bobTimer = 0;
            }

            bobTimer = Mathf.Clamp(bobTimer + Time.deltaTime, 0, bobInterval);
            cam.transform.localPosition = Vector3.up * Mathf.Lerp(oldHeadHeight, headTargetPos, bobTimer * (1 / bobInterval));
        }

        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = Vector3.ClampMagnitude(move, 1) * speed;
        localMove = transform.localToWorldMatrix * move * acceleration;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            running = true;
            speed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            running = false;
            speed = walkingSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            rb.AddForce(localMove);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, move.magnitude);
        }

        
    }
}
