using System;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public enum Phase { Day, Night }

    [SerializeField] float dayDuration = 60f;  
    [SerializeField] Light sun;                 
    [SerializeField] float dayIntensity = 1f;
    [SerializeField] float nightIntensity = 0.1f;

    public Phase Current { get; private set; } = Phase.Day;
    public event Action OnDayStarted;
    public event Action OnNightStarted;

    float timer;

    void Start() => StartDay();

    void Update()
    {
        if (Current != Phase.Day) return;

        timer += Time.deltaTime;
        if (timer >= dayDuration) StartNight();
    }

    void StartDay()
    {
        timer = 0f;
        Current = Phase.Day;
        if (sun) sun.intensity = dayIntensity;
        OnDayStarted?.Invoke();
    }

    void StartNight()
    {
        Current = Phase.Night;
        if (sun) sun.intensity = nightIntensity;
        OnNightStarted?.Invoke();
    }
    public void EndNight()
    {
        if (Current == Phase.Night) StartDay();
    }
}