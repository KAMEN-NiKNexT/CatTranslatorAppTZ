using CatTranslator.Save;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CatTranslator.UI
{
    public class GenderButtonComponent : ButtonComponent
    {
        #region Variables

        [SerializeField] private CatProfileData.CatGender _choosenGender;

        #endregion

        #region Properties

        public CatProfileData.CatGender ChoosenGender { get => _choosenGender; }

        #endregion

        #region SetUp Methods

        public virtual void AddMethodForGenderButtonClick(UnityAction<CatProfileData.CatGender> callback) => _button.onClick.AddListener(() => callback(_choosenGender));
        public virtual void RemoveMethodGenderForButtonClick(UnityAction<CatProfileData.CatGender> callback) => _button.onClick.RemoveListener(() => callback(_choosenGender));

        #endregion
    }
}