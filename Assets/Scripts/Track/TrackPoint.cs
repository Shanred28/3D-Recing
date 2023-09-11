using UnityEngine;
using UnityEngine.Events;

namespace Race
{
    public class TrackPoint : MonoBehaviour
    {
        public event UnityAction<TrackPoint> triggered;

        protected virtual void OnPassed() { }
        protected virtual void OnAssignAsTarget() { }

        public TrackPoint next;
        public bool isFerst;
        public bool isLast;

        protected bool _isTarget;
        public bool isTarget => _isTarget;

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.root.GetComponent<Car>() == null) return;

            triggered?.Invoke(this);
        }

        public void Passed()
        {
            _isTarget = false;
            OnPassed();
        }

        public void AssignAsTarget()
        {
            _isTarget = true;
            OnAssignAsTarget();
        }

        public void Reset()
        {
            next = null; 
            isFerst = false;
            isLast = false;            
        }
    }
}

