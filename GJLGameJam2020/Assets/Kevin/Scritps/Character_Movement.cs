using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Movement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    private bool m_lockMovement;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (!m_lockMovement)
        {
            float moveHorizontal = Input.GetAxis("Horizontal") * speed;
            float moveVertical = Input.GetAxis("Vertical") * speed;

            Vector3 movement = new Vector3(moveHorizontal, rb.velocity.y, moveVertical);
            transform.LookAt(transform.position + new Vector3(movement.x, 0.0f, movement.z));

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
