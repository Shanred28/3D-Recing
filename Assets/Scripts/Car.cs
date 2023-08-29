using UnityEngine;

namespace Race
{
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {      
        [SerializeField] private float _maxSteerAngle;
        [SerializeField] private float _maxBrakeTorque;      

        [SerializeField] private AnimationCurve _engineTorqueCurve;
        [SerializeField] private int _maxSpeed;
        [SerializeField] private float _maxMotorTorque;


        public float LinearVelocity => _chassis.linearVelocity;
        public float WheelSpeed => _chassis.GetWheelSpeed();
        public float MaxSpeed => _maxSpeed;


        [SerializeField] private float linearVelocity;
        public float throttleControl;
        public float steerControl;
        public float brakeControl;
        //public float handBrakeControl;

        private CarChassis _chassis;

        private void Start()
        {
           _chassis = GetComponent<CarChassis>();
        }

        private void Update()
        {
            linearVelocity = LinearVelocity;
            float engineTorque = _engineTorqueCurve.Evaluate(linearVelocity / _maxSpeed) * _maxMotorTorque;

            if (linearVelocity >= _maxSpeed)
                engineTorque = 0;

            _chassis.motorTorque = throttleControl * engineTorque;
            _chassis.breakTorque = brakeControl * _maxBrakeTorque;
            _chassis.steerAngle = steerControl * _maxSteerAngle;
        }
    }
}
