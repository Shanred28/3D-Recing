using UnityEngine;

namespace Race
{
    [RequireComponent(typeof(AudioSource))]
    public class EngineSound : MonoBehaviour
    {
        [SerializeField] private Car _car;

        [SerializeField] private AudioSource _engineCarAudioSource;
        [SerializeField] private float _pitchModifier;
        [SerializeField] private float _volumeModifier;
        [SerializeField] private float _rpmModifier;

        [SerializeField] private float _basePitch = 1.0f;
        [SerializeField] private float _baseeVolume = 0.4f;

        private void Start () 
        {
            _engineCarAudioSource.GetComponent<AudioSource>();
        }

        private void Update () 
        {
            _engineCarAudioSource.pitch = _basePitch + _pitchModifier * (_car.EngineRpm / _car.EngineMaxRpm * _rpmModifier);
            _engineCarAudioSource.volume = _baseeVolume + _volumeModifier * (_car.EngineRpm / _car.EngineMaxRpm);
        }
    }
}

