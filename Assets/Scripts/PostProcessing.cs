using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Race
{
    public class PostProcessing : MonoBehaviour
    {
        [SerializeField] private Car _car;
        [SerializeField] private PostProcessVolume _postProcessing;
        [SerializeField] private Vignette _vignette;

        private void Start()
        {
            _postProcessing = GetComponent<PostProcessVolume>();
            _vignette = ScriptableObject.CreateInstance<Vignette>();
            _vignette.enabled.Override(true);
            _vignette.intensity.Override(1f);
            _postProcessing = PostProcessManager.instance.QuickVolume(gameObject.layer, 100f, _vignette);
        }

        private void Update()
        {
            _vignette.intensity.value = Mathf.Lerp(0, 0.486f, _car.NormalizeVelocity);
        }
    }
}

