using System;
using TMPro;
using UnityEngine;

namespace Race
{
    public class Metre : MonoBehaviour, IDependency<Car>, IDependency<RaceStateTracker>
    {
        private Car _car;
        public void Construct(Car obj) => _car = obj;

        private RaceStateTracker _raceStateTracker;
        public void Construct(RaceStateTracker obj) => _raceStateTracker = obj;

        [SerializeField] private RectTransform _arrowSpeed;
        [SerializeField] private RectTransform _arrowTaxometre;

        [SerializeField] private TMP_Text _textSpeed;
        [SerializeField] private TMP_Text _texttaxometre;

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxEngineRPM;

        private void Start () 
        {
            _arrowSpeed.rotation = Quaternion.Euler(0f, 0f, -117f);
            _car.GearChange += OnGearChange;
            _maxEngineRPM = _car.EngineMaxRpm;
            _raceStateTracker.eventPreparationStarted += OnPreparationStarted;
            _raceStateTracker.eventCompleted += OnPreparationCompleted;
            gameObject.SetActive(false);
            
        }

        private void Update()
        {
            float speed = _car.LinearVelocity;
            float angleSpeed = MapValue(speed, 0f, _maxSpeed, 117f, -117f);
            _arrowSpeed.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(angleSpeed, speed, Time.deltaTime));

            _textSpeed.text = speed.ToString("F0");

            float engineRPM = _car.EngineRpm;
            float angleEngineSpeed = MapValue(engineRPM, 0f, _maxEngineRPM, 115f, -115f);
            _arrowTaxometre.rotation = Quaternion.Euler(0f, 0f, angleEngineSpeed);

        }

        private void OnDestroy()
        {
            _car.GearChange -= OnGearChange;
            _raceStateTracker.eventPreparationStarted -= OnPreparationStarted;
        }

        float MapValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
        {
            return (value - inputMin) * (outputMax - outputMin) / (inputMax - inputMin) + outputMin;
        }

        private void OnGearChange(string gearName)
        {
            _texttaxometre.text = gearName;
        }

        private void OnPreparationStarted()
        {
            gameObject.SetActive(true);
        }
        private void OnPreparationCompleted()
        {
            gameObject.SetActive(false);
        }
    }
}

