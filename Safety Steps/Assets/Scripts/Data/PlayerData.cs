using EncryptionDecryptionUsingSymmetricKey;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class PlayerData
{
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
}
