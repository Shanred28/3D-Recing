using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RaceUI
{
    public class UIRaceButton : UISeelectableButton, IScriptableObjectProperty
    {
        [SerializeField] private RaceInfo _raceInfo;
        public RaceInfo InfoRace => _raceInfo;
        [SerializeField] private Image _ImageBatton;
        [SerializeField] private Image _icon;       
        [SerializeField] Image _iconDisable;
        [SerializeField] private TMP_Text _textTitle;

        private bool _isFinish;
        public bool Is_Finish => _isFinish;

        private void Start () 
        {
            ApplyProperty(_raceInfo);
            SetEnable();
        }

        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;
            if(property is RaceInfo == false) return;

            _raceInfo = property as RaceInfo;

            _icon.sprite = _raceInfo.Icon;
            _textTitle.text = _raceInfo.Title; 
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (_raceInfo == null) return;
            SceneManager.LoadScene(_raceInfo.SceneName);
        }

        public void SetIsFinish(bool active)
        {
            _isFinish = active;
        }

        public void SetEnable()
        {
            if (_isFinish == false)
            {
                _iconDisable.enabled = true;
                _ImageBatton.raycastTarget = false;
                interactable = false;
                
            }

            else
            {
                _iconDisable.enabled = false;
                _ImageBatton.raycastTarget = true;
                interactable = true;
            }
        }
    }
}

