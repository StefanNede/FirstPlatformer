﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    public Text scoreText;

    int score = 0;

    private void Awake()
    {
        // at the very start of the game we are creating an instance ofo the score manager that can be 
        // accessed in other classes
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddPoint()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }
}
