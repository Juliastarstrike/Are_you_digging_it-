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
    public float playerWon = 0;

    //timer
    float decimalTimer = 15;
    int timer;
    public TextMeshProUGUI status;

    //seaRisning and camera to move to 
    public float Followspeed = 30f;
    public float velocity = 0;
    public Transform lose_target;
    public Transform win_target;
    public Transform seaRisning_pos;
    public Transform camera1;
    
    //SetAtive(false)
    public GameObject player;
    public GameObject fixedJoystick;
    private Vector2 target;

    private void Start() 
    {
        playerWon = 0;
    }
    void Update()
    {
        //timer countdown
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

            //SceneManager.LoadScene("Scoreboard");
        }
        
        Score_manager score_manager = canvas.GetComponent<Score_manager>();

        //if you win
        if (timer <= 0 && score_manager.score > blockDestroydToWin)
        {
            WinLoseState();
            playerWon = 1;
            //float step = Followspeed;
            //move sea
            //Vector3 newPos_seaL = new Vector3(win_target.position.x, win_target.position.y, 0);
            //float newpos_sea2L = Mathf.SmoothDamp(seaRisning_pos.position.y , newPos_seaL.y, ref velocity,20);
            //seaRisning_pos.position = Vector2.MoveTowards(seaRisning_pos.position, win_target.position, step);
            //camera1.position = Vector2.MoveTowards(seaRisning_pos.position, win_target.position, step);
            //seaRisning_pos.position = new Vector2(seaRisning_pos.position.x, newpos_sea2L);
        }
        //if you lose
        else if(timer <= 0 && score_manager.score < blockDestroydToWin)
        {
            WinLoseState();
            playerWon = 2;
            //float step = Followspeed ;
            //float lerpDuration = 10;
            //float timeElapsed=0;
            //float time =10;
            
            //move sea
            /* Vector2 newPos1 = new Vector2(lose_target.position.x, lose_target.position.y);
            float newpos2 = Mathf.SmoothDamp(seaRisning_pos.position.y , newPos1.y, ref velocity,20);
            seaRisning_pos.position = new Vector2(seaRisning_pos.position.x, newpos2); */
            //target = new Vector2(100.0f, 100.0f);
            //if(timeElapsed < lerpDuration)
            //{
                //valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
                //seaRisning_pos.position = Vector3.Lerp(seaRisning_pos.position, lose_target.position, time);
                //timeElapsed += Time.deltaTime;
            //}
           
            //seaRisning_pos.position = Vector3.MoveTowards(seaRisning_pos.position, lose_target.position, step * Time.deltaTime);
            //seaRisning_pos.position = Vector2.MoveTowards(seaRisning_pos.position, target, step);
            //camera1.position = Vector3.MoveTowards(seaRisning_pos.position, lose_target.position , step);
            //camera1.position = Vector3.MoveTowards(seaRisning_pos.position, new Vector3(lose_target.position.x , lose_target.position.y, lose_target.position.z-1) , step);
        }
    }
        void WinLoseState()
        {
            player.SetActive(false);
            fixedJoystick.SetActive(false);
            GameObject.Find("Main Camera").GetComponent<Camera_follow_player>().enabled = false;
        }
}
