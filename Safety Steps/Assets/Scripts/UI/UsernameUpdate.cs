using TMPro;
using UnityEngine;
using Firebase.Firestore;

public class UsernameUpdate : MonoBehaviour
{
    public async void UpdateUsername(TMP_InputField newUser)
    {
        PlayerData.playerName = newUser.text;
        PlayerData.SavePlayerData();
        newUser.text = string.Empty;
    }
}
