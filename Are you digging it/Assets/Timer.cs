using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    int timer;
    float decimalTimer = 10;
    public GameObject canvas;
    public GameObject player;
    public GameObject camera1;

    public bool playerWon = false;


    public TextMeshProUGUI status;
    
    void Start()
    {
    }

    void Update()
    {
        decimalTimer -= 1 * Time.deltaTime;
        timer = Mathf.RoundToInt(decimalTimer);

        status.text = timer + " s";
        

        //if you lose
        Score_manager score_manager = canvas.GetComponent<Score_manager>();
        if (timer <= 0 || score_manager.score < 1000)
        {
            playerWon = false;
            WinLoseState();
        }

        //if you win
        if (timer >= 0 && score_manager.score > 1000)
        {
            playerWon = true;
            WinLoseState();
        }
    }
        void WinLoseState()
        {
            player.SetActive(false);
        }
}
