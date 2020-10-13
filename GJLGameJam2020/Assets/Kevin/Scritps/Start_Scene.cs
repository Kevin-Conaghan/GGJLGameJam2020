using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Start_Scene : MonoBehaviour
{
    public Button startButton;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        //load the scene here
        SceneManager.LoadScene("MainLevel");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
