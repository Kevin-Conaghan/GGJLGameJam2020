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

    public bool SetPocketedItem(string name, Mesh currMesh, Material currMaterial)
    {
        pocketedItem = new ItemAttributes(name, currMesh, currMaterial);
        Debug.Log(pocketedItem.itemName);
        return true;
    }

    public bool SetPocketedItem(string name)
    {
        return SetPocketedItem(name, pocketedItem.itemMesh, pocketedItem.itemMaterial);
    }
}
