using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject.Asteroids;

public class GameTime : MonoBehaviour
{
    private Text _gameTime;
    private float _seconds;
    private int _minutes;
    private string _time;
    private void Start()
    {
        _gameTime = GetComponent<Text>();
        Debug.Log(_gameTime);
    }

    private void Update()
    {
        _seconds += Time.deltaTime;
        if ((int) _seconds == 60)
        {
            _minutes++;
            _seconds = 0;
        }

        _time = _minutes.ToString();
        _time += ":";
        _time += ((int) _seconds).ToString();
        _gameTime.text = _time;
    }
}
