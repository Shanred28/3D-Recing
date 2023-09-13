using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RaceUI
{
    public class UISeelectableButton : UIButton
    {
        [SerializeField] private Image _selectImage;

        public UnityEvent OnSelect;
        public UnityEvent OnUnSelect;

        public override void SetFocuse()
        {
            base.SetFocuse();

            _selectImage.enabled = true;
            OnSelect?.Invoke();
        }

        public override void SetUnFocuse()
        {
            base.SetUnFocuse();

            _selectImage.enabled = false;
            OnUnSelect?.Invoke();
        }
    }
}

