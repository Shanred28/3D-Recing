using UnityEngine;

namespace RaceUI
{
    [CreateAssetMenu]
    public class ResolutionSetting : Setting
    {
        [SerializeField]
        private Vector2Int[] _avalibaleResolutions = new Vector2Int[]
        {
            new Vector2Int(800, 600),
            new Vector2Int(1280,720),
            new Vector2Int(1600,900),
            new Vector2Int(1920,1080),
        };

        private int _currentresolutionIndex = 0;

        public override bool isMinValue { get => _currentresolutionIndex == 0; }
        public override bool isMaxValue { get => _currentresolutionIndex == _avalibaleResolutions.Length - 1; }

        public override void SetNextValue()
        {
            if(isMaxValue == false)
                _currentresolutionIndex++;
        }
        public override void SetPreviousValue()
        {
            if (isMinValue == false)
                _currentresolutionIndex--;
        }

        public override object GetValue()
        {
            return _avalibaleResolutions[_currentresolutionIndex];
        }

        public override string GetStringValue()
        {
            return _avalibaleResolutions[_currentresolutionIndex].x + "x" + _avalibaleResolutions[_currentresolutionIndex].y;
        }

        public override void Apply()
        {
            Screen.SetResolution(_avalibaleResolutions[_currentresolutionIndex].x, _avalibaleResolutions[_currentresolutionIndex].y, true);
            Save();
        }

        public override void Load()
        {
            _currentresolutionIndex = PlayerPrefs.GetInt(title, 0);
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(title, _currentresolutionIndex);
        }
    }
}

