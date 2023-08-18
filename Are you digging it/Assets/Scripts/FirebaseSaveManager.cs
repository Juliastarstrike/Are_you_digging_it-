using Firebase.Database;
using Firebase.Extensions;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseSaveManager : MonoBehaviour
{
    //Singleton variables
    private static FirebaseSaveManager _instance;
    public static FirebaseSaveManager Instance { get { return _instance; } }

    //Function that gets called after load or save
    public delegate void OnLoadedDelegate(DataSnapshot snapshot); //when we want the json stuff
    public delegate void OnLoadedDelegate<T>(T data); //When we want an object directly
    public delegate void OnLoadedMultipleDelegate<T>(List<T> data); //list of objects
    public delegate void OnSaveDelegate();

    FirebaseDatabase db;
    private void Awake()
    {
        //Singleton setup
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        db = FirebaseDatabase.DefaultInstance;
        db.SetPersistenceEnabled(false); //Fix data cache problems
    }

    //Returns one object that I can load from the database
    public void LoadData<T>(string path, OnLoadedDelegate<T> onLoadedDelegate)
    {
        db.RootReference.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            onLoadedDelegate(JsonUtility.FromJson<T>(task.Result.GetRawJsonValue()));
		});
    }

    //Returns one list of objects that I can load from the database
    public void LoadMultipleData<T>(string path, OnLoadedMultipleDelegate<T> onLoadedDelegate)
	{
		db.RootReference.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
		{
			if (task.Exception != null)
				Debug.LogWarning(task.Exception);

			var dataStuff = new List<T>();

			foreach (var item in task.Result.Children)
				dataStuff.Add(JsonUtility.FromJson<T>(item.GetRawJsonValue()));

			onLoadedDelegate(dataStuff);
		});
	}

	//loads the data at "path" then returns json result to the delegate/callback function
	public void LoadHighScoreData<T>(string path, int amount, OnLoadedMultipleDelegate<T> onLoadedDelegate)
    {
        db.RootReference.Child(path).OrderByChild("victories").LimitToLast(amount).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            var dataStuff = new List<T>();

            foreach (var item in task.Result.Children)
                dataStuff.Add(JsonUtility.FromJson<T>(item.GetRawJsonValue()));

            onLoadedDelegate(dataStuff);
        });
    }

    //Save the data at the given path, save callback optional
    public void SaveData(string path, string data, OnSaveDelegate onSaveDelegate = null)
    {
        db.RootReference.Child(path).SetRawJsonValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            //Call delegate if it's not null
            onSaveDelegate?.Invoke();
        });
    }

    //Save the data at the given path, save callback optional
    public void PushData(string path, string data, OnSaveDelegate onSaveDelegate = null)
    {
        db.RootReference.Child(path).Push().SetRawJsonValueAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

            //Call delegate if it's not null
            onSaveDelegate?.Invoke();
        });
    }

    //Used to remove multiple data from the database based on their age.
    public void RemoveOldestData(string path, int amount)
	{
        //First load the data
        db.RootReference.Child(path).GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
                Debug.LogWarning(task.Exception);

			if (task.Result.ChildrenCount < amount)
			{
                amount = (int)task.Result.ChildrenCount;
            }

            //Call remove on each of the data, that I want gone.
            int i = 0;
            foreach (var item in task.Result.Children)
			{
                i++;
                RemoveData(path + "/" + item.Key);

				if (i >= amount)
				{
                    break;
				} 
            } 
        });
    }
    public void RemoveData(string path)
	{
        db.RootReference.Child(path).RemoveValueAsync();
	}
}