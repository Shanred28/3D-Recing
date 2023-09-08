using UnityEngine;

namespace Race
{
    public class SuspensionArm : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _factor;

        private float _baseOffset;

        private void Start()
        {
            _baseOffset = _target.localRotation.y;
        }

        private void Update()
        {
            transform.localEulerAngles = new Vector3(0, 0, (_target.localRotation.y - _baseOffset) * _factor);
        }
    }
}

