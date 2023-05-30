using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

[Serializable]
public class UserInfo
{
    public string name;
    public int victories;
}
public class SetUpMenu : MonoBehaviour
{
  //Lots of UI setup references.
    public TMP_InputField playerName;
    public TMP_InputField score;
    public Button playbutton;


    public TextMeshProUGUI NeighbourAnswere;
    public GameObject enable_NeighbourAnswere;
    public GameObject disable_saveName;
    public GameObject enable_Playbutton;
    public GameObject canvas;

    public string test;

    void Start()
    {
        //Setup listener for when the user makes changes.
        playbutton.onClick.AddListener(ButtonClick);

        //Load saved data if we have any, call UserLoaded.
        FirebaseSaveManager.Instance.LoadData<UserInfo>("users/" + SignIn.Instance.GetUserID, UserLoaded);
    }

	private void UserLoaded(UserInfo loadedUser)
	{
        //Update our UI based on loaded data.
        playerName.text = loadedUser.name;
    }

    //When we click play, create user info and save all the data and send it to firebase.
	public void ButtonClick()
	{
        var user = new UserInfo();
        user.name = playerName.text;
        //user.victories = int.Parse(score.text);

        string jsonString = JsonUtility.ToJson(user);
        string path = "users/" + SignIn.Instance.GetUserID;
        FirebaseSaveManager.Instance.SaveData(path, jsonString);

        enable_NeighbourAnswere.SetActive(true);
        NeighbourAnswere.text = "Hay!, " + user.name + " The water is is rising you need to save our village dig away so mutch dirt as you can at least 1000 meter , but be carful not to drown, hurry hurry HURRY!!!";
        disable_saveName.SetActive(false);
        enable_Playbutton.SetActive(true);
	}

    public void PlayButtonClick()
	{
        var user = new UserInfo();
        user.name = playerName.text;
        //user.victories = int.Parse(score.text);

        string jsonString = JsonUtility.ToJson(user);
        string path = "users/" + SignIn.Instance.GetUserID;
        FirebaseSaveManager.Instance.SaveData(path, jsonString);
        SceneManager.LoadScene("Game");
	}
    public void ButtonClick_Score()
	{
        var user = new UserInfo();
        Score_manager score_manager = canvas.GetComponent<Score_manager>();

        string jsonString = score_manager.score.ToString();

        string path = "users/" + SignIn.Instance.GetUserID + "/victories";
        FirebaseSaveManager.Instance.SaveData(path, jsonString);
	}
}