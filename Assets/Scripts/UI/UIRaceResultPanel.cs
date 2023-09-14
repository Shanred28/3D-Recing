using Race;
using TMPro;
using UnityEngine;

namespace RaceUI
{
    public class UIRaceResultPanel : MonoBehaviour, IDependency<RaceResultTime>
    {
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private TMP_Text _recordTime;
        [SerializeField] private TMP_Text _currentTime;

        private RaceResultTime _raceResultTime;
        public void Construct(RaceResultTime obj) => _raceResultTime = obj;

        private void Start()
        {
            _resultPanel.SetActive(false);
            _raceResultTime.UpdateRecords += OnUpdateResult;
        }

        private void OnDestroy()
        {
            _raceResultTime.UpdateRecords -= OnUpdateResult;
        }

        private void OnUpdateResult()
        {
            _resultPanel.SetActive(true);
            _recordTime.text = StringTime.SecondToTimeString(_raceResultTime.GetAbsolureeRecord());
            _currentTime.text = StringTime.SecondToTimeString(_raceResultTime.CurrentTime);
        }
    }
}

