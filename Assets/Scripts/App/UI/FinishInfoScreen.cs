using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using UnityEngine.UI;
using Kamen.DataSave;

namespace CatTranslator.UI
{
    public class FinishInfoScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Button _startLimitedVersionButton;
        [SerializeField] private Button _subscribeAndContinueButton;

        [Header("Settings")]
        [SerializeField] private string _baseScreenName;
        [SerializeField] private string _controlBarMenu;

        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _startLimitedVersionButton.onClick.RemoveListener(StartLimitedVersion);
            _subscribeAndContinueButton.onClick.RemoveListener(SubscribeAndContinue);
        }

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();
            SetUpButtons();
        }
        private void SetUpButtons()
        {
            _startLimitedVersionButton.onClick.AddListener(StartLimitedVersion);
            _subscribeAndContinueButton.onClick.AddListener(SubscribeAndContinue);
        }
        private void StartLimitedVersion()
        {
            Succeded();
        }
        private void SubscribeAndContinue()
        {
            PurchasesListener.Instance.PurchaseSubscription("test", Succeded);         
        }
        private void Succeded()
        {
            ScreenManager.Instance.TransitionTo(_baseScreenName);
            ControlBar.Instance.Show();
            ControlBar.Instance.ChangeCurrentMenu(_controlBarMenu);

            FinishStartScreens();
        }
        private void FinishStartScreens()
        {
            DataSaveManager.Instance.MyData.IsStartScreensShowed = true;
            DataSaveManager.Instance.SaveData();
        }

        #endregion
    }
}