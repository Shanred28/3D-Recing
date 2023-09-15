using System;
using UnityEngine;

namespace Race
{
    public class CarInputControl : MonoBehaviour
    {
        [SerializeField] private Car _car;
        [SerializeField] private AnimationCurve _breakCurve;
        [SerializeField] private AnimationCurve _steerCurve;

        [SerializeField]
        [Range(0.0f, 1.0f)] private float _autoBreakStrength = 0.5f;

        private float _wheelSpeed;
        private float _verticalAxis;
        private float _horizontalAxis;
        private float _handBreakAxis;

        private void Update()
        {
            _wheelSpeed = _car.WheelSpeed;

            UpdateAxis();
            UpdateThrottle();
            UpdateSteer();


            //_car.brakeControl = Input.GetAxis("Jump");

            UpdateAutoBreak();

/*            if(Input.GetKeyDown(KeyCode.E))
                _car.UpGear();
            if(Input.GetKeyDown(KeyCode.Q))
                _car.DownGear();*/

        }

        private void UpdateSteer()
        {
            _car.steerControl = _steerCurve.Evaluate(_wheelSpeed / _car.MaxSpeed) * _horizontalAxis;
        }

        private void UpdateThrottle()
        {

            if (Mathf.Sign(_verticalAxis) == Mathf.Sign(_wheelSpeed) || Mathf.Abs(_wheelSpeed) < 0.5f)
            {
                _car.throttleControl = Mathf.Abs( _verticalAxis);
                _car.brakeControl = 0;
            }
            else
            {
                _car.throttleControl = 0;
                _car.brakeControl = _breakCurve.Evaluate(_wheelSpeed / _car.MaxSpeed);
            }

            //Gears
            if (_verticalAxis < 0 && _wheelSpeed > -0.5f && _wheelSpeed <= 0.5f)
                _car.ShiftToReverseGeear();

            if(_verticalAxis > 0 && _wheelSpeed > -0.5f && _wheelSpeed < 0.5f)
                _car.ShiftToFirstGear();
        }

        private void UpdateAxis()
        {
            _verticalAxis = Input.GetAxis("Vertical");
            _horizontalAxis = Input.GetAxis("Horizontal");
            _handBreakAxis = Input.GetAxis("Jump");
        }

        private void UpdateAutoBreak()
        {
            if(_verticalAxis == 0)
               _car.brakeControl = _breakCurve.Evaluate(_wheelSpeed / _car.MaxSpeed) * _autoBreakStrength;
        }

        public void Stop()
        {
            Reset();

            _car.brakeControl = 1;
        }

        public void Reset()
        {
            _verticalAxis = 0;
            _horizontalAxis = 0;

            _car.throttleControl = 0;
            _car.steerControl = 0;
            _car.brakeControl = 0;
        }
    }
}

