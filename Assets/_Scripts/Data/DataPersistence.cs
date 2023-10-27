using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class DataPersistence
{
    #region  INSTANCE
    public DataPersistence() { }

    public static DataPersistence Io => Instance.Io;

    private class Instance
    {
        static Instance() { }
        static DataPersistence _io;
        internal static DataPersistence Io => _io ??= new DataPersistence();
        internal static void Destruct() => _io = null;
    }

    public void SelfDestruct()
    {
        Instance.Destruct();
    }
    #endregion INSTANCE

    //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    //private static void TryLoadSettings()
    //{
    //    if (SavedSettingsExists()) { LoadSettings(); }

    //    static bool SavedSettingsExists() => File.Exists(Application.persistentDataPath + "save.settings");
    //    static void LoadSettings()
    //    {
    //        string path1 = Application.persistentDataPath + "save.settings";
    //        BinaryFormatter bf1 = new BinaryFormatter();
    //        FileStream stream1 = new FileStream(path1, FileMode.Open);

    //        TheoryPuzzleData sp = bf1.Deserialize(stream1) as TheoryPuzzleData;
    //        stream1.Close();

    //        DataManager.Io.TheoryPuzzleData = 
    //    }
    //}


}
