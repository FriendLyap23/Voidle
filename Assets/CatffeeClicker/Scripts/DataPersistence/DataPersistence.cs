using UnityEngine;
using System.Collections.Generic;
using Zenject;

public class DataPersistence : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private SaveConfig _saveConfig;

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
        _fileDataHandler = new FileDataHandler
            (Application.persistentDataPath,
            _saveConfig.fileName,
            _saveConfig.useEncryption
            );

        Debug.Log($"Save file path: {Application.persistentDataPath}/{_saveConfig.fileName}");

        LoadGame();
    }

    public void NewGame() 
    {
        _gameData = CreateDefaultGameData();
    }

    private GameData CreateDefaultGameData()
    {
        return new GameData
        {
            Money = _saveConfig.defaultMoney,
            MoneyPerClick = _saveConfig.defaultMoneyPerClick,
            MoneyPerSecond = _saveConfig.defaultMoneyPerSecond,
            MaxMonies = _saveConfig.defaultMaxMonies,
            Level = _saveConfig.defaultLevel,
            ExperienceLevel = _saveConfig.defaultExperienceLevel,
            ExperiencePerClick = _saveConfig.defaultExperiencePerClick,
            UpgradesSaveData = new List<UpgradeSaveData>()
        };
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
