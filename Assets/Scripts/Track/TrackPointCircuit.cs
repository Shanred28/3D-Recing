using UnityEngine;
using UnityEngine.Events;

namespace Race
{
    public enum TrackType
    { 
       Circular,
       Sprint
    }

    public class TrackPointCircuit : MonoBehaviour
    {
        public event UnityAction<TrackPoint> TrackPointTriggered;
        public event UnityAction<int> LapCompleted;

        [SerializeField] private TrackType _trackType;
        public TrackType TrackType => _trackType;

        private TrackPoint[] _points;

        private int _lapsComplited = -1;

        private void Awake()
        {
            BuildCircuit();
        }

        private void Start()
        {
            for (int i = 0; i < _points.Length; i++)
            {
                _points[i].triggered += OnTrackPointTriggered;
            }

            _points[0].AssignAsTarget();
        }      

        private void OnDestroy()
        {
            for (int i = 0; i < _points.Length; i++)
            {
                _points[i].triggered -= OnTrackPointTriggered;
            }
        }

        private void OnTrackPointTriggered(TrackPoint trackPoint)
        {
            if (trackPoint.isTarget == false) return;

            trackPoint.Passed();
            trackPoint.next?.AssignAsTarget();

            TrackPointTriggered?.Invoke(trackPoint);

            if (trackPoint.isLast == true)
            {
                _lapsComplited++;

                if (_trackType == TrackType.Sprint)
                    LapCompleted?.Invoke(_lapsComplited);

                if (_trackType == TrackType.Circular)
                    if (_lapsComplited > 0)
                        LapCompleted?.Invoke(_lapsComplited);
            }
        }

        [ContextMenu(nameof(BuildCircuit))]
        private void BuildCircuit()
        {
            _points = TrackCircultBuilder.Build(transform,_trackType);
        }
    }
}

