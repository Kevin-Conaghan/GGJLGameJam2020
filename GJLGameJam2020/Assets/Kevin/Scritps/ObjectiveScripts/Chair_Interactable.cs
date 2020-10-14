using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chair_Interactable : Interactable
{
    private bool m_isInChair;
    public Slider chairPowerSlider;
    private Character_Movement playerMovementScript;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (m_isInChair)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                //get out of the chair
                this.gameObject.transform.parent = null;
                m_isInChair = false;
                playerMovement.SetIsInChair(m_isInChair);
                playerMovement.chairPowerSlider.gameObject.SetActive(false);

                //get score and then reset score
                int chairScore = playerMovement.GetChairScore();
                playerMovement.SetChairScore(0);
                currGameManager.SetPlayerScore(chairScore);
            }
        }

        if (m_isInTrigger)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                //disable the collider on the chair to allow player to sit on it
                this.GetComponent<BoxCollider>().enabled = false;
                this.GetComponent<SphereCollider>().enabled = false;
                this.GetComponent<Rigidbody>().isKinematic = true;
                
                playerObject.transform.position = this.transform.position;
                playerObject.transform.rotation = this.transform.rotation;
                this.transform.parent = playerObject.transform;

                //enable chair functionality
                m_isInChair = true;
                playerMovement.SetIsInChair(m_isInChair);
                playerMovement.chairPowerSlider.gameObject.SetActive(true);
            }
        }
    }
}
