using UnityEngine;
using System.IO;

public static class JsonSaveLoader
{
    private static readonly string fileName = "save.json";
    private static string FilePath => Path.Combine(Application.persistentDataPath, fileName);

    // 저장
    public static void Save(SaveData data)
    {
        string json = JsonUtility.ToJson(data, prettyPrint: true);
        File.WriteAllText(FilePath, json);
    }

    // 불러오기
    public static SaveData Load()
    {
        if (File.Exists(FilePath))
        {
            string json = File.ReadAllText(FilePath);
            return JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            SaveData data = new SaveData();
            Save(data);
            return data;
        }
    }

    // 초기화 (파일 삭제)
    public static void ResetSave()
    {
        if (File.Exists(FilePath))
        {
            File.Delete(FilePath);
        }
    }
}