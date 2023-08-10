using TMPro;
using UnityEngine;

//Script in the login scene that has UI references and just passes data to the singleton
public class SignInMenu : MonoBehaviour
{
    //Login form connections
    public TMP_InputField email;
    public TMP_InputField password;

    //Our buttons in connected to this function.
    public void SignInButton()
    {
        SignIn.Instance.SignInFirebase(email.text, password.text);
    }

    public void RegisterButton()
    {
        SignIn.Instance.RegisterNewUser(email.text, password.text);
    }

    /* //Called from our debug buttons to quickly log in as some default accounts that we can create.
    public void DebugLogIn(int number)
    {
        SignIn.Instance.SignInFirebase("test" + number + "@test.test", "password");
    } */
}
