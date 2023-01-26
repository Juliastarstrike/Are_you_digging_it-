using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_manager : MonoBehaviour
{
    public static Score_manager instance;
    public TextMeshProUGUI scoreText;

    public int score = 0;
    private void Awake() 
    {
        instance = this;
    }
    void Start()
    {
       scoreText.text = "Blocks destroyed: " + score.ToString();
       
    }

    public void addPoint()
    {
        score += 1;
        scoreText.text = "Blocks destroyed: " + score.ToString();
    }

    void Update()
    {
        
    }
}
