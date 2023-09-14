

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

    private GameplayData _gameplayData;
    public GameplayData GamePlay => _gameplayData ??= new();

    private VolumeData _volume;
    public VolumeData Volume => _volume ??= new();
}