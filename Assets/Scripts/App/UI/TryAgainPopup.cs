using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using UnityEngine.UI;
using CatTranslator.Audio;

namespace CatTranslator.UI
{
    public class TryAgainPopup : Popup
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private TranslatorScreen _translatorScreen;
        [SerializeField] private Button _tryAgainButton;
        [SerializeField] private Button _toMainPageButton;

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();

            _tryAgainButton.onClick.AddListener(CallTryAgain);
            _toMainPageButton.onClick.AddListener(CallToMainPage);
        }
        private void CallTryAgain()
        {
            PopupManager.Instance.Hide("TryAgainPopup");
            AudioRecorder.Instance.StartRecord();
        }
        private void CallToMainPage()
        {
            PopupManager.Instance.Hide("TryAgainPopup");
            _translatorScreen.CallStandingMode();
        }

        #endregion
    }
}