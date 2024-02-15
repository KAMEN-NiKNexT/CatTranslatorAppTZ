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
        [SerializeField] private List<CatProfileData> _catProfilesData = new List<CatProfileData>();
        [SerializeField] private int _currentProfileIndex;
        public event Action<int, int> OnCurrentProfileChanged;

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

        #endregion
    }
}