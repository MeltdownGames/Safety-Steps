using EncryptionDecryptionUsingSymmetricKey;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class PlayerData
{
    public static int playerID { get; private set; }
    public static string playerName;

    public static float highscore;

    public static bool SaveScore(float score)
    {
        string fullPath = Path.Combine(Application.persistentDataPath, "score.ss");
        try
        {
            bool isScoreHigher = score > highscore;
            float newHighscore = isScoreHigher ? score : highscore;

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = AesOperation.EncryptString(newHighscore.ToString());

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }

            if (isScoreHigher)
                return true;
            else
                return false;
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
            return false;
        }
    }

    public static void LoadScore()
    {
        string path = Application.persistentDataPath + "/score.ss";
        if (File.Exists(path))
        {
            try
            {
                string dataToLoad_ = "";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad_ = reader.ReadToEnd();
                    }
                }

                string score = AesOperation.DecryptString(dataToLoad_);

                highscore = float.Parse(score);
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + path + "\n" + e);
            }
        }
    }

    public static void SavePlayerData()
    {
        string fullPath = Path.Combine(Application.persistentDataPath, "player.ss");
        try
        {
            Dictionary<string, object> playerData = new Dictionary<string, object>();
            playerData.Add("id", playerID);
            playerData.Add("name", playerName);

            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = AesOperation.EncryptString(JsonConvert.SerializeObject(playerData));

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    public static void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/player.ss";
        if (File.Exists(path))
        {
            try
            {
                string dataToLoad_ = "";
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad_ = reader.ReadToEnd();
                    }
                }

                string json = AesOperation.DecryptString(dataToLoad_);

                Dictionary<string, object> playerData = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                playerID = (int)playerData["id"];
                playerName = (string)playerData["name"];
            }
            catch (Exception e)
            {
                Debug.LogError("Error occured when trying to load data from file: " + path + "\n" + e);
            }
        }
        else
        {
            // get new player ID and random name

            SavePlayerData();
        }
    }
}
