using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Interactable : MonoBehaviour
{
    public float objectiveTime;
    private float currObjTime;
    private float m_normalizedObjTime;

    private bool m_isInTrigger;
    protected static bool m_isDoingTask;
    private bool m_isNeedReset;

    //UI Variables
    public Text randDialogue;
    public Slider objectiveDurTemplate;
    private Slider m_objectiveDuration;
    private Canvas m_InteractableCanvas;
    private RectTransform m_canvasRect;
    private bool hasSpawnedUI;

    private Character_Movement player;

    // Start is called before the first frame update
    void Start()
    {
        //find the player object so we can reference the movement script and control locking/unlocking of movement
        GameObject tempObject = GameObject.FindGameObjectWithTag("Player");
        player = tempObject.GetComponent<Character_Movement>();
        //reset temp for later use
        tempObject = null;
        m_isInTrigger = false;
        m_isDoingTask = false;
        hasSpawnedUI = false;

        //Find the interactable Canvas so that we can add UI elements to it
        tempObject = GameObject.FindGameObjectWithTag("InteractableUI");
        m_InteractableCanvas = tempObject.GetComponent<Canvas>();
        m_canvasRect = tempObject.GetComponent<RectTransform>();

        if (m_InteractableCanvas == null)
        {
            Debug.Log("Could not locate Canvas component on " + tempObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Only start the objective if the player is inside the trigger and has pressed E
        if (m_isInTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                m_isDoingTask = true;
            }
        }

        if (m_isDoingTask)
        {
            //lock the players movement while interacting
            player.SetLockMovement(true);

            if (!hasSpawnedUI)
            {
                //Spawn UI Elements
                m_objectiveDuration = Instantiate(objectiveDurTemplate, m_InteractableCanvas.transform.position, Quaternion.identity);
                m_objectiveDuration.transform.parent = m_InteractableCanvas.transform;
                hasSpawnedUI = true;
            }
            else
            {
                //Keep the slider bar locked to above the objective
                UpdateUIElements();
            }

            currObjTime += Time.deltaTime;

            //make sure slider has been created
            if (m_objectiveDuration != null)
            {
                //normalise timer to fit in slider
                m_normalizedObjTime = currObjTime / objectiveTime;
                m_objectiveDuration.value = m_normalizedObjTime;
            }

            if (currObjTime >= objectiveTime)
            {
                m_isNeedReset = true;
            }
        }

        //reset state
        if (m_isNeedReset)
        {
            //finish objective state
            GameObject.Destroy(m_objectiveDuration.gameObject);
            hasSpawnedUI = false;
            m_isDoingTask = false;
            m_isInTrigger = false;
            currObjTime = 0.0f;
            player.SetLockMovement(false);
            m_isNeedReset = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if the player is inside the trigger and is pressing the interact key
        if (other.tag == "Player")
        {
            m_isInTrigger = true;
        }
        else
        {
            m_isInTrigger = false;
        }
    }

    void UpdateUIElements()
    {
        //get the position in world space that we want the ui to be displayed on
        float UIOffset = this.transform.position.y + 0.5f;
        Vector3 offsetPos = new Vector3(this.transform.position.x, UIOffset, this.transform.position.z);

        Vector2 canvasPos;
        //convert the world space position to screen space
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

        //Convert screen spave to Canvas space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvasRect, screenPoint, null, out canvasPos);

        m_objectiveDuration.gameObject.transform.localPosition = canvasPos;
    }
}
