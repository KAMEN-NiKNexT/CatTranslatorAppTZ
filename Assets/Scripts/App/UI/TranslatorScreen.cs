using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using Kamen.DataSave;
using CatTranslator.IAP;
using UnityEngine.UI;
using TMPro;
using CatTranslator.Control;
using CatTranslator.Audio;

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

        [Header("Variables")]
        private bool _isCatFounded;

        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void Start()
        {
            AIManager.Instance.OnCatNoticed += DecodeAIAnswer;
            AudioRecorder.Instance.OnRecordingStateChanged += RecordViewManager;
        }

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
        private void DecodeAIAnswer(bool isHaveCatVoice)
        {
            if (_isCatFounded) return;

            if (isHaveCatVoice)
            {
                ObjectsManage(TranslatorScreenState.RecordingWithSound);
                _isCatFounded = true;
            }
            else ObjectsManage(TranslatorScreenState.RecordingWithoutSound);
        }
        private void RecordViewManager(AudioRecorder.RecorderState state)
        {
            if (state == AudioRecorder.RecorderState.Active)
            {
                _isCatFounded = false;
                ObjectsManage(TranslatorScreenState.RecordingWithoutSound);
            }
            else if (state == AudioRecorder.RecorderState.Inactive)
            {
                if (_isCatFounded)
                {
                    PopupManager.Instance.Show("RecognitionPopup");
                }
                else
                {
                    PopupManager.Instance.Show("TryAgainPopup");
                    //ObjectsManage(TranslatorScreenState.UndoRecoginzing);
                }
            }
        }
        public void CallShowResalt()
        {
            if (DataSaveManager.Instance.MyData.IsSubscribed) PopupManager.Instance.Show("ReadyTranslationPopup");
            else PopupManager.Instance.Show("LockedTranslationPopup");
        }
        public void CallStandingMode() => ObjectsManage(TranslatorScreenState.Standing);

        #endregion
    }
}

