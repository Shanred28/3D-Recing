using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore;
using UnityEngine.UI;

namespace RaceUI
{
    public class UIRaceButton : UISeelectableButton
    {
        [SerializeField] private RaceInfo _raceInfo;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _textTitle;

        private void Start () 
        {
            ApllyProperty(_raceInfo);
        }

        public void ApllyProperty(RaceInfo property)
        {
            if (property == null) return;

            _raceInfo = property;

            _icon.sprite = _raceInfo.Icon;
            _textTitle.text = _raceInfo.Title; 
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (_raceInfo == null) return;
            SceneManager.LoadScene(_raceInfo.SceneName);
        }
    }
}

