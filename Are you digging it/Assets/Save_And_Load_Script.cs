using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Save_And_Load_Script : MonoBehaviour
{
    // Start is called before the first frame update

    public Score_manager score;
    public GameObject canvas;
    
    void Start()
    {
        //blocks = GetComponent<"Score_manager">;
        GameObject canvas = GameObject.Find("Canvas");
        Score_manager score_manager = canvas.GetComponent<Score_manager>();
        score_manager.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
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