using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    public Vector3 movement;
    private bool m_lockMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!m_lockMovement)
        {
            float moveHorizontal = Input.GetAxis("Horizontal") * speed;
            float moveVertical = Input.GetAxis("Vertical") * speed;

            movement = new Vector3(moveHorizontal, rb.velocity.y, moveVertical);
            transform.LookAt(transform.position + new Vector3(movement.x, 0, movement.z));
        }
    }
    void FixedUpdate()
    {
        if (!m_lockMovement)
        { 
            rb.velocity = movement;
        }
        else
        {
            //stop velocity completely so the player cant skid away
            rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }

    }

    public void SetLockMovement(bool movementLocked)
    {
        m_lockMovement = movementLocked;
    }
}
