using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum JumpState
    {
        GROUND,
        JUMP,
        DOUBLE_JUMP
    }

    JumpState jumpState = JumpState.GROUND;
    Rigidbody rb;
    [SerializeField] float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (jumpState != JumpState.DOUBLE_JUMP && Input.GetButtonDown("Jump"))
        {
            Jump();
        }


    }

    void FixedUpdate()
    {
        // get input
        float x = Input.GetAxis("Horizontal");

        // set velocity
        rb.velocity = new Vector3(x * moveSpeed, rb.velocity.y, rb.velocity.z);
    }

    void Jump()
    {
        // set y velocity to 10, keep x and z velocity
        rb.velocity = new Vector3(rb.velocity.x, 10, rb.velocity.z);
        if (jumpState == JumpState.GROUND)
            jumpState = JumpState.JUMP;
        else if (jumpState == JumpState.JUMP)
            jumpState = JumpState.DOUBLE_JUMP;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpState = JumpState.GROUND;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpState = JumpState.JUMP;
        }
    }
}
