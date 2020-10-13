using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver_Scene : MonoBehaviour
{

    public Button restartButton;
    public Text scoreText;
    private int score;


    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("Score");
        scoreText.text = score.ToString();
        Button btn = restartButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("MainLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
