using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class HighScore : MonoBehaviour
{
	//description: this script puts in users and their most resent score in a list in order of who got the highest score..
    public GameObject highScorePrefab;
	public int numberOfScores = 10;

    void Start()
	{
		FirebaseSaveManager.Instance.LoadHighScoreData<UserInfo>("users", numberOfScores, LoadedAllUsers);
	}
	private void LoadedAllUsers(List<UserInfo> users)
	{
		users.Reverse();

		//Create our high score list from the data
		foreach (var item in users)
		{
			var newHighScore = Instantiate(highScorePrefab, transform);
			newHighScore.transform.Find("Name").GetComponent<TextMeshProUGUI>().text = item.name;
			newHighScore.transform.Find("Score").GetComponent<TextMeshProUGUI>().text = item.victories.ToString();
		}
	}
    public void restart()
    {
        SceneManager.LoadScene("Cutscene");
    }

	public void quit()
    {
        Application.Quit();
    }
}
