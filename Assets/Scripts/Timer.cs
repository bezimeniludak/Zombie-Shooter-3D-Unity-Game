using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool _isRunning=false;
    public float _time=300f;
    public float _timeRemaining;
    public Text _timeText;
    private void Start()
    {
        _timeRemaining = _time;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isRunning)
        {
            DisplayTime(_timeRemaining);
            if (_timeRemaining > 0)
            {
                _timeRemaining -= Time.deltaTime;
            }
            else
            {
                _isRunning = false;
                _timeRemaining = 0;                
            }
        }
    }

    public void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        _timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
