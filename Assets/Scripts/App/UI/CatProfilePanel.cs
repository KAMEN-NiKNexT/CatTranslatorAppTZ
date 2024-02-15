using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using CatTranslator.Save;
using CatTranslator.Control;

namespace CatTranslator.UI
{
    public class CatProfilePanel : ButtonComponent
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _icon;

        [Header("Variables")]
        private CatProfileData _catProfileData;
        public event Action<CatProfileData> OnChoosed;

        #endregion

        #region Control Methods

        public void SetUpProfileInfo(CatProfileData catProfileData)
        {
            _catProfileData = catProfileData;

            _icon.sprite = CatIconLoader.Instance.LoadIcon(_catProfileData);
            _text.text = _catProfileData.Name;

            _button.onClick.AddListener(Click);
        }
        private void Click()
        {
            OnChoosed?.Invoke(_catProfileData);
        }

        #endregion
    }
}