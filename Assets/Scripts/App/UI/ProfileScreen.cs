using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using System;
using CatTranslator.Save;
using Kamen.DataSave;

namespace CatTranslator.UI
{
    public class ProfileScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private CatProfilePanel _catProfilePanelPrefab;

        [Header("Objects")]
        [SerializeField] private AddNewCatPopup _addNewCatPopup;
        [SerializeField] private GameObject _catProfilesHolder;

        [Header("Variables")]
        private List<CatProfilePanel> _catProfilePanels = new List<CatProfilePanel>();

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();

            _addNewCatPopup.OnAddedNewCatProfile += CreateNewCatProfile;
            DataSaveManager.Instance.MyData.OnCurrentProfileChanged += ChangeCatProfileView;
            FillProfilesHolder();
        }
        private void FillProfilesHolder()
        {
            for (int i = 0; i < DataSaveManager.Instance.MyData.CatProfilesData.Count; i++)
            {
                SpawnCatProfilePanel(DataSaveManager.Instance.MyData.CatProfilesData[i]);
            }

            _catProfilePanels[DataSaveManager.Instance.MyData.CurrentProfileIndex].CallChangeState();
        }
        private void CreateNewCatProfile(CatProfileData catProfileData)
        {
            DataSaveManager.Instance.MyData.CatProfilesData.Add(catProfileData);
            SpawnCatProfilePanel(catProfileData);

            DataSaveManager.Instance.MyData.CurrentProfileIndex = _catProfilePanels.Count - 1;
            DataSaveManager.Instance.SaveData();
        }
        private void SpawnCatProfilePanel(CatProfileData catProfileData)
        {
            CatProfilePanel newPanel = Instantiate(_catProfilePanelPrefab, _catProfilesHolder.transform);
            newPanel.Initialize();
            newPanel.SetUpProfileInfo(catProfileData);
            newPanel.OnChoosed += CallToChangeCatProfile;
            _catProfilePanels.Add(newPanel);
        }
        private void CallToChangeCatProfile(CatProfileData catProfileData)
        {
            int newIndex = DataSaveManager.Instance.MyData.CatProfilesData.IndexOf(catProfileData);
            Debug.Log(newIndex);
            DataSaveManager.Instance.MyData.CurrentProfileIndex = newIndex;
        }

        private void ChangeCatProfileView(int oldPanelIndex, int newPanelIndex)
        {
            _catProfilePanels[oldPanelIndex].CallChangeState();
            _catProfilePanels[newPanelIndex].CallChangeState();
        }

        #endregion
    }
}