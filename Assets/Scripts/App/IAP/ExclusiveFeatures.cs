using Kamen.DataSave;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CatTranslator.IAP
{
    public class ExclusiveFeatures : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _timerView;

        [Header("Settings")]
        [SerializeField] private int _offerTime;

        [Header("Variables")]
        private float _timeFromStartTimer;
        private bool _isTimerFinished;

        private int _fullTime;
        private int _viewMinutes;
        private int _viewSeconds;

        public event Action OnFeaturesStoped;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }
        private void Update()
        {
            if (DataSaveManager.Instance.MyData.IsFeaturesTimerStoped) return;

            _timeFromStartTimer += Time.deltaTime;
            UpdateView();
            if (_timeFromStartTimer >= _offerTime) CallStopFeautures();
        }
        private void OnEnable()
        {
            if (DataSaveManager.Instance.MyData.IsFeaturesTimerStoped) gameObject.SetActive(false);
        }
        private void OnApplicationQuit()
        {
            WriteTimeInDataBase();
        }
        private void OnApplicationPause(bool pause)
        {
            WriteTimeInDataBase();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _timeFromStartTimer = DataSaveManager.Instance.MyData.TimeFromStartTimer;
        }
        private void UpdateView()
        {
            _fullTime = Mathf.RoundToInt(_offerTime - _timeFromStartTimer);
            _viewMinutes = _fullTime / 60;
            _viewSeconds = _fullTime - _viewMinutes * 60;
            string timerText = (_viewMinutes < 10 ? "0" + _viewMinutes : "" + _viewMinutes) + ":" +  (_viewSeconds < 10 ? "0" + _viewSeconds : "" + _viewSeconds);
            _timerView.text = timerText;
        }
        private void WriteTimeInDataBase()
        {
            DataSaveManager.Instance.MyData.TimeFromStartTimer = _timeFromStartTimer;
            DataSaveManager.Instance.SaveData();
        }
        private void CallStopFeautures()
        {
            DataSaveManager.Instance.MyData.IsFeaturesTimerStoped = true;
            DataSaveManager.Instance.SaveData();

            gameObject.SetActive(false);
            OnFeaturesStoped?.Invoke();
        }

        #endregion
    }
}