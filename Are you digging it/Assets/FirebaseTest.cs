using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;

[Serializable]
public class SavePostion
{
    public Vector3 pos;
    public string name = "Linus";
}
public class FirebaseTest : MonoBehaviour
{
    FirebaseAuth auth;
    SavePostion savePostion;

    void Start()
    {
        savePostion = new SavePostion();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogError(task.Exception);

            auth = FirebaseAuth.DefaultInstance;

            if(auth.CurrentUser == null)
               AnonymousSignIn(); 
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("spara position");
            savePostion.pos = transform.position;

            string jsonstring = JsonUtility.ToJson(savePostion);

            DataTest(auth.CurrentUser.UserId, jsonstring);
        }
            
         if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadFromFirebase();
        }
    }

    private void AnonymousSignIn()
    {
        auth.SignInAnonymouslyAsync().ContinueWithOnMainThread(task => {
            if (task.Exception != null)
            {
                Debug.LogWarning(task.Exception);
            }
            else
            {
                FirebaseUser newUser = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                	newUser.DisplayName, newUser.UserId);
            }
        });
    }

    private void DataTest(string userID, string data)
    {
        Debug.Log("Trying to write data...");
        var db = FirebaseDatabase.DefaultInstance;
        db.RootReference.Child("users").Child(userID).SetRawJsonValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);
            else
                Debug.Log("DataTestWrite: Complete");
        });
    }
    private void LoadFromFirebase()
{
    var db = FirebaseDatabase.DefaultInstance;
    var userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
    db.RootReference.Child("users").Child(userId).GetValueAsync().ContinueWithOnMainThread(task =>
    {
        if (task.Exception != null)
        {
            Debug.LogError(task.Exception);
            Debug.Log("kom jag in här");
        }

        //here we get the result from our database.
        DataSnapshot snap = task.Result;

        //And send the json data to a function that can update our game.
        
        savePostion = JsonUtility.FromJson<SavePostion>(snap.GetRawJsonValue());
        transform.position = savePostion.pos;
        
    });
}
}