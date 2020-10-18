using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Inventory : MonoBehaviour
{

    public ItemAttributes pocketedItem; 

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetPocketedItemName()
    {
        return pocketedItem.itemName;
    }

    public bool GetHasItem()
    {
        return pocketedItem.hasItem;
    }


    public bool SetPocketedItem(string name, Mesh currMesh, Material currMaterial, bool item)
    {
        pocketedItem = new ItemAttributes(name, currMesh, currMaterial, item);
        Debug.Log(pocketedItem.itemName);
        return true;
    }

    public bool SetPocketedItem(string name)
    {
        return SetPocketedItem(name, pocketedItem.itemMesh, pocketedItem.itemMaterial, true);
    }
    public bool SetPocketedItem(bool item)
    {
        return SetPocketedItem(pocketedItem.itemName, pocketedItem.itemMesh, pocketedItem.itemMaterial, item);
    }
}
