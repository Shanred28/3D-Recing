using UnityEngine;

namespace Race
{
    public class CameraShaker : CarCameraComponent
    {
        [SerializeField][Range(0.0f, 1.0f)] private float _normolizeSpeedShake;
        [SerializeField] private float _shakeAmount;

        private void Update()
        {
            if(_car.NormalizeVelocity >= _normolizeSpeedShake)
               transform.localPosition += Random.insideUnitSphere * _shakeAmount * Time.deltaTime;
        }
    }
}

