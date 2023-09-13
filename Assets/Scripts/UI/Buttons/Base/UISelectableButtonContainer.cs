using System;
using UnityEngine;

namespace RaceUI
{
    public class UISelectableButtonContainer : MonoBehaviour
    {
        [SerializeField] private Transform _buttonsContainer;

        public bool Interactable = true;
        public void SetInteractable(bool interactable) => Interactable = interactable;

        private UISeelectableButton[] _buttons;
        private int _selectButtonIndex;

        private void Start()
        {
            _buttons = _buttonsContainer.GetComponentsInChildren<UISeelectableButton>();

            if (_buttons == null)
                Debug.LogError("Button list is empty!");

            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].eventPointerEnter += OnPointerEnter;
            }

            if (Interactable == false) return;

            _buttons[_selectButtonIndex].SetFocuse();
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttons[i].eventPointerEnter -= OnPointerEnter;
            }
        }

        private void OnPointerEnter(UIButton button)
        {
            SelectButton(button);
        }

        private void SelectButton(UIButton button)
        {
            if (Interactable == false) return;

            _buttons[_selectButtonIndex].SetUnFocuse();

            for (int i = 0; i < _buttons.Length; i++)
            {
                if (button == _buttons[i])
                {
                    _selectButtonIndex = i;
                    button.SetFocuse();
                    break;
                }
            }
        }

        public void SelectNext()
        { 
        
        }

        public void SelectPrevious()
        {

        }
    }
}

