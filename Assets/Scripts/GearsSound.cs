using UnityEngine;

namespace Race
{
    public class GearsSound : MonoBehaviour
    {
        [SerializeField] private Car _car;

        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            _car.GearChange += GearSwitch;
        }

        private void OnDestroy()
        {
            _car.GearChange -= GearSwitch;
        }
        private void GearSwitch(string gearName)
        {

            _audioSource.Play();
        }
    }
}

