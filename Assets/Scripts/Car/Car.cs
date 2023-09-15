using UnityEngine;
using UnityEngine.Events;

namespace Race
{
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {
        [SerializeField] private float _maxSteerAngle;
        [SerializeField] private float _maxBrakeTorque;

        [Header("Engine")]
        [SerializeField] private AnimationCurve _engineTorqueCurve;
        [SerializeField] private float _engineTorque;
        [SerializeField] private float _engineMaxTorque;
        [SerializeField] private float _engineRpm;

        public float EngineRpm => _engineRpm;

        [SerializeField] private float _engineMinRpm;
        [SerializeField] private float _engineMaxRpm;

        public float EngineMaxRpm => _engineMaxRpm;

        [Header("Gearbox")]
        [SerializeField] private float[] _gears;
        [SerializeField] private float _finalDriveRatio;
        [SerializeField] private float _upShiftEngineRpm;
        [SerializeField] private float _downShiftEngineRpm;
        //Debug
        [SerializeField] private int _selectedGeadIndex;

        //Debug
        [SerializeField] private float selecteedGear;
        [SerializeField] private float rearGear;

        [SerializeField] private int _maxSpeed;

        public float LinearVelocity => _chassis.linearVelocity;
        public float NormalizeVelocity => _chassis.linearVelocity / _maxSpeed;
        public float WheelSpeed => _chassis.GetWheelSpeed();
        public float MaxSpeed => _maxSpeed;


        [SerializeField] private float linearVelocity;
        public float throttleControl;
        public float steerControl;
        public float brakeControl;
        //public float handBrakeControl;

        private CarChassis _chassis;
        public Rigidbody Rigidbody => _chassis == null ?  GetComponent<CarChassis>().Rigidbody : _chassis.Rigidbody;

        public event UnityAction<string> GearChange;
        private void Start()
        {
            _chassis = GetComponent<CarChassis>();
        }

        private void Update()
        {
            linearVelocity = LinearVelocity;

            UpdateEngineTorque();
            AutoGearShift();

            if (linearVelocity >= _maxSpeed)
                _engineTorque = 0;

            _chassis.motorTorque = throttleControl * _engineTorque;
            _chassis.breakTorque = brakeControl * _maxBrakeTorque;
            _chassis.steerAngle = steerControl * _maxSteerAngle;
        }

        //Gearbox
        private void AutoGearShift()
        {
            if (selecteedGear < 0) return;

            if (_engineRpm >= _upShiftEngineRpm)
                UpGear();

            if(_engineRpm < _downShiftEngineRpm)
                DownGear();
        }

        public void UpGear()
        {
            ShiftGear(_selectedGeadIndex + 1);
        }

        public void DownGear()
        {
            ShiftGear(_selectedGeadIndex - 1);
        }

        public void ShiftToReverseGeear()
        {
            selecteedGear = rearGear;

            GearChange?.Invoke(GetSelectedGearName());
        }

        public void ShiftToFirstGear()
        {
            ShiftGear(0);
        }
        public void ShiftToNetral()
        {
            selecteedGear = 0;
            GearChange?.Invoke(GetSelectedGearName());
        }

        public string GetSelectedGearName()
        { 
            if(selecteedGear == rearGear) return "R";
            if (selecteedGear == 0) return "N";
            return (_selectedGeadIndex +1).ToString();
        }                         

        private void ShiftGear(int gearIndex)
        {
            gearIndex = Mathf.Clamp(gearIndex, 0, _gears.Length - 1); 
            selecteedGear = _gears[gearIndex];
            _selectedGeadIndex = gearIndex;

            GearChange?.Invoke(GetSelectedGearName());
        }

        private void UpdateEngineTorque()
        {
            _engineRpm = _engineMinRpm + Mathf.Abs(_chassis.GetAverageRpm() * selecteedGear * _finalDriveRatio);
            _engineRpm = Mathf.Clamp(_engineRpm, _engineMinRpm, _engineMaxRpm);

            _engineTorque = _engineTorqueCurve.Evaluate(_engineRpm / _engineMaxRpm) * _engineMaxTorque * _finalDriveRatio * Mathf.Sign(selecteedGear) * _gears[0];
        }

        public void Reset()
        {
            _chassis.Reset();
            
            _chassis.motorTorque = 0;
            _chassis.breakTorque = 0;
            _chassis.steerAngle = 0;

            throttleControl = 0;
            brakeControl = 0;
            steerControl = 0;

        }

        public void Respawn(Vector3 position, Quaternion rotation)
        {
            Reset();

            transform.position = position;
            transform.rotation = rotation;
        }
    }
}
