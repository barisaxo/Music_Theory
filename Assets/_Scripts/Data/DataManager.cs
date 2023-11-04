using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class DataManager
{
    #region  INSTANCE
    public DataManager() { }
    public static DataManager Io => Instance.Io;

    private class Instance
    {
        static Instance() { }
        static DataManager _io;
        internal static DataManager Io => _io ??= new DataManager();
        internal static void Destruct() => _io = null;
    }

    public void SelfDestruct()
    {
        Instance.Destruct();
    }
    #endregion INSTANCE

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void TryLoadData()
    {
        if (File.Exists(Application.persistentDataPath + fileName))
        {
            FileStream stream = new(Application.persistentDataPath + fileName, FileMode.Open);
            var data = new BinaryFormatter().Deserialize(stream) as TheoryPuzzleData;
            stream.Close();
            if (Io.TheoryPuzzleData.Stats != null) Io.TheoryPuzzleData.LoadStatsData(data.Stats);
        }
    }

    public void SaveTheoryPuzzleData()
    {
        FileStream fileStream = new(Application.persistentDataPath + fileName, FileMode.Create);
        new BinaryFormatter().Serialize(fileStream, TheoryPuzzleData);
        fileStream.Close();
    }

    const string fileName = "save.me";

    private GameplayData _gameplayData;
    public GameplayData GamePlay => _gameplayData ??= new();

    private VolumeData _volume;
    public VolumeData Volume => _volume ??= new();

    private TheoryPuzzleData _theoryPuzzle;
    public TheoryPuzzleData TheoryPuzzleData => _theoryPuzzle ??= new();
}