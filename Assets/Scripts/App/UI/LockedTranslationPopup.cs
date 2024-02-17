using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
//using CatTranslator.SaveData;
using UnityEngine.UI;

namespace CatTranslator.UI
{
    public class LockedTranslationPopup : Popup
    {
        #region Variables

        [SerializeField] private Button _subscribeButton;
        [SerializeField] private TranslatorScreen _translatorScreen;

        #endregion

        #region Properties



        #endregion

        public override void Initialize()
        {
            base.Initialize();

            _subscribeButton.onClick.AddListener(CallSubscribe);
        }

        #region Control  Methods

        public void CallSubscribe()
        {
            PurchasesListener.Instance.PurchaseSubscription("test", ShowResult);
        }
        private void ShowResult()
        {
            PopupManager.Instance.Hide("LockedTranslationPopup");
            _translatorScreen.CallShowResalt();
        }

        #endregion
    }
}