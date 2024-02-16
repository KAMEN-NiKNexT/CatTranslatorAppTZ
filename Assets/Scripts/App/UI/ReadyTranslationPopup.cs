using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using CatTranslator.Audio;
using UnityEngine.UI;
using Kamen.DataSave;

namespace CatTranslator.UI
{
    public class ReadyTranslationPopup : Popup
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private TranslatorScreen _translatorScreen;

        [Header("Vaiables")]
        private string _currentPath;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(RestartRecord);
            _saveButton.onClick.RemoveListener(SaveTranslation);
        }

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();

            SetUpsButton();
            AudioKeeper.Instance.OnSavedWithName += WritePath;
        }
        private void SetUpsButton()
        {
            _restartButton.onClick.AddListener(RestartRecord);
            _saveButton.onClick.AddListener(SaveTranslation);
        }
        private void RestartRecord()
        {
            PopupManager.Instance.Hide("ReadyTranslationPopup");
            AudioRecorder.Instance.StartRecord();
        }
        private void SaveTranslation()
        {
            DataSaveManager.Instance.MyData.CatProfilesData[DataSaveManager.Instance.MyData.CurrentProfileIndex].AudioFileNames.Add(_currentPath);
            PopupManager.Instance.Hide("ReadyTranslationPopup");
            _translatorScreen.CallStandingMode();
        }
        private void WritePath(string path) => _currentPath = path;

        #endregion
    }
}