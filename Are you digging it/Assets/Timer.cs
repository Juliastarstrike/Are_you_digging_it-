using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    int timer;
    public float blockDestroydToWin = 1000;
    float decimalTimer = 30;
    public bool playerWon = false;
    public GameObject canvas;
    public GameObject player;
    public TextMeshProUGUI status;

    public float Followspeed = 0.1f;
    public float velocity = 0;
    public float yOffset =0f;
    public Transform lose_target;
    public Transform win_target;

    public Transform seaRisning;
    

    void Update()
    {
        //round of Timer to Int
        decimalTimer -= 1 * Time.deltaTime;
        timer = Mathf.RoundToInt(decimalTimer);
        status.text = timer + " s";
        
        Score_manager score_manager = canvas.GetComponent<Score_manager>();

        //if you win
        if (timer <= 0 && score_manager.score > blockDestroydToWin)
        {
            playerWon = true;
            WinLoseState();

            Debug.Log("you win");
            Vector2 newPos1 = new Vector2(win_target.position.x, win_target.position.y + yOffset);
            float newpos2 = Mathf.SmoothDamp(seaRisning.position.y , newPos1.y, ref velocity,20);
            seaRisning.position = new Vector2(seaRisning.position.x, newpos2);
        }
        //if you lose
        else if(timer <= 0 && score_manager.score < blockDestroydToWin)
        {
            Debug.Log("you lose");
            WinLoseState();
            Vector2 newPos1 = new Vector2(lose_target.position.x, lose_target.position.y + yOffset);
            float newpos2 = Mathf.SmoothDamp(seaRisning.position.y , newPos1.y, ref velocity,20);
            seaRisning.position = new Vector2(seaRisning.position.x, newpos2);
        }
    }
        void WinLoseState()
        {
            player.SetActive(false);

            //kameran sätts i mitten
            //kanske något ljud av späning
        }
}
