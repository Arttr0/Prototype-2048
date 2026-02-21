using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem
{
    private static string path = Application.persistentDataPath + "/save.json";

    public static void Save(GameProgress progress) {
        //Debug.Log("Save");
        string json = JsonUtility.ToJson(progress,true);
        File.WriteAllText(path, json);
    }
    public static GameProgress Load() {
        if (File.Exists(path)) {
            //Debug.Log("Load");
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameProgress>(json);
        }
        return new GameProgress();
    }
    public static void Reset() {
        if (File.Exists(path))
            File.Delete(path);
        //Debug.Log("Reset");
    }
}
