using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float interval = 1f;
    public TextMeshProUGUI timerText;
    private IDisposable timer;

    void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        var startTime = DateTime.Now;
        Observable.Interval(TimeSpan.FromSeconds(1))
            .Subscribe(_ => {
                var elapsed = DateTime.Now - startTime;
                timerText.text = $" Time {elapsed.Minutes}:{elapsed.Seconds}";
            });
    }
    

    public void StopTimer()
    {
        
    }
}