using UnityEngine;

namespace RaceUI
{
    [RequireComponent(typeof(AudioSource))]
    public class UiButtonSound : MonoBehaviour
    {
        [SerializeField] private AudioClip _clickClip;
        [SerializeField] private AudioClip _selectClip;
        private AudioSource _audioSource;
        private UIButton[] _uiButtons;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _uiButtons = GetComponentsInChildren<UIButton>(true);

            for (int i = 0; i < _uiButtons.Length; i++)
            {
                _uiButtons[i].eventPointerEnter += OnPointerEnter;
                _uiButtons[i].eventPointerClick += OnPointerClick;
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _uiButtons.Length; i++)
            {
                _uiButtons[i].eventPointerEnter -= OnPointerEnter;
                _uiButtons[i].eventPointerClick -= OnPointerClick;
            }
        }

        private void OnPointerEnter(UIButton arg0)
        {
            _audioSource.PlayOneShot(_selectClip);
        }

        private void OnPointerClick(UIButton arg0)
        {
            _audioSource.PlayOneShot(_clickClip);
        }
    }
}

