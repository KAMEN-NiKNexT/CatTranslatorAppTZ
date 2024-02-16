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
        [SerializeField] private string _controlBarMenu;

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
                ControlBar.Instance.Hide();
            }
            else
            {
                FastTransitionTo(_baseScreenName);
                ControlBar.Instance.Show();
                ControlBar.Instance.ChangeCurrentMenu(_controlBarMenu);
            }
        }

        #endregion
    }
}