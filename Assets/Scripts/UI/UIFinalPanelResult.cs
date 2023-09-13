using Race;
using TMPro;
using UnityEngine;

public class UIFinalPanelResult : MonoBehaviour, IDependency<RaceResultTime>
{
    [SerializeField] private TMP_Text _recordText;
    [SerializeField] private TMP_Text _curreentRaceTimeText;

    private RaceResultTime _resultTime;
    public void Construct(RaceResultTime obj) => _resultTime = obj;

    private void Start()
    {
        _resultTime.UpdateRecords += OnResultPanel;
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _resultTime.UpdateRecords -= OnResultPanel;
    }

    private void OnResultPanel()
    {
        gameObject.SetActive(true);
        if (_resultTime.PlayerRecordTime > _resultTime.GoldTime || _resultTime.IsRecordWasSet == false)
        {
            _recordText.text = StringTime.SecondToTimeString(_resultTime.GoldTime);
        }
        else
            _recordText.text = StringTime.SecondToTimeString(_resultTime.PlayerRecordTime);
        _curreentRaceTimeText.text = StringTime.SecondToTimeString(_resultTime.CurrentTime);
    }
}
