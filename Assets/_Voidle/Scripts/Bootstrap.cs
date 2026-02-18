using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
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
        await Addressables.InitializeAsync().Task.AsUniTask();

        AsyncOperationHandle<SceneInstance> handle = Addressables.LoadSceneAsync(_sceneName, LoadSceneMode.Single);

        await handle.Task.AsUniTask();

        Debug.Log($"The {_sceneName} scene has been uploaded successfully!");
    }
}