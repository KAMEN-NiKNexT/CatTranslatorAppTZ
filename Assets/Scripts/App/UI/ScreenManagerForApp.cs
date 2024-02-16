using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kamen.UI;
using Kamen.DataSave;

namespace CatTranslator.UI
{
    public class ScreenManagerForApp : ScreenManager
    {
        #region Variables

        [SerializeField] private string _baseScreenName;

        #endregion

        #region Control Methods

        protected override void Initialize()
        {
            for (int i = 0; i < _screenInfos.Length; i++)
            {
                _screenInfos[i].ThisScreen.Initialize();
            }
            _state = State.Standing;

            if (!DataSaveManager.Instance.MyData.IsStartScreensShowed)
            {
                FastTransitionTo(_startScreen);
                ControlBar.Instance.gameObject.SetActive(false);
            }
            else
            {
                FastTransitionTo(_baseScreenName);
                ControlBar.Instance.gameObject.SetActive(true);
            }
        }

        #endregion
    }
}