using TMPro;
using UnityEngine;

namespace Race
{
    public class Metre : MonoBehaviour
    {
        [SerializeField] private Car _car;

        [SerializeField] private RectTransform _arrowSpeed;
        [SerializeField] private RectTransform _aarowTaxometre;

        [SerializeField] private TMP_Text _textSpeed;
        [SerializeField] private TMP_Text _texttaxometre;

        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _maxEngineRPM;

        private void Start () 
        {
            _arrowSpeed.rotation = Quaternion.Euler(0f, 0f, -117f);
            _car.GearChange += OnGearChange;
            _maxEngineRPM = _car.EngineMaxRpm;
        }

        private void Update()
        {
            float speed = _car.LinearVelocity;
            float angleSpeed = MapValue(speed, 0f, _maxSpeed, 117f, -117f);
            _arrowSpeed.rotation = Quaternion.Euler(0f, 0f, Mathf.Lerp(angleSpeed, speed, Time.deltaTime));

            _textSpeed.text = speed.ToString("F0");

            float engineRPM = _car.EngineRpm;
            float angleEngineSpeed = MapValue(engineRPM, 0f, _maxEngineRPM, 115f, -115f);
            _aarowTaxometre.rotation = Quaternion.Euler(0f, 0f, angleEngineSpeed);

        }

        private void OnDestroy()
        {
            _car.GearChange -= OnGearChange;
        }

        float MapValue(float value, float inputMin, float inputMax, float outputMin, float outputMax)
        {
            return (value - inputMin) * (outputMax - outputMin) / (inputMax - inputMin) + outputMin;
        }

        private void OnGearChange(string gearName)
        {
            _texttaxometre.text = gearName;
        }
    }
}

