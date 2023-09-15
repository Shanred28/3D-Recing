using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Pauser : MonoBehaviour
{
    public event UnityAction<bool> pauseStateChange;

    private bool _isPause;
    public bool isPaused => _isPause;

    private void Awake()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoadeed;
    }

    private void SceneManager_sceneLoadeed(Scene arg0, LoadSceneMode arg1)
    {
        UnPause();
    }

    public void ChangePauseState()
    {
        if (_isPause == true)
            UnPause();
        else
            Pause();
    }

    public void Pause()
    {
        if (_isPause == true) return;

        Time.timeScale = 0;

        _isPause = true;
        pauseStateChange?.Invoke(_isPause);
    }

    public void UnPause()
    {
        if (_isPause == false) return;

        Time.timeScale = 1;

        _isPause = false;
        pauseStateChange?.Invoke(_isPause);
    }
}
