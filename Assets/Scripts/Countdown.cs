using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class countdown : MonoBehaviour
{

    private float timeLimit = 8;
    public bool timerIsRunning = false;
    public TMP_Text timeText;
    public TMP_Text gameOver;

    void Start()
    {
        timeLimit *= 60;
        timerIsRunning = true;

    }


    void Update()
    {
        if (timerIsRunning)
        {

            if (timeLimit > 0)
            {

                timeLimit -= Time.deltaTime;
                DisplayTime(timeLimit);

                if (timeLimit == (timeLimit / 3))
                {
                    //new Appear1();

                }
                if (timeLimit == (timeLimit * 0.6))
                {
                    //cue second target appears scene 

                }
                if (timeLimit == (timeLimit - 1))
                {
                    //cue last target appears scene

                }
            }
            else
            {
                //replace with gameover screen later

                gameOver.text = string.Format("Bed Time");
                Debug.Log("Bed Time - Game Over");
                timeLimit = 0;
                timerIsRunning = false;
                ExitGame();
            }

        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void ExitGame()
    {
        Application.Quit();
    }
}

