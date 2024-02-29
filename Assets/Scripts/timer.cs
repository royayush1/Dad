using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class timer : MonoBehaviour
{

    public float timeRemaining = 0;
    public bool timeIsRuning = false;
    public TMP_Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        timeIsRuning = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeIsRuning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                // Once timeRemaining hits 0 or goes below, stop the timer and fix the display to 00:00
                timeRemaining = 0;
                DisplayTime(0); // Ensure the timer displays 00:00
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
