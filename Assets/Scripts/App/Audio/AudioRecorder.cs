using Kamen;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using System.Text;
#if UNITY_ANDROID
    using UnityEngine.Android;
#endif

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
        [SerializeField] private float _timeToSplit;

        [Header("Control Variables")]
        private RecorderState _state = RecorderState.Inactive;
        private AudioClip _audioClip;
        private bool _isHavePermission;

        [Header("Record Variables")]
        private List<float> _samplesDataList = new List<float>();
        private float[] _samplesData;
        private int _recordedSamples;
        private float _currentTimeRecord;

        [Header("Record Split Variables")]
        private List<float> _splitSamplesDataList = new List<float>();
        private float[] _splitSamplesData;
        private int _recordedSplitSamples;
        private Coroutine _timerToCallSplitCoroutine;
        public event Action<float[]> OnRecordSplited;

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
        public void RequestMicrophonePermission()
        {
            if (!CheckMicrophonePermission())
            {
                Debug.Log("User does not have microphone permission");
#if UNITY_ANDROID
                Debug.Log("Requesting Android mic permission");
                Permission.RequestUserPermission(Permission.Microphone);
#endif
#if UNITY_IOS
                Debug.Log("Requesting iOS mic permission");
                Application.RequestUserAuthorization(UserAuthorization.Microphone);
#else
                Debug.Log("Requesting permission in else block");
                Application.RequestUserAuthorization(UserAuthorization.Microphone);
#endif

                if (CheckMicrophonePermission())
                {
                    _isHavePermission = true;
                    Debug.Log("User has enabled microphone");
                }
                else Debug.Log("User did not provide microphone permission");
            }
            else 
            {
                _isHavePermission = true;
                Debug.Log("User has microphone permission");
            }
        }

        public void StartRecord()
        {
            if (!_isHavePermission)
            {
                RequestMicrophonePermission();
                return;
            }

            if (_state != RecorderState.Inactive) return;
            _state = RecorderState.Preparation;

            _timerToSaveAfterLimitCoroutine = StartCoroutine(TimerToSaveAfterLimit(_maxTimeRecord));
            _timeRecordControlCoroutine = StartCoroutine(TimeRecordControl());
            _timerToCallSplitCoroutine = StartCoroutine(TimerToCallSplit());
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
            if (_timerToCallSplitCoroutine != null) StopCoroutine(_timerToCallSplitCoroutine);

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
        private void SplitRecord(AudioClip clipToSplit)
        {
            _splitSamplesData = new float[clipToSplit.samples * clipToSplit.channels];
            clipToSplit.GetData(_splitSamplesData, 0);

            _splitSamplesDataList.Clear();
            _splitSamplesDataList = _splitSamplesData.ToList();
            _recordedSplitSamples = (int)(_splitSamplesData.Length * (_currentTimeRecord / (float)_maxTimeRecord));

            if (_recordedSplitSamples < _splitSamplesData.Length - 1)
            {
                _splitSamplesDataList.RemoveRange(_recordedSplitSamples, _splitSamplesData.Length - _recordedSplitSamples);
                _splitSamplesData = _splitSamplesDataList.ToArray();
            }

            OnRecordSplited?.Invoke(_splitSamplesData);
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
        private IEnumerator TimerToCallSplit()
        {
            while (true)
            {
                for (float t = 0; t < _timeToSplit; t += Time.deltaTime) { yield return null; }
                if (_audioClip != null) SplitRecord(_audioClip);
            }
        }
        private bool CheckMicrophonePermission()
        {
#if UNITY_ANDROID
            Debug.Log("Checked Android mic permission");
            return Permission.HasUserAuthorizedPermission(Permission.Microphone);
#endif
#if UNITY_IOS
            Debug.Log("Checked iOS mic permission");
            return Application.HasUserAuthorization(UserAuthorization.Microphone);
#else
            Debug.Log("Checked else mic permission");
            return Application.HasUserAuthorization(UserAuthorization.Microphone);
#endif
        }

        #endregion
    }
}