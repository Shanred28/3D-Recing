using UnityEngine;

namespace Race
{
    public static class TrackCircultBuilder
    {
        public static TrackPoint[] Build(Transform trackTransform, TrackType type)
        {
            TrackPoint[] _points = new TrackPoint[trackTransform.childCount];

            ResetPoints(trackTransform, _points);
            MakeLinks(_points, type);
            MarkPoint(_points, type);
            
            return _points;
        }

        private static void ResetPoints(Transform trackTransform, TrackPoint[] points)
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = trackTransform.GetChild(i).GetComponent<TrackPoint>();

                if (points[i] == null)
                {
                    Debug.LogError("There is no TrackPoint script on one of the child objects");
                    return;
                }
                points[i].Reset();
            }
        }

        private static void MakeLinks(TrackPoint[] points, TrackType type)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                points[i].next = points[i + 1];
            }

            if (type == TrackType.Circular)
            {
                points[points.Length - 1].next = points[0];
            }
        }

        private static void MarkPoint(TrackPoint[] points, TrackType type)
        {
            points[0].isFerst = true;

            if (type == TrackType.Sprint)
                points[points.Length - 1].isLast = true;

            if (type == TrackType.Circular)
                points[0].isLast = true;
        }
    }
}

