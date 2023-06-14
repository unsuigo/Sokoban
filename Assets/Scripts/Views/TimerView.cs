using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace Sokoban.View
{
    
    public class TimerView : MonoBehaviour
    {
        public event Action Finished;
        public event Action LastSecondsStarted;
        public event Action Updated;
        
        [SerializeField] private TMP_Text _label;
        private const string FocusLossTimeKey = "focus-lose-time-key";
        private const int TimerUpdateTime = 1;
        
        private RectTransform _rectTs;
        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private int _totalSeconds;
        private int _lastSecondsCount;
        
        private void Awake()
        {
            _rectTs = GetComponent<RectTransform>();
        }
       
        private void OnDisable()
        {
            _compositeDisposable?.Dispose();
            _totalSeconds = 0;
        }
        private void OnDestroy()
        {
            UnsubscribeFinishEvent();
        }
        
        public void UnsubscribeFinishEvent()
        {
            if(Finished == null) return;
            
            Delegate[] subscribers = Finished.GetInvocationList();
            for(int i = 0; i < subscribers.Length; i++)    
            {    
                Finished -= subscribers[i] as Action;
            }
        }
        
        public void SetTimer(int totalSeconds)
        {
            _lastSecondsCount = -1;
            _totalSeconds = totalSeconds;
            UpdateLabel();
            _compositeDisposable?.Dispose();
            if (_totalSeconds > 0)
            {
                SubscribeToTimer();
            }
        }
        
        public void SetTimer(int totalSeconds, int lastSecondsCount)
        {
            SetTimer(totalSeconds);
            _lastSecondsCount = lastSecondsCount;
        }
        
        public void AddToTimer(int seconds)
        {
            _totalSeconds += seconds;
            SetTimer(_totalSeconds);
        }
        
        public void SetActive(bool isVisible)
        {
            gameObject.SetActive(isVisible);
            SetDescriptionActive(false);
        }
        
        public void SetPosition(RectTransform rectTs)
        {
            _rectTs.anchorMin = rectTs.anchorMin;
            _rectTs.anchorMax = rectTs.anchorMax;
            _rectTs.pivot = rectTs.pivot;
            transform.localPosition = rectTs.localPosition;
        }
        private void SubscribeToTimer()
        {
            _compositeDisposable = new CompositeDisposable();
            Observable.Timer(TimeSpan.FromSeconds(TimerUpdateTime)) 
                .Repeat()
                .Subscribe(_ => {UpdateLabel();})
                .AddTo(_compositeDisposable);
        }
        
        
        private void UpdateLabel()
        {
            if (_totalSeconds > 0)
            {
                _totalSeconds -= TimerUpdateTime;
                TimeSpan timeSpan = TimeSpan.FromSeconds(_totalSeconds);
                // _label.text = DevTools.GetTimeText(0, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, " ");
                Updated?.Invoke();
                
                if (_totalSeconds <= _lastSecondsCount)
                {
                    LastSecondsStarted?.Invoke();
                    _lastSecondsCount = -1;
                }
            }
            
            if (_totalSeconds <= 0)
            {
                Finished?.Invoke();
            }
        }
        private void SetDescriptionActive(bool isActive)
        {
           
                gameObject.SetActive(isActive);
        }
        
        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                SaveFocusLossTime();
            }
            else
            {
                int outOfFocusTime = GetTimeOutOfFocus();
                if (_totalSeconds >= outOfFocusTime)
                {
                    _totalSeconds -= outOfFocusTime;
                    SetTimer(_totalSeconds);
                }
                else
                {
                    SetTimer(0);
                }
            }
        }
        
        private void SaveFocusLossTime()
        {
            PlayerPrefs.SetString(FocusLossTimeKey, DateTime.Now.ToBinary().ToString());
        }
    
        private int GetTimeOutOfFocus()
        {
            if (!PlayerPrefs.HasKey(FocusLossTimeKey))
            {
                Debug.LogWarning($"<b>{nameof(TimerView)}{nameof(GetTimeOutOfFocus)}</b> Can't find unfocused time. Key {FocusLossTimeKey} was removed from local data.");
                return 0;
            }
            long temp = Convert.ToInt64(PlayerPrefs.GetString(FocusLossTimeKey));
            DateTime focusOffDateTime = DateTime.FromBinary(temp);
            DateTime focusOnDateTime = DateTime.Now;
            TimeSpan difference = focusOnDateTime.Subtract(focusOffDateTime);
            return (int)difference.TotalSeconds;
        }
    }

}