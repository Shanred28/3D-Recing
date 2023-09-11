using UnityEngine;

namespace Race
{
    public class CameraPatchFollower : CarCameraComponent
    {
        [SerializeField] private Transform _path;
        [SerializeField] private Transform _lookTarget;
        [SerializeField] private float _movementSpeed;

        private Vector3[] _points; 
        private int _pointIndex;

        private void Start()
        {
            _points = new Vector3[_path.childCount];

            for (int i = 0; i < _points.Length; i++)
            {
                _points[i] = _path.GetChild(i).position;
            }
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _points[_pointIndex], _movementSpeed * Time.deltaTime);

            if (transform.position == _points[_pointIndex])
            { 
                if(_pointIndex == _points.Length - 1)
                    _pointIndex = 0;
                else
                    _pointIndex ++;
            }

            transform.LookAt(_lookTarget);
        }

        public void StartMoveToNearesPoint()
        { 
            float minDistance = float.MaxValue;

            for (int i = 0; i < _points.Length; i++)
            {
                float distance = Vector3.Distance(transform.position, _points[i]); 

                if (distance < minDistance)
                { 
                    minDistance = distance;
                    _pointIndex = i;
                }
            }
        }

        public void SetLookTarget(Transform target)
        {
            _lookTarget = target;
        }
    }
}

