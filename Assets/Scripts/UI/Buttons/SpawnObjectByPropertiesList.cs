using UnityEngine;

namespace RaceUI
{
    public class SpawnObjectByPropertiesList : MonoBehaviour
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private ScriptableObject[] _properties;

        [ContextMenu(nameof(SpawnInEditMode))]
        public void SpawnInEditMode()
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
                GameObject go = Instantiate(_prefab, _parent);
                IScriptableObjectProperty scriptableObjectProperty = go.GetComponent<IScriptableObjectProperty>();
                scriptableObjectProperty.ApplyProperty(_properties[i]);
            }
        }
    }
}

