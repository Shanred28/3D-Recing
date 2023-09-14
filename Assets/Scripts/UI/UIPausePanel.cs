using Race;
using UnityEngine;

namespace RaceUI
{
    public class UIPausePanel : MonoBehaviour, IDependency<Pauser>
    {
        [SerializeField] private GameObject _panel;

        private Pauser _pauser;
        public void Construct(Pauser obj) => _pauser = obj;


        private void Start()
        {
            
            _pauser.pauseStateChange += OnPauseStartChange;
            _panel.SetActive(false);
        }

        private void OnDestroy()
        {
            _pauser.pauseStateChange -= OnPauseStartChange;
        }

        public void OnPauseStartChange(bool isPause)
        {
            _panel.SetActive(isPause);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) == true)
            { 
                _pauser.ChangePauseState();
            }
        }

        public void UnPause()
        {
            _pauser.UnPause();
        }
    }
}

