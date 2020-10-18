using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemAttributes
{
    public string itemName;
    public Mesh itemMesh;
    public Material itemMaterial;
    public bool hasItem;

    public ItemAttributes(string name, Mesh currMesh, Material currMaterial, bool Item)
    {
        this.itemName = name;
        this.itemMesh = currMesh;
        this.itemMaterial = currMaterial;
        this.hasItem = Item;
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
        m_itemDetails = new ItemAttributes(itemName, this.gameObject.GetComponent<Mesh>(), this.gameObject.GetComponent<Material>(), false);

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
                    if (!m_playerInventory.GetHasItem())
                    {
                        m_playerInventory.GetComponent<Character_Inventory>().SetPocketedItem(m_itemDetails.itemName, m_itemDetails.itemMesh, m_itemDetails.itemMaterial, true);

                        this.gameObject.transform.position = m_playerInventory.gameObject.transform.position;
                        this.gameObject.transform.parent = m_playerInventory.transform;
                        this.gameObject.transform.localRotation = m_playerInventory.gameObject.transform.rotation;
                        this.gameObject.transform.localPosition = new Vector3(-1.0f, 1.5f, 0.0f);
                        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

                        if (this.gameObject.GetComponent<Animator>())
                        {
                            Animator currAnim = this.gameObject.GetComponent<Animator>();

                            currAnim.SetBool("isAnimating", false);
                            this.gameObject.transform.localRotation = Quaternion.identity;
                        }

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
