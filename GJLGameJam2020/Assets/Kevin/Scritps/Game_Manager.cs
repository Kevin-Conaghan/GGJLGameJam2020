using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject[] m_interactablesSpawned;

    // Start is called before the first frame update
    void Start()
    {
        m_gameState = GameState.Start;
        m_gameTimer = timeLimit;

        m_interactablesSpawned = GameObject.FindGameObjectsWithTag("Interactable");
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
                break;
            case GameState.GameOver:
                break;
            case GameState.Outro:
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
        Destroy(currObj);
    }
}
