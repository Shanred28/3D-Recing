using UnityEngine;

namespace Race
{
    public class CameraFovCorrectors : CarCameraComponent
    {
        [SerializeField] private float _minFieldOfView;
        [SerializeField] private float _maxFieldOfView;

        private float _defaultFov;

        private void Start()
        {
            _camera = GetComponent<Camera>();
            _camera.fieldOfView = _defaultFov;
        }

        private void Update()
        {
            _camera.fieldOfView = Mathf.Lerp(_minFieldOfView, _maxFieldOfView, _car.NormalizeVelocity);
        }
    }
}

