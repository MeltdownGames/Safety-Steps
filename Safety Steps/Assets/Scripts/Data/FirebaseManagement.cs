using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseManagement : MonoBehaviour
{
    public static FirebaseManagement Instance { get; private set; }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
