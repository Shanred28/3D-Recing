using UnityEngine;

namespace Race
{
    public class WheelEffect : MonoBehaviour
    {
        [SerializeField] private WheelCollider[] _wheels;
        [SerializeField] private ParticleSystem[] _wheelsSmoke;

        [SerializeField] private float _forwardSlipLimits;
        [SerializeField] private float _sidewaySlipLimits;

        [SerializeField] private GameObject _skidPrefab;
        [SerializeField] private float _yOffsetSkid;

        [SerializeField] private AudioSource _audioSource;

        private WheelHit _wheelHit;
        private Transform[] _skidTtaills;

        private void Start()
        {
            _skidTtaills = new Transform[_wheels.Length];
        }

        private void Update()
        {
            bool isSlip = false;

            for (int i = 0; i < _wheels.Length; i++)
            {
                _wheels[i].GetGroundHit(out _wheelHit);

                if (_wheels[i].isGrounded == true)
                {
                    if (_wheelHit.forwardSlip > _forwardSlipLimits || _wheelHit.sidewaysSlip > _sidewaySlipLimits)
                    {
                        if (_skidTtaills[i] == null)
                            _skidTtaills[i] = Instantiate(_skidPrefab).transform;

                        if(_audioSource.isPlaying == false)
                            _audioSource.Play();

                        if (_skidTtaills[i] != null)
                        {
                            _skidTtaills[i].position = _wheelHit.point + new Vector3(0, _yOffsetSkid,0);
                            _skidTtaills[i].forward = -_wheelHit.normal;

                            _wheelsSmoke[i].transform.position = _skidTtaills[i].position;
                            _wheelsSmoke[i].Emit(5);
                            continue;
                        }
                    }
                    isSlip = true;
                    
                }

                _skidTtaills[i] = null;
                _wheelsSmoke[i].Stop();
            }

            if(isSlip == false)
                _audioSource.Stop();
        }
    }
}

