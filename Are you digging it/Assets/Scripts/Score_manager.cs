using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score_manager : MonoBehaviour
{
    //description: this script updates the score text to how many scores the player has.
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
}
