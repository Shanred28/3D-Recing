using UnityEngine;

namespace Race
{
    public class ActivatedTrackPoint : TrackPoint
    {
        [SerializeField] private GameObject _hint;

        private void Start()
        {
            _hint.SetActive(isTarget);
        }

        protected override void OnPassed()
        {
            _hint.SetActive(false);
        }

        protected override void OnAssignAsTarget()
        {
            _hint.SetActive(true);
        }
    }
}

