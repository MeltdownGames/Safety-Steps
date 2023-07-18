using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using System;
using System.Threading.Tasks;

public class FirebaseManagement : MonoBehaviour
{
    public static FirebaseManagement Instance { get; private set; }

    public static FirebaseApp app;
    public static FirebaseFirestore firestoreReference;
    public static FirebaseAuth firebaseAuth;

    public static bool loggedIn = false;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        AppOptions options = new AppOptions();
        options.ApiKey = "AIzaSyAaXq7VUpgPllB_Ks5VnRTw198jzPuPD74";
        options.AppId = "1:219056795761:android:9cb5a638c1595165c3ebd0";
        options.DatabaseUrl = null; //new System.Uri("https://tank-strike-e466c-default-rtdb.firebaseio.com/");
        options.ProjectId = "safety-steps";
        options.StorageBucket = "safety-steps.appspot.com";

        app = FirebaseApp.Create(options, Guid.NewGuid().ToString());
        firestoreReference = FirebaseFirestore.GetInstance(app);
        firebaseAuth = FirebaseAuth.GetAuth(app);

        SignIn();
    }

    async void SignIn()
    {
        Task<AuthResult> task = firebaseAuth.SignInAnonymouslyAsync();
        await task;
        loggedIn = true;
        PlayerData.LoadPlayerData();
    }

    public Dictionary<string, object> GetScores()
    {
        return new Dictionary<string, object>();
    }

    private void OnApplicationQuit()
    {
        firebaseAuth.CurrentUser.DeleteAsync();
    }
}
