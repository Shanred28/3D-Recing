using UnityEngine;

namespace Race
{
    public class CameraFovCorrectors : MonoBehaviour
    {
        [SerializeField] private Car _car;
        private new Camera _camera;

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

