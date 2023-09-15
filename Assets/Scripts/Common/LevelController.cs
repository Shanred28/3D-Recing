using RaceUI;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private UIRaceButton[] _uIRaceButtons;

    private string[] _nameScene;

    private void Start()
    {
        Load();
    }

    private void Load()
    {
        
        print(PlayerPrefs.GetInt(_uIRaceButtons[0].InfoRace.SceneName, 0));
        print(_uIRaceButtons[0].InfoRace.SceneName);
        print(PlayerPrefs.GetInt(_uIRaceButtons[1].InfoRace.SceneName, 0));
        print(_uIRaceButtons[1].InfoRace.SceneName);
        print(PlayerPrefs.GetInt(_uIRaceButtons[2].InfoRace.SceneName, 0));
        print(_uIRaceButtons[2].InfoRace.SceneName);
        print(PlayerPrefs.GetInt(_uIRaceButtons[3].InfoRace.SceneName, 0));
        print(_uIRaceButtons[3].InfoRace.SceneName);
        for (int i = 1; i < _uIRaceButtons.Length; i++)
        {
            //var name = PlayerPrefs.GetString(_uIRaceButtons[i].InfoRace.SceneName, "Empty");
            var active = PlayerPrefs.GetInt(_uIRaceButtons[i].InfoRace.SceneName, 0);
            // _uIRaceButtons[i].SetEnable(active);
            if (active == 0)
            {
                if(PlayerPrefs.GetInt(_uIRaceButtons[i-1].InfoRace.SceneName, 0) != 0)
                _uIRaceButtons[i].SetIsFinish(true);
            }
               
            else
               _uIRaceButtons[i].SetIsFinish(true);
               
        }
/*
        for (int i = 1; i < _uIRaceButtons.Length; i++)
        {
            if (_uIRaceButtons[i].Is_Finish == false)
            {
                if(_uIRaceButtons[i - 1].Is_Finish == true)
                _uIRaceButtons[i].SetIsFinish(true);
                return;
            }


        }*/

        //var _nameScene = PlayerPrefs.GetString(_uIRaceButtons.InfoRace.SceneName, "Empty");


        /* if (_uIRaceButtons.InfoRace.SceneName == SceneManager.GetSceneByBuildIndex(1).name)
             _uIRaceButtons.SetEnable(1);
         else
         {
             var active = PlayerPrefs.GetInt(_uIRaceButtons.InfoRace.SceneName, 0);
             _uIRaceButtons.SetEnable(active);
         }
         print(SceneManager.GetSceneByBuildIndex(1).name);*/
    }
}
