using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private void Awake()
    {

        Load().Forget();
    }

    private async UniTaskVoid Load()
    {
        await SceneManager.LoadSceneAsync(_sceneName);
        Debug.Log($"Scene {_sceneName} is loaded!");

    }
}