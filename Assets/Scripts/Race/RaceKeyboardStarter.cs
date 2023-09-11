using UnityEngine;

namespace Race
{
    public class RaceKeyboardStarter : MonoBehaviour
    {
       [SerializeField] private RaceStateTracker _raceStateTracker;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) == true)
                _raceStateTracker.LaunchPreparationStart();
        }
    }
}

