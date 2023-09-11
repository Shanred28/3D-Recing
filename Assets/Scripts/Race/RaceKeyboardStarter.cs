using UnityEngine;

namespace Race
{
    public class RaceKeyboardStarter : MonoBehaviour, IDependency<RaceStateTracker>
    {
       private RaceStateTracker _raceStateTracker;
       public void Construct(RaceStateTracker obj) => _raceStateTracker = obj;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) == true)
                _raceStateTracker.LaunchPreparationStart();
        }
    }
}

