using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RaceUI
{
    public class UISettingButton : UISeelectableButton
    {
        [SerializeField] private Setting _setting;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _valueText;
        [SerializeField] private Image _previousImage;
        [SerializeField] private Image _nextImage;

        private void Start()
        {
            ApllyProperty(_setting);
        }

        public void SetNextValueSetting()
        {
            _setting?.SetNextValue();
            UpdateInfo();
            _setting?.Apply();
        }
        public void SetPreviousValueSetting()
        {
            _setting?.SetPreviousValue();
            UpdateInfo();
            _setting?.Apply();
        }

        private void UpdateInfo() 
        {
            _titleText.text = _setting.Title;
            _valueText.text = _setting.GetStringValue();

            _previousImage.enabled = !_setting.isMinValue;
            _nextImage.enabled = !_setting.isMaxValue;
        }

        public void ApllyProperty(Setting property)
        {
            if (property == null) return;
            _setting = property;
            UpdateInfo();
        }
    }
}
