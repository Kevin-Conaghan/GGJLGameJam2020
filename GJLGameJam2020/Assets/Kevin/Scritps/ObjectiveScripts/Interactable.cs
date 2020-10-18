using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interactable : MonoBehaviour
{
    //time taken to complete objective
    public string objectiveName;
    public float objectiveTime;
    public int objectiveScore;

    private float m_currObjTime;
    private float m_normalizedObjTime;

    protected bool m_isInTrigger;
    protected bool m_isDoingTask;
    protected bool m_isObjectiveCompleted;

    //UI Variables
    public Slider objectiveDurTemplate;

    private Slider m_objectiveDurationSlider;
    private Canvas m_InteractableCanvas;
    private RectTransform m_canvasRect;
    private bool hasSpawnedUI;

    protected Character_Movement playerMovement;
    protected GameObject playerObject;
    protected Game_Manager currGameManager;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //find the player object so we can reference the movement script and control locking/unlocking of movement
        GameObject tempObject = GameObject.FindGameObjectWithTag("Player");
        playerMovement = tempObject.GetComponent<Character_Movement>();

        tempObject = GameObject.FindGameObjectWithTag("GameManager");
        currGameManager = tempObject.GetComponent<Game_Manager>();

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
    protected virtual void Update()
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
            if (!playerMovement.GetLockMovment())
            {
                //lock the players movement while interacting
                playerMovement.SetLockMovement(true);
            }
            //carry out objective
            UpdateObjectiveTask(objectiveTime);
           
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //check if the player is inside the trigger and is pressing the interact key
        if (other.gameObject.tag == "Player")
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

    protected void UpdateUIElements()
    {
        if (!m_objectiveDurationSlider.IsDestroyed())
        {
            //get the position in world space that we want the ui to be displayed on
            float UIOffset = this.transform.position.y + 2f;
            Vector3 offsetPos = new Vector3(this.transform.position.x, UIOffset, this.transform.position.z);

            Vector2 canvasPos;
            //convert the world space position to screen space
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(offsetPos);

            //Convert screen spave to Canvas space
            RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvasRect, screenPoint, null, out canvasPos);

            m_objectiveDurationSlider.gameObject.transform.localPosition = canvasPos;
        }
    }

    protected void UpdateObjectiveTask(float objectiveMaxTime)
    {
        if (!hasSpawnedUI)
        {
            //Spawn UI Elements
            m_objectiveDurationSlider = Instantiate(objectiveDurTemplate, m_InteractableCanvas.transform.position, Quaternion.identity);
            m_objectiveDurationSlider.transform.parent = m_InteractableCanvas.transform;
            hasSpawnedUI = true;
        }
        else
        {
            //Keep the slider bar locked to above the objective
            UpdateUIElements();
        }

        //increment objective timer
        m_currObjTime += Time.deltaTime;

        //make sure slider has been created
        if (m_objectiveDurationSlider != null)
        {
            //normalise timer to fit in slider
            m_normalizedObjTime = m_currObjTime / objectiveTime;
            m_objectiveDurationSlider.value = m_normalizedObjTime;
        }

        if (m_currObjTime >= objectiveMaxTime)
        {
            m_isObjectiveCompleted = true;
            //send score to game manager and then destroy 
            playerMovement.SetLockMovement(false);
            if (!m_objectiveDurationSlider.IsDestroyed())
            {
                GameObject.Destroy(m_objectiveDurationSlider.gameObject);
            }
            SendObjectiveScore(objectiveScore);
            DestroyMePls();
        }
    }

    protected void SendObjectiveScore(int objectiveScore)
    {
        currGameManager.SetPlayerScore(objectiveScore);
    }

    protected void DestroyMePls()
    {
        currGameManager.DestroyInteractable(this.gameObject);
    }

}
