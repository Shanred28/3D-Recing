using System;
using UnityEngine;

namespace Race
{
    public class CarCameraController : MonoBehaviour, IDependency<Car>, IDependency<RaceStateTracker>
    {
        [SerializeField] private new Camera _camera;
        [SerializeField] private CameraFollow _cameraFollow;
        [SerializeField] private CameraShaker _cameraShaker;
        [SerializeField] private CameraFovCorrectors _cameraFovCorrectors;
        [SerializeField] private CameraPatchFollower _cameraPatchFollower;

        private Car _car;
        public void Construct(Car obj) => _car = obj;

        private RaceStateTracker _raceStateTracker;
        public void Construct(RaceStateTracker obj) => _raceStateTracker = obj;



        private void Awake()
        {
            _cameraFollow.SetProperties(_car, _camera);
            _cameraShaker.SetProperties(_car, _camera);
            _cameraFovCorrectors.SetProperties(_car, _camera);

        }

        private void Start()
        {
            _raceStateTracker.eventPreparationStarted += OnPreparationStarted;
            _raceStateTracker.eventCompleted += OnCompleted;
            _cameraFollow.enabled = false;
            _cameraPatchFollower.enabled = true;
        }
        private void OnDestroy()
        {
            _raceStateTracker.eventPreparationStarted -= OnPreparationStarted;
            _raceStateTracker.eventCompleted -= OnCompleted;
        }

        private void OnPreparationStarted()
        {
            _cameraFollow.enabled = true;
            _cameraPatchFollower.enabled = false;
        }

        private void OnCompleted()
        {          
            _cameraPatchFollower.enabled = true;
            _cameraPatchFollower.StartMoveToNearesPoint();
            _cameraPatchFollower.SetLookTarget(_car.transform);

            _cameraFollow.enabled = false;
        }
    }
}

