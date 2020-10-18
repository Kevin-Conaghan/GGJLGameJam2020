using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee_Interactable : Interactable
{
    protected Character_Inventory m_playerInventory;
    public string m_completionItemName;
    public string m_requiredItemName;

    public bool isDestroyItem;

    // Update is called once per frame
    protected override void Update()
    {
        if (m_isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                InventoryCheck(m_requiredItemName);
            }
        }

        if (m_isDoingTask)
        {
            //lock the players movement while interacting
            playerMovement.SetLockMovement(true);

            UpdateObjectiveTask(objectiveTime);

        }

        if (isDestroyItem)
        {
            if(m_isObjectiveCompleted)
            {
                if (playerObject.transform.childCount > 0)
                {
                    Destroy(playerObject.transform.GetChild(1).gameObject);
                }
            }
        }
    }

    public void InventoryCheck(string requiredItemName)
    {
        //we should now have playerObject reference
        m_playerInventory = playerObject.GetComponent<Character_Inventory>();

        //check to make sure the player has the cofee mug
        if (m_playerInventory.pocketedItem.itemName == requiredItemName)
        {
            m_isDoingTask = true;
            UpdateItem(m_completionItemName);
        }
        else
        {
            //pop up ui stating player doesnt have mug
            Debug.Log("No mug you mug");

        }
    }

    public void UpdateItem(string name)
    {
        //update item in player inventory for next objective
        m_playerInventory.SetPocketedItem(name);
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if the player is inside the trigger and is pressing the interact key
        if (other.tag == "Player")
        {
            playerObject = other.gameObject;
            m_isInTrigger = true;
        }
        else
        {
            m_isInTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_isInTrigger = false;
    }
}
