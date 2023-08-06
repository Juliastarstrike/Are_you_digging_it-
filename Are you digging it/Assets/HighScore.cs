using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class HighScore : MonoBehaviour
{
    public GameObject highScorePrefab;
	public int numberOfScores = 10;

    // Start is called before the first frame update
    void Start()
	{
		FirebaseSaveManager.Instance.LoadHighScoreData<UserInfo>("users", numberOfScores, LoadedAllUsers);
	}

	private void LoadedAllUsers(List<UserInfo> users)
	{
		//We get the list in the wrong order.
		users.Reverse();

		//Create our high score list from our data
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
