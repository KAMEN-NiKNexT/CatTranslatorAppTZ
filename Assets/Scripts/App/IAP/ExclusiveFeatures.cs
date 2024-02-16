using Kamen.DataSave;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CatTranslator.IAP
{
    [RequireComponent(typeof(Button))]
    public class ExclusiveFeatures : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TextMeshProUGUI _timerView;

        [Header("Settings")]
        [SerializeField] private int _offerTime;
        [SerializeField] private string _subscribeName;

        [Header("Variables")]
        private float _timeFromStartTimer;
        private bool _isTimerFinished;

        private int _fullTime;
        private int _viewMinutes;
        private int _viewSeconds;

        private Button _button;

        public event Action OnFeaturesStoped;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }
        private void Update()
        {
            if (DataSaveManager.Instance.MyData.IsFeaturesTimerStoped || DataSaveManager.Instance.MyData.IsSubscribed) return;

            _timeFromStartTimer += Time.deltaTime;
            UpdateView();
            if (_timeFromStartTimer >= _offerTime) CallStopFeautures();
        }
        private void OnEnable()
        {
            if (DataSaveManager.Instance.MyData.IsFeaturesTimerStoped || DataSaveManager.Instance.MyData.IsSubscribed) gameObject.SetActive(false);
        }
        private void OnApplicationQuit()
        {
            WriteTimeInDataBase();
        }
        private void OnApplicationPause(bool pause)
        {
            if (pause) WriteTimeInDataBase();
        }
        private void OnApplicationFocus(bool focus)
        {
            if (!focus) WriteTimeInDataBase();
        }
        private void OnDestroy()
        {
            _button.onClick.RemoveListener(GetExclusiveFeature);
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            _timeFromStartTimer = DataSaveManager.Instance.MyData.TimeFromStartTimer;
            _button = GetComponent<Button>();
            _button.onClick.AddListener(GetExclusiveFeature);
        }
        private void UpdateView()
        {
            _fullTime = Mathf.RoundToInt(_offerTime - _timeFromStartTimer);
            _viewMinutes = _fullTime / 60;
            _viewSeconds = _fullTime - _viewMinutes * 60;
            string timerText = (_viewMinutes < 10 ? "0" + _viewMinutes : "" + _viewMinutes) + ":" +  (_viewSeconds < 10 ? "0" + _viewSeconds : "" + _viewSeconds);
            _timerView.text = timerText;
        }
        private void GetExclusiveFeature()
        {
            PurchasesListener.Instance.PurchaseSubscriptionWithDiscount(_subscribeName, CallStopFeautures);
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