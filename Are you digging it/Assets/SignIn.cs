using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine.SceneManagement;

public class SignIn : MonoBehaviour
{
    //Singleton variables
    private static SignIn _instance;
    public static SignIn Instance { get { return _instance; } }
    
    //Login from connections
    public TMP_InputField email;
    public TMP_InputField password;
    public TextMeshProUGUI status;

    public Button playButton;

    FirebaseAuth auth;
    public string GetUserID { get { return auth.CurrentUser.UserId; } }

    private void Awake()    
    {
        //Singelton setup
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        //start firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
            }

            auth = FirebaseAuth.DefaultInstance;

            //Check if user are logged in
            if (auth.CurrentUser == null)
            {
                //if not sign in anonymousely
                AnonymouseSignIn();
            }
            else
            {
                //User is logged in
                //the program will remember user
                Debug.Log(auth.CurrentUser.Email + " is logged in.");
                UserIsSignedIn_LoadGame();
            }
        });

    }

    private void UserIsSignedIn_LoadGame()
    {
        playButton.interactable = true;
        SceneManager.LoadScene("Cutscene");
    }

    private void AnonymouseSignIn()
    {
         auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task => {
            if (task.Exception != null)
            {
                Debug.LogError(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User Registerd: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);

                playButton.interactable = true;
            }
        });
    }

    public void SignInputButton()
    {
        SignInFirebase(email.text, password.text);
    }
    
    private void SignInFirebase(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
                status.text = newUser.Email + "is sign in";

                SceneManager.LoadScene("Cutscene");
            }
        });
    }
    public void RegisterButton()
    {
        RegisterNewUser(email.text, password.text);
    }

    private void RegisterNewUser(string email, string password)
    {
        Debug.Log("Starting Registration");
        status.text = "Starting Registration";
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.Log("are we in here?");
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User Registerd: {0} ({1})",
                newUser.DisplayName, newUser.UserId);

                playButton.interactable = true;
            }
        });
    }

    public void DebugLogIn(int number)
    {
        SignInFirebase("test" + number + "@test.test", "password");
    }
}