using UnityEngine;

namespace Race
{
    public class WindEffect : MonoBehaviour
    {
        [SerializeField] private Car _car;
        [SerializeField][Range(0,1)] private float _normalizeWindSpeedEffect;
        [SerializeField] private float _minParticleSpeed;
        [SerializeField] private float _maxParticleSpeed;
        [SerializeField] private float _minParticleOverTime;
        [SerializeField] private float _maxParticleOverTime;

        [SerializeField] private float _minVolumeSoundWild;
        [SerializeField] private float _maxVolumeSoundWild;



        private ParticleSystem _particleSystem;
        private ParticleSystem.MainModule _mainModule;
        private ParticleSystem.EmissionModule _emissionModule;

        private AudioSource _audioSource;
        private void Start()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _audioSource = GetComponent<AudioSource>();

            _mainModule = _particleSystem.main;

            _emissionModule = _particleSystem.emission;
        }

        private void Update()
        {
            if (_car.NormalizeVelocity >= _normalizeWindSpeedEffect)
            {
                _audioSource.volume = Mathf.Lerp(_minVolumeSoundWild, _maxVolumeSoundWild, _car.NormalizeVelocity);
                _particleSystem.Emit(1);
                _mainModule.simulationSpeed = Mathf.Lerp(_maxParticleSpeed, _minParticleSpeed, _car.NormalizeVelocity);
                _emissionModule.rateOverTime = Mathf.Lerp(_maxParticleOverTime, _minParticleOverTime, _car.NormalizeVelocity);
            }
            else
                _audioSource.volume = 0f;
        }
    }
}

