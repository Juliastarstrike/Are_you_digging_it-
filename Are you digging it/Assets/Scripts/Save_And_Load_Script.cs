using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Save_And_Load_Script : MonoBehaviour
{
    [Serializable]
    class PlayerSaveData
    {
        public string Name;
        public float ColorHUE;
        public bool Hidden;
        public Vector3 Position;
    }
    //____________________________________________________________________________________
    public Score_manager score;
    public GameObject canvas;
    
    
    void Start()
    {
        //blocks = GetComponent<"Score_manager">;
        GameObject canvas = GameObject.Find("Canvas");
        Score_manager score_manager = canvas.GetComponent<Score_manager>();
        score_manager.score = 0;
    }
    public void Save()
    {
        GameObject canvas = GameObject.Find("Canvas");
        Score_manager score_manager = canvas.GetComponent<Score_manager>();
        
        PlayerPrefs.SetInt("Destroyd_blocks", score_manager.score);
        
    }
    public void Load()
    {

        GameObject canvas = GameObject.Find("Canvas");
        Score_manager score_manager = canvas.GetComponent<Score_manager>();
        
        score_manager.score = PlayerPrefs.GetInt("Destroyd_blocks");
    }

}
