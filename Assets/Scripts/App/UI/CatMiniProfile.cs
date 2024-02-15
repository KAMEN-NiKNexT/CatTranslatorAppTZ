using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kamen.DataSave;
using CatTranslator.Save;
using CatTranslator.Control;

namespace CatTranslator.UI
{
    public class CatMiniProfile : MonoBehaviour
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            DataSaveManager.Instance.MyData.OnCurrentProfileChanged += UpdateProfileView;
            SetUpView(DataSaveManager.Instance.MyData.CatProfilesData[DataSaveManager.Instance.MyData.CurrentProfileIndex]);
        }
        private void UpdateProfileView(int oldProfileIndex, int newProfileIndex)
        {
            SetUpView(DataSaveManager.Instance.MyData.CatProfilesData[newProfileIndex]);
        }
        private void SetUpView(CatProfileData catProfileData)
        {
            _icon.sprite = CatIconLoader.Instance.LoadIcon(catProfileData);
            _nameText.text = catProfileData.Name;
        }

        #endregion
    }
}