using UnityEngine;

namespace Race
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private new Rigidbody _rigidbody;

        [Header("Offset")]
        [SerializeField] private float _viewHeight;
        [SerializeField] private float _height;
        [SerializeField] private float _distance;

        [Header("Damping")]
        [SerializeField] private float _rotationDamping;
        [SerializeField] private float _heightDamping;
        [SerializeField] private float _speedThreshold;

        private void FixedUpdate()
        {

            Vector3 velocity = _rigidbody.velocity;
            Vector3 targetRotation = _target.eulerAngles;

            if (velocity.magnitude > _speedThreshold)
            { 
                targetRotation = Quaternion.LookRotation(velocity, Vector3.up).eulerAngles;
            }

            //Lerp
            float currentAngle = Mathf.LerpAngle(transform.eulerAngles.y, targetRotation.y, _rotationDamping * Time.fixedDeltaTime);
            float currentHight = Mathf.Lerp(transform.position.y,_target.position.y+ _height,_heightDamping * Time.fixedDeltaTime);

            //Position
            Vector3 positionOffset = Quaternion.Euler(0, currentAngle, 0) * Vector3.forward * _distance;
            transform.position = _target.position - positionOffset;
            transform.position = new Vector3(transform.position.x, currentHight, transform.position.z);

            //Rotation
            transform.LookAt(_target.position + new Vector3(0, _viewHeight, 0) );
        }
    }
}

