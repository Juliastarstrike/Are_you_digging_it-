using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

    [Serializable] public class PlayerSaveData : MonoBehaviour
{
    public string Name;
    public float ColorHUE;
    public bool Hidden;
    public Vector3 Position;
}
public class Save_And_Load_Script : MonoBehaviour
{
    // Start is called before the first frame update

    public Score_manager score;
    public GameObject canvas;
    
    void Start()
    {
        //blocks = GetComponent<"Score_manager">;
        
        Score_manager score_manager = canvas.GetComponent<Score_manager>();
        score_manager.score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Save()
    {
        PlayerSaveData saveData = new PlayerSaveData();
        GameObject canvas = GameObject.Find("Canvas");
        Score_manager score_manager = canvas.GetComponent<Score_manager>();
        //PlayerPrefs.SetInt("Destroyd_blocks", score_manager.score);
    
        //Create our saveData object
        //Put our data in our object
        //saveData.Score = 0;
        saveData.Name = "Julia";
        saveData.ColorHUE = 0.5f;
        saveData.Hidden = true;
        saveData.Position = transform.position;

        //Convert saveData object to JSON
        string jsonString = JsonUtility.ToJson(saveData);

        //For now just save it using PlayerPrefs
        //PlayerPrefs.SetString("PlayerSaveData", jsonString);
        SaveToFile("Are_you_digging_it_database.json",jsonString);
    }
    //Return the content of the file in a string

    public void SaveToFile(string fileName, string jsonString)
{
    // Open a file in write mode. This will create the file if it's missing.
    // It is assumed that the path already exists.
    using (var stream = File.OpenWrite(fileName))
    {
        // Truncate the file if it exists (we want to overwrite the file)
        stream.SetLength(0);

        // Convert the string into bytes. Assume that the character-encoding is UTF8.
        // Do you not know what encoding you have? Then you have UTF-8
        var bytes = Encoding.UTF8.GetBytes(jsonString);

        // Write the bytes to the hard-drive
        stream.Write(bytes, 0, bytes.Length);

        // The "using" statement will automatically close the stream after we leave
        // the scope - this is VERY important
    }
}
public string Load(string fileName)
{
    // Open a stream for the supplied file name as a text file
    using (var stream = File.OpenText(fileName))
    {
        // Read the entire file and return the result. This assumes that we've written the
        // file in UTF-8
        Debug.Log("kommer vi in hi?");
        return stream.ReadToEnd();
    }
}

public void Load()
{

}
}