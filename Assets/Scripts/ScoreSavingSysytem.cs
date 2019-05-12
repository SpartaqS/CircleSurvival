using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class ScoreSavingSysytem
{
    static string savePath = Application.persistentDataPath + "/highscore.data";

    public static void SaveHighscore(float time)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream saveStream = new FileStream(savePath, FileMode.Create);

        binaryFormatter.Serialize(saveStream, time);

        saveStream.Close();
    }

    public static float ReadHighscore()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        try
        {
            FileStream readStreamTest = new FileStream(savePath, FileMode.Open);
            readStreamTest.Close();
        }
        catch (FileNotFoundException)
        {
            FileStream emergencyStream = new FileStream(savePath, FileMode.Create);
            float startingHighscore = -1f;
            binaryFormatter.Serialize(emergencyStream, startingHighscore);
            emergencyStream.Close();
        }

        FileStream readStream = new FileStream(savePath, FileMode.Open);
        float loadedHighscore = (float)binaryFormatter.Deserialize(readStream);
        readStream.Close();
        return loadedHighscore;
    }
}
