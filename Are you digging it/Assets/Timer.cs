using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Timer : MonoBehaviour
{
    //Win or lose
    public GameObject canvas;
    public float blockDestroydToWin = 1000;
    public bool playerWon = false;

    //timer
    float decimalTimer = 15;
    int timer;
    public TextMeshProUGUI status;

    //seaRisning aned camera to move to 
    public float Followspeed = 0.1f;
    public float velocity = 0;
    public float yOffset =0f;
    public Transform lose_target;
    public Transform win_target;
    public Transform seaRisning;
    public Transform camera1;
    
    //SetAtive(false)
    public GameObject player;
    public GameObject fixedJoystick;
    

    void Update()
    {
        if (timer >= 0 )
        {
        //round of Timer to Int
        decimalTimer -= 1 * Time.deltaTime;
        timer = Mathf.RoundToInt(decimalTimer);
        status.text = timer + " s";
        }
        else if (timer < 0)
        {
            status.text ="0 s";
            SceneManager.LoadScene("Scoreboard");
            Debug.Log("LoadScene(Scoreboard);");
        }
        
        Score_manager score_manager = canvas.GetComponent<Score_manager>();

        //if you win
        if (timer <= 0 && score_manager.score > blockDestroydToWin)
        {
            playerWon = true;
            WinLoseState();
            //move sea
            Vector2 newPos_seaL = new Vector2(win_target.position.x, win_target.position.y + yOffset);
            float newpos_sea2L = Mathf.SmoothDamp(seaRisning.position.y , newPos_seaL.y, ref velocity,20);
            seaRisning.position = new Vector2(seaRisning.position.x, newpos_sea2L);

        }
        //if you lose
        else if(timer <= 0 && score_manager.score < blockDestroydToWin)
        {
            WinLoseState();
            //move sea
            Vector2 newPos1 = new Vector2(lose_target.position.x, lose_target.position.y + yOffset);
            float newpos2 = Mathf.SmoothDamp(seaRisning.position.y , newPos1.y, ref velocity,20);
            seaRisning.position = new Vector2(seaRisning.position.x, newpos2);

        }
    }
        void WinLoseState()
        {
            player.SetActive(false);
            fixedJoystick.SetActive(false);
            GameObject.Find("Main Camera").GetComponent<Camera_follow_player>().enabled = false;
        }
}
