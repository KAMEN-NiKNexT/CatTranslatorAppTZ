using Kamen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CatTranslator.Audio
{
    public class AudioRecorder : SingletonComponent<AudioRecorder>
    {
        #region Enums

        public enum RecorderState
        {
            Active,
            Inactive,
            Preparation,
        }

        #endregion

        #region Variables

        [Header("Settings")]
        [SerializeField] private int _maxTimeRecord;

        [Header("Control Variables")]
        private RecorderState _state = RecorderState.Inactive;
        private AudioClip _audioClip;

        [Header("Record Variables")]
        private List<float> _samplesDataList = new List<float>();
        private float[] _samplesData;
        private int _recordedSamples;
        private float _currentTimeRecord;

        [Header("Help Variables")]
        private Coroutine _timerToSaveAfterLimitCoroutine;
        private Coroutine _timeRecordControlCoroutine;
        public event Action<RecorderState> OnRecordingStateChanged;
        public event Action<AudioClip, float[]> OnRecordClipReady;

        #endregion

        #region Properties

        public RecorderState State { get => _state; }

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();

            InitializeMicrophone();
        }

        #endregion

        #region Control Methods

        private void InitializeMicrophone()
        {
        }


        public void StartRecord()
        {
            if (_state != RecorderState.Inactive) return;
            _state = RecorderState.Preparation;

            _timerToSaveAfterLimitCoroutine = StartCoroutine(TimerToSaveAfterLimit(_maxTimeRecord));
            _timeRecordControlCoroutine = StartCoroutine(TimeRecordControl());
            Microphone.End(Microphone.devices[0]);
            _audioClip = Microphone.Start(Microphone.devices[0], false, _maxTimeRecord, 44100);

            _state = RecorderState.Active;
            OnRecordingStateChanged?.Invoke(RecorderState.Active);
        }
        public void StopRecord()
        {
            if (_state != RecorderState.Active) return;
            _state = RecorderState.Preparation;

            StartCoroutine(StopRecordCoroutine());
        }
        private IEnumerator StopRecordCoroutine()
        {
            if (_timeRecordControlCoroutine != null) StopCoroutine(_timeRecordControlCoroutine);
            if (_timerToSaveAfterLimitCoroutine != null) StopCoroutine(_timerToSaveAfterLimitCoroutine);

            while (!(Microphone.GetPosition(null) > 0))
            {
                yield return null;
                Debug.LogWarning($"[AudioRecorder] - Problem with microphone");
            }

            _samplesData = new float[_audioClip.samples * _audioClip.channels];
            _audioClip.GetData(_samplesData, 0);

            _samplesDataList.Clear();
            _samplesDataList = _samplesData.ToList();
            _recordedSamples = (int)(_samplesData.Length * (_currentTimeRecord / (float)_maxTimeRecord));

            if (_recordedSamples < _samplesData.Length - 1)
            {
                _samplesDataList.RemoveRange(_recordedSamples, _samplesData.Length - _recordedSamples);
                _samplesData = _samplesDataList.ToArray();
            }

            OnRecordClipReady?.Invoke(_audioClip, _samplesData);
            Microphone.End(Microphone.devices[0]);

            _state = RecorderState.Inactive;
            OnRecordingStateChanged?.Invoke(RecorderState.Inactive);
        }

        #endregion

        #region Calculation Methods

        private IEnumerator TimerToSaveAfterLimit(float duration)
        {
            yield return new WaitForSeconds(duration);
            if (_state == RecorderState.Inactive) yield break;

            StopRecord();
        }
        private IEnumerator TimeRecordControl()
        {
            _currentTimeRecord = 0;

            while (true)
            {
                yield return null;
                _currentTimeRecord += Time.deltaTime;
            }
        }

        #endregion
    }
}