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
    public float Followspeed = 0.01f;
    public float velocity = 0;
    public Transform lose_target;
    public Transform win_target;
    public Transform seaRisning_pos;
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

            //save how many point the player got
            var user = new UserInfo();
            Score_manager score_manager1 = canvas.GetComponent<Score_manager>();
            string jsonString = score_manager1.score.ToString();
            string path = "users/" + SignIn.Instance.GetUserID + "/victories";
            FirebaseSaveManager.Instance.SaveData(path, jsonString);

            SceneManager.LoadScene("Scoreboard");
        }
        
        Score_manager score_manager = canvas.GetComponent<Score_manager>();

        //if you win
        if (timer <= 0 && score_manager.score > blockDestroydToWin)
        {
            playerWon = true;
            WinLoseState();
            //move sea
            //Vector3 newPos_seaL = new Vector3(win_target.position.x, win_target.position.y, 0);
            //float newpos_sea2L = Mathf.SmoothDamp(seaRisning_pos.position.y , newPos_seaL.y, ref velocity,20);
            seaRisning_pos.position = Vector3.MoveTowards(seaRisning_pos.position, win_target.position, Followspeed);
            camera1.position = Vector3.MoveTowards(seaRisning_pos.position, win_target.position, Followspeed);
            //seaRisning_pos.position = new Vector2(seaRisning_pos.position.x, newpos_sea2L);
        }
        //if you lose
        else if(timer <= 0 && score_manager.score < blockDestroydToWin)
        {
            WinLoseState();
            //move sea
            /* Vector2 newPos1 = new Vector2(lose_target.position.x, lose_target.position.y);
            float newpos2 = Mathf.SmoothDamp(seaRisning_pos.position.y , newPos1.y, ref velocity,20);
            seaRisning_pos.position = new Vector2(seaRisning_pos.position.x, newpos2); */

            seaRisning_pos.position = Vector3.MoveTowards(seaRisning_pos.position, lose_target.position, Followspeed);
            camera1.position = Vector3.MoveTowards(seaRisning_pos.position, lose_target.position, Followspeed);
        }
    }
        void WinLoseState()
        {
            player.SetActive(false);
            fixedJoystick.SetActive(false);
            GameObject.Find("Main Camera").GetComponent<camera_follow_player>().enabled = false;
        }
}
