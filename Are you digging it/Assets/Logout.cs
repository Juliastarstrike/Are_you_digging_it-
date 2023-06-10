using UnityEngine;
using UnityEngine.UI;

//Script that will log out the current user
public class Logout : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClicked);
    }

	private void ButtonClicked()
	{
        //Call the firebase singleton and let it handle logout.
        SignIn.Instance.SignOut();
	}
}
