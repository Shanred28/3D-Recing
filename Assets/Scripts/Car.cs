using UnityEngine;

namespace Race
{
    [RequireComponent(typeof(CarChassis))]
    public class Car : MonoBehaviour
    {      
        [SerializeField] private float _maxMotorTorque;
        [SerializeField] private float _maxSteerAngle;
        [SerializeField] private float _maxBrakeTorque;

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
            _chassis.motorTorque = throttleControl * _maxMotorTorque;
            _chassis.breakTorque = brakeControl * _maxBrakeTorque;
            _chassis.steerAngle = steerControl * _maxSteerAngle;
        }
    }
}
