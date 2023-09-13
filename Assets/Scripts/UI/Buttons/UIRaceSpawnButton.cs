using UnityEngine;

namespace RaceUI
{
    public class UIRaceSpawnButton : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private UIRaceButton _uiRaceButtonPrefab;
        [SerializeField] private RaceInfo[] _properties;

        [ContextMenu(nameof(Spawn))]
        public void Spawn()
        {
            if (Application.isPlaying == true) return;

            GameObject[] allObj = new GameObject[_parent.childCount];

            for (int i = 0; i < _parent.childCount; i++)
            {
                allObj[i] = _parent.GetChild(i).gameObject;
            }

            for (int i = 0; i < allObj.Length; i++)
            {
                DestroyImmediate(allObj[i]);
            }

            for (int i = 0; i < _properties.Length; i++)
            {
                UIRaceButton button = Instantiate(_uiRaceButtonPrefab, _parent);
                button.ApllyProperty(_properties[i]);
            }
        }
    }
}

