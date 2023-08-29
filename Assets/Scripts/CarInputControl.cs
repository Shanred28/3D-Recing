using UnityEngine;

namespace Race
{
    public class CarInputControl : MonoBehaviour
    {
        [SerializeField] private Car _car;

        private void Update()
        {
            _car.throttleControl = Input.GetAxis("Vertical");
            _car.brakeControl = Input.GetAxis("Jump");
            _car.steerControl = Input.GetAxis("Horizontal");

        }
    }
}

