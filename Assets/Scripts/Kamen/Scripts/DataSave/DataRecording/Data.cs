using System;
using UnityEngine;
using System.Collections.Generic;
using CatTranslator.Save;

namespace Kamen.DataSave
{
    [Serializable] public class Data
    {
        #region Variables

        [SerializeField] private List<TimerInfo> _timersInfo = new List<TimerInfo>();
        [SerializeField] private DateTime _quitTime;
        [Space]
        [SerializeField] private List<CatProfileData> _catProfilesData = new List<CatProfileData>(1) { new CatProfileData("", "Unknown cat", 0, "", CatProfileData.CatGender.Male, 0) };
        [SerializeField] private int _currentProfileIndex;
        public event Action<int, int> OnCurrentProfileChanged;
        [Space]
        [SerializeField] private float _timeFromStartFeaturesTimer;
        [SerializeField] private bool _isFeaturesTimerStoped;
        [Space]
        [SerializeField] private bool _isStartScreensShowed;
        [SerializeField] private bool _isSubcribed;

        public Action OnDataChanged;

        #endregion

        #region Properties

        public List<TimerInfo> TimersInfo { get => _timersInfo; }
        public DateTime QuitTime 
        { 
            get => _quitTime;
            set 
            {
                if (value != null) _quitTime = value;
            }
        }

        public List<CatProfileData> CatProfilesData { get => _catProfilesData; }
        public int CurrentProfileIndex
        {
            get => _currentProfileIndex;
            set
            {
                if (value >= 0 && value <= _catProfilesData.Count - 1)
                {
                    OnCurrentProfileChanged?.Invoke(_currentProfileIndex, value);
                    _currentProfileIndex = value;
                }
            }
        }

        public float TimeFromStartTimer 
        {
            get => _timeFromStartFeaturesTimer;
            set
            {
                if (value < 0) return;
                _timeFromStartFeaturesTimer = value;
            }
        }
        public bool IsFeaturesTimerStoped
        {
            get => _isFeaturesTimerStoped;
            set
            {
                if (!value) return;
                _isFeaturesTimerStoped = value;
            }
        }

        public bool IsStartScreensShowed
        {
            get => _isStartScreensShowed;
            set
            {
                if (!value) return; 
                _isStartScreensShowed = value;
            }
        }
        public bool IsSubscribed
        {
            get => _isSubcribed;
            set { _isSubcribed = true; }
        }

        #endregion
    }
}