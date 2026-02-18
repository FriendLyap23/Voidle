using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class DataPersistence : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string _fileName;
    [SerializeField] private bool _useEncryption;

    private GameData _gameData;
    private List<IDataPersistence> _dataPersistenceObjects;
    private FileDataHandler _fileDataHandler;

    public static DataPersistence Instance { get; private set; }

    [Inject]
    private void Construct(List<IDataPersistence> dataPersistenceObjects)
    {
        _dataPersistenceObjects = dataPersistenceObjects;
    }

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Found more than one Data Persistence in the scene.");

        Instance = this;
    }

    private void Start()
    {
        _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);

        Debug.Log($"Save file path: {Application.persistentDataPath}/{_fileName}");

        LoadGame();
    }

    public void NewGame() 
    {
        _gameData = new GameData();
    }

    public void LoadGame()
    {
        _gameData = _fileDataHandler.Load();
        Debug.Log($"LoadGame called. Data found: {_gameData != null}");

        if (_gameData == null)
        {
            Debug.Log("No data was found. Initializing data to defults");
            NewGame();
        }

        foreach (IDataPersistence dataPersistence in _dataPersistenceObjects) 
        {
            dataPersistence.LoadData(_gameData);
        }
    }

    public void SaveGame()
    {
        foreach (IDataPersistence dataPersistence in _dataPersistenceObjects)
        {
            dataPersistence.SaveData(ref _gameData);
        }

        _fileDataHandler.Save(_gameData);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
