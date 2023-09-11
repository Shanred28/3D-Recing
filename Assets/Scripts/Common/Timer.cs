using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public event UnityAction eventFinishedTimer;

    [SerializeField] private float _time;

    private float _value;

    public float Value => _value;

    private void Start()
    {
        _value = _time;
    }

    private void Update()
    {
        _value -= Time.deltaTime;

        if (Value <= 0)
        { 
            enabled = false;

            eventFinishedTimer?.Invoke();
        }
    }
}
