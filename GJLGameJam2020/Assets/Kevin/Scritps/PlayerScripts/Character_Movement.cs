using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Movement : MonoBehaviour
{
    Rigidbody rb;
    public float speed;

    public Vector3 movement;
    private bool m_lockMovement;

    private bool m_isInChair;
    private bool m_isAddingForce;
    private bool m_isDecelerating;

    private float m_chairForce;

    public Slider chairPowerSlider;
    private float chairScore;
    private float shiftSpeed;

    void Start()
    {
        shiftSpeed = speed * 2.0f;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

        if (!m_lockMovement && !m_isInChair)
        {
            float moveHorizontal, moveVertical;

            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");
            if (Input.GetKey(KeyCode.LeftShift))
            {
                movement = new Vector3(moveHorizontal * shiftSpeed, 0.0f, moveVertical * shiftSpeed);
            }
            else
            {
                movement = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);
            }
        }

       // if ( !m_lockMovement && (m_isInChair && !m_isDecelerating))
       // {
       //     //player is in the chair, initiate thursters ALL SYSTEMS GO
       //     float moveHorizontal = Input.GetAxis("Horizontal");
       //     float moveVertical = Input.GetAxis("Vertical");
       // 
       //     if (moveHorizontal > 0 || moveVertical > 0  || moveHorizontal < 0 || moveVertical < 0)
       //     {
       //         //charge dem thrusters
       //         //map UI value
       //         chairPowerSlider.value = m_chairForce / 50;
       //         m_chairForce += Time.deltaTime * 100;
       //         //add score for playing with chair
       //         chairScore += Time.deltaTime * 10;
       //
       //     }
       // 
       //     //if we charged the thrusters apply the power
       //     if ((moveHorizontal == 0 && moveVertical == 0) && m_chairForce > 0)
       //     {
       //         m_isAddingForce = true;
       //     }
       // }
    }
    
    void FixedUpdate()
    {
        if (!m_lockMovement && !m_isInChair)
        {
            rb.AddRelativeForce(movement, ForceMode.Acceleration);
        }

       // if(!m_lockMovement && m_isInChair)
       // {
       //     if(m_isAddingForce)
       //     {
       //         //apply force and initiate decelarition
       //         rb.AddForce(-gameObject.transform.forward * m_chairForce, ForceMode.Impulse);
       // 
       //         chairPowerSlider.value = m_chairForce / 50;
       // 
       //         m_isAddingForce = false;
       //         m_isDecelerating = true;
       //     }
       //     //  else if (m_isDecelerating)
       //     //  {
       //     //      //decelerate
       //     //      m_chairForce -= (m_chairForce * 0.95f) * (Time.deltaTime * 1.2f);
       //     //      if (rb.sqrMagnitude <= 0.0f)
       //     //      {
       //     //          m_isDecelerating = false;
       //     //      }
       //     //  }
       // }
    }

    public void SetLockMovement(bool movementLocked)
    {
        m_lockMovement = movementLocked;
    }

    public void SetIsInChair(bool isChair)
    {
        m_isInChair = isChair;
        rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public void SetChairScore(int score)
    {
        chairScore = score;
    }

    public int GetChairScore()
    {
        return (int)chairScore;
    }

    public bool GetLockMovment()
    {
        return m_lockMovement;
    }
}
