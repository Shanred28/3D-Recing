using UnityEngine;

namespace Race
{
    [RequireComponent(typeof(AudioSource))]
    public class PauseAudioSourse : MonoBehaviour, IDependency<Pauser>
    {
        private Pauser _pauser;
        public void Construct(Pauser obj) => _pauser = obj;
        private new AudioSource _audioSource;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            _pauser.pauseStateChange += OnPauseStateChange;
        }
        private void OnDestroy()
        {
            _pauser.pauseStateChange -= OnPauseStateChange;
        }

        private void OnPauseStateChange(bool pause)
        {           
            if(pause == true)
                _audioSource.Stop();
            if(pause == false)
                _audioSource.Play();
        }
    }
}

