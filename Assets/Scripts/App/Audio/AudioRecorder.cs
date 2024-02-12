using Kamen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using System.Linq;

namespace CatTranslator.Audio
{
    public class AudioRecorder : SingletonComponent<AudioRecorder>
    {
        #region Enums

        public enum RecorderState
        {
            Active,
            Inactive,
            Preparation
        }

        #endregion

        #region Variables

        [Header("Variables")]
        private RecorderState _state = RecorderState.Inactive;
        private AudioClip _currentAudio;

        #endregion

        #region Properties
        
        public RecorderState State { get => _state; }

        #endregion

        #region Unity Methods


        #endregion

        #region Control Methods

        public void StartRecord()
        {
            if (_state != RecorderState.Inactive) return;
            _state = RecorderState.Preparation;

            var enumerator = new MMDeviceEnumerator();
            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
            
           // var enumerator = new MMD

            //_currentAudio = Microphone.Start(null, false, 1, 44100);
            //_state = RecorderState.Active;
        }
        public void StopRecord()
        {
            if (_state != RecorderState.Active) return;
            _state = RecorderState.Preparation;

            Microphone.GetPosition(null);

            Microphone.End(null);
           // _currentAudio.GetData()
                SaveRecord();
            //_state = RecorderState.Inactive;
        }

        public void SaveRecord()
        {
            SavWav.Save(Application.persistentDataPath + "a.mp3", _currentAudio);
        }
        public void LoadRecord()
        {

        }

        #endregion
    }
}