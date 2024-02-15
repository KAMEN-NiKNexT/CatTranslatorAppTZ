using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using CatTranslator.IAP;
using UnityEngine.UI;
using TMPro;

namespace CatTranslator.UI
{
    public class TranslatorScreen : Kamen.UI.Screen
    {
        #region Enums

        public enum TranslatorScreenState
        {
            Standing,
            RecordingWithoutSound,
            RecordingWithSound,
            UndoRecoginzing,
            ConfirmRecogning,
            ShowingResult,
            ShowingSubscribePopup
        }

        #endregion

        #region Variables

      //  [Header("Objects")]


        [Header("Standing State Objects")]
        [SerializeField] private ExclusiveFeatures _exclusiveFeature;
        [SerializeField] private CatMiniProfile _miniProfile;
        [SerializeField] private Button _recordButton;

        [Header("Recording Objects")]
        [SerializeField] private GameObject _recordView;
        [SerializeField] private Image _soundLineWithSound;
        [SerializeField] private Image _soundLineWithoutSound;
        [SerializeField] private TextMeshProUGUI _noDetectedText;
        [SerializeField] private Button _stopRecordingButton;


        #endregion

        #region Properties



        #endregion

        #region Control Methods

        private void ObjectsManage(TranslatorScreenState currentState)
        {
            _exclusiveFeature.gameObject.SetActive(currentState == TranslatorScreenState.Standing);
            _miniProfile.gameObject.SetActive(currentState == TranslatorScreenState.Standing);
            _recordButton.gameObject.SetActive(currentState == TranslatorScreenState.Standing);

            if (currentState == TranslatorScreenState.Standing) ControlBar.Instance.Show();
            else ControlBar.Instance.Hide();

            _recordView.gameObject.SetActive(currentState == TranslatorScreenState.RecordingWithSound || currentState == TranslatorScreenState.RecordingWithoutSound);
            _soundLineWithSound.gameObject.SetActive(currentState == TranslatorScreenState.RecordingWithSound);
            _soundLineWithoutSound.gameObject.SetActive(currentState == TranslatorScreenState.RecordingWithoutSound);
            _noDetectedText.gameObject.SetActive(currentState == TranslatorScreenState.RecordingWithoutSound);
            _stopRecordingButton.gameObject.SetActive(currentState == TranslatorScreenState.RecordingWithSound || currentState == TranslatorScreenState.RecordingWithoutSound);
        }
        public void OpenRecordView()
        {
            ObjectsManage(TranslatorScreenState.RecordingWithoutSound);
        }
        public void CallStandingMode() => ObjectsManage(TranslatorScreenState.Standing);

        #endregion
    }
}

