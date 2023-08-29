using UnityEngine;

namespace Race
{
    public class CarChassis : MonoBehaviour
    {
        [SerializeField]private WheelAxle[] _wheelAxles;

        public float motorTorque;
        public float breakTorque;
        public float steerAngle;

        private void FixedUpdate()
        {
            UpdateWheelAxles();
        }

        private void UpdateWheelAxles()
        {
            for (int i = 0; i < _wheelAxles.Length; i++)
            {
                _wheelAxles[i].Update();
                _wheelAxles[i].ApplyMotorTorque(motorTorque);
                _wheelAxles[i].ApplyBreakTorque(breakTorque);
                _wheelAxles[i].ApplySteeerAngle(steerAngle);
            }
        }
    }
}

