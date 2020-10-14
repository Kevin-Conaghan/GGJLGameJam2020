using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemAttributes
{
    public string itemName;
    public Mesh itemMesh;
    public Material itemMaterial;

    public ItemAttributes(string name, Mesh currMesh, Material currMaterial)
    {
        this.itemName = name;
        this.itemMesh = currMesh;
        this.itemMaterial = currMaterial;
    }

}

public class Item : MonoBehaviour
{
    public GameObject thisItemPrefab;
    private Character_Inventory m_playerInventory;
    private bool m_isInTrigger;

    public string itemName;

    public ItemAttributes m_itemDetails;

    // Start is called before the first frame update
    void Start()
    {
        m_itemDetails = new ItemAttributes(itemName, this.gameObject.GetComponent<Mesh>(), this.gameObject.GetComponent<Material>());
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (m_playerInventory != null)
                {
                    if (m_playerInventory.GetPocketedItemName() != "")
                    {
                        m_playerInventory.GetComponent<Character_Inventory>().SetPocketedItem(m_itemDetails.itemName, m_itemDetails.itemMesh, m_itemDetails.itemMaterial);

                        this.gameObject.transform.parent = m_playerInventory.transform;
                        this.gameObject.transform.position = m_playerInventory.transform.position + m_playerInventory.transform.forward;
                        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                        //Destroy all components apart form the mesh so the player can carry it about
                        Destroy(this.GetComponent<SphereCollider>());
                        Destroy(this.GetComponent<BoxCollider>());  
                        Destroy(this);
                    }
                }

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_playerInventory = other.gameObject.GetComponent<Character_Inventory>();
            m_isInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            m_isInTrigger = false;
        }
    }
}
