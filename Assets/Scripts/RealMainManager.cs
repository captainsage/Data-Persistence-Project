using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using static UnityEditor.Progress;

public class RealMainManager : MonoBehaviour
{
    public static RealMainManager Instance;

    public Dictionary<string, int> players = new Dictionary<string, int>();
    public string ActivePlayer;
    public int HighScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadPlayerData();
    }


    [System.Serializable]
    class SaveData
    {
        public List<int> highScores = new List<int>();
        public List<string> names = new List<string>(); 

    }


    private void SaveDictionary(Dictionary<string, int> playerData, SaveData data)
    {
        data.highScores = new List<int>();
        data.names = new List<string>();

        foreach (var item in playerData)
        {
            data.highScores.Add(item.Value);
            data.names.Add(item.Key);
        }
    }

    private Dictionary<string, int> LoadDictionary(SaveData data)
    {
        Dictionary<string, int> loadedData = new Dictionary<string, int>();
        for (int i = 0; i < data.highScores.Count; i++)
        {
            loadedData[data.names[i]] = data.highScores[i];
        }
        return loadedData;
    }

    public void SavePlayerData()
    {
        SaveData data = new SaveData();

        if(players.Any())
        {
            SaveDictionary(players, data);

            string json = JsonUtility.ToJson(data);
            Debug.Log(json);
            Debug.Log(data.highScores.Count());
            Debug.Log(Application.persistentDataPath);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            players = LoadDictionary(data);
        }
    }

    public void SetScore(int score)
    {
        if((ActivePlayer != null && ActivePlayer != "") && ((players.ContainsKey(ActivePlayer) && score > players[ActivePlayer]) || !players.ContainsKey(ActivePlayer)))
        {
            players[ActivePlayer] = score;
            Debug.Log(players.Count());
        }
    }

    public string SetHighScoreText()
    {
        Debug.Log($"pCount: {players.Count}");
        if (players.Any())
        {
            int maxV = 0;
            string maxP = "";
            foreach (var item in players)
            {
                if(item.Value > maxV)
                {
                    maxV = item.Value;
                    maxP = item.Key;
                }
            }
            return $"Best Score : {maxV} Name : {maxP}";
        }
        else return $"Best Score : Name : 0";
    }
}
