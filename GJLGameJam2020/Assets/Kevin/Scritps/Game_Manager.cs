using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start,
    Playing,
    GameOver,
    Outro
}

public class Game_Manager : MonoBehaviour
{
    //timer variables
    public int timeLimit;
    private float m_gameTimer;
    private int m_displayTimer;
    public Text timerText;

    public int playerScore;

    private bool m_isInitialized;

    public GameState m_gameState;

    public List<GameObject> m_interactablesSpawned;
    private List<Text> m_objectiveTextList;
    public Text objectiveList;
    public GameObject objectivePanel;
    private int indentVal = -50;

    private Vector3 m_objTextTransform;

    // Start is called before the first frame update
    void Start()
    {
        m_gameState = GameState.Playing;
        m_gameTimer = timeLimit;

        m_objectiveTextList = new List<Text>();
        m_interactablesSpawned = new List<GameObject>();

        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("Interactable");

        for (int i = 0; i < tempArray.Length; i++)
        {
            m_interactablesSpawned.Add(tempArray[i]);
        }

        for (int i = 0; i < m_interactablesSpawned.Count; i++)
        {
            Text tempText = Instantiate(objectiveList, objectivePanel.transform.position + new Vector3(0.0f, -indentVal), Quaternion.identity, objectivePanel.transform);
            m_objectiveTextList.Add(tempText);
            //grab the objective name
            string tempstr = m_interactablesSpawned[i].GetComponent<Interactable>().objectiveName;
            //concat the number with the objective name
            tempText.text += tempstr + "\n";
            indentVal += 20;


        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_gameState)
        {
            case GameState.Start:
                //input menu functionality here
                m_gameState = GameState.Playing;
                break;
            case GameState.Playing:
                //Add gameplay here
                UpdateTimer();

                if (m_gameTimer <= 0)
                {
                    //Save the player score for displaying in the game over scene
                    PlayerPrefs.SetInt("Score", playerScore);
                    //load the game over scene
                    SceneManager.LoadScene("GameOver");
                }

                break;
        };
    }

    void UpdateTimer()
    {
        //decrement timer
        m_gameTimer -= Time.deltaTime;
        m_displayTimer = (int)m_gameTimer;

        //set timer text
        timerText.text = m_displayTimer.ToString();
    }

    public void SetPlayerScore(int score)
    {
        playerScore += score;
    }

    public void DestroyInteractable(GameObject currObj)
    {
        DestroyUIObjective(currObj);
        Destroy(currObj);
    }

    void DestroyUIObjective(GameObject currObj)
    {
        bool hasBeenDeleted = false;    

        for (int i = 0; i < m_objectiveTextList.Count; i++)
        {
            if (currObj == m_interactablesSpawned[i])
            {
                //store the transform of the text before we destroy it
                m_objTextTransform = m_objectiveTextList[i].transform.position;

                //destroy the text
                GameObject tempObject = m_objectiveTextList[i].gameObject;
                Destroy(tempObject);
                //remove the reference for both the game object and the corresponding text
                m_objectiveTextList.Remove(m_objectiveTextList[i]);
                m_interactablesSpawned.Remove(m_interactablesSpawned[i]);

                hasBeenDeleted = true;
                
            }

            if (hasBeenDeleted && i != m_objectiveTextList.Count)
            {
                //shuffle the list up a position
                Vector3 tempPosition = m_objectiveTextList[i].transform.position;
                m_objectiveTextList[i].transform.position = m_objTextTransform;
                m_objTextTransform = tempPosition;  
            }
        }
    }
}
