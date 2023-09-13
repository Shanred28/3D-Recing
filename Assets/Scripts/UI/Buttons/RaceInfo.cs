using UnityEngine;

namespace RaceUI
{
    [CreateAssetMenu]
    public class RaceInfo : ScriptableObject
    {
        [SerializeField] private string _sceneName;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _title;

        public string SceneName => _sceneName;
        public Sprite Icon => _icon;
        public string Title => _title;
    }
}

