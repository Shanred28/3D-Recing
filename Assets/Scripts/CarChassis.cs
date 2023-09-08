using UnityEngine;

namespace Race
{
    [RequireComponent(typeof(Rigidbody))]
    public class CarChassis : MonoBehaviour
    {
        [SerializeField] private WheelAxle[] _wheelAxles;
        [SerializeField] private float _wheelBaseLenght;
        [SerializeField] private Transform _centerOfMass;

        [Header("AngularDrag")]
        [SerializeField] private float _angularDragMin;
        [SerializeField] private float _angularDragMax;
        [SerializeField] private float _angularDragFactor;

        [Header("Down Force Air")]
        [SerializeField] private float _downForceMin;
        [SerializeField] private float _downForceMax;
        [SerializeField] private float _downForceFactor;

        public float motorTorque;
        public float breakTorque;
        public float steerAngle;

        private new Rigidbody _rigidbody;
        public float linearVelocity => _rigidbody.velocity.magnitude * 3.6f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            if (_centerOfMass != null)
                _rigidbody.centerOfMass = _centerOfMass.localPosition;

            for (int i = 0; i < _wheelAxles.Length; i++)
            {
                _wheelAxles[i].ConfigureVehicleSubSteps(5, 5, 5);
            }
        }

        private void FixedUpdate()
        {
            UpdateAngularDrag();
            UpdateDownForce();

            UpdateWheelAxles();
        }

        public float GetAverageRpm()
        {
            float sum = 0;

            for (int i = 0; i < _wheelAxles.Length; i++)
            {
                sum += _wheelAxles[i].GetAvarageRpm();
            }

            return sum / _wheelAxles.Length;
        }

        public float GetWheelSpeed()
        { 
            return GetAverageRpm() * _wheelAxles[0].GetRadius() * 2 * 0.1885f;
        }

        private void UpdateDownForce()
        {
             float downForce = Mathf.Clamp(_downForceFactor * linearVelocity, _downForceMin, _downForceMax);
            _rigidbody.AddForce(-transform.up * downForce);
        }

        private void UpdateAngularDrag()
        {
            _rigidbody.angularDrag = Mathf.Clamp(_angularDragFactor * linearVelocity, _angularDragMin, _angularDragMax);
        }

        private void UpdateWheelAxles()
        {
            int amountMotorWheel = 0;
            for (int i = 0; i < _wheelAxles.Length; i++)
            {
                if (_wheelAxles[i].IsMotor == true)
                    amountMotorWheel += 2;
            }

            for (int i = 0; i < _wheelAxles.Length; i++)
            {
                _wheelAxles[i].Update();
                _wheelAxles[i].ApplyMotorTorque(motorTorque / amountMotorWheel);
                _wheelAxles[i].ApplyBreakTorque(breakTorque);
                _wheelAxles[i].ApplySteeerAngle(steerAngle, _wheelBaseLenght);
            }
        }
    }
}

