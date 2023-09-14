using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDependenciesContainer : Dependency
{
    [SerializeField] private Pauser _pauser;
    private static GlobalDependenciesContainer _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    protected override void BindAll(MonoBehaviour mono)
    {
        Bind<Pauser>(_pauser, mono);
    }

    private void OnSceneLoad(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectToBind();
    }
}
