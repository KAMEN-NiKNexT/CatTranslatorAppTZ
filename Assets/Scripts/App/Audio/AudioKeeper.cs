using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using System.IO;
using System.Text;
using System;

namespace CatTranslator.Audio
{
    public class AudioKeeper : SingletonComponent<AudioKeeper>
    {
        #region Enums

        public enum KeeperState
        {
            Standing,
            Saving,
            Loading
        }

        #endregion

        #region Variables

        [Header("Settings")]
        private const int HEADER_SIZE = 44;

        [Header("Variables")]
        private KeeperState _state = KeeperState.Standing;
        private AudioClip _audioClipForSave;
        private string _recordName = "Record";
        private string _fileName;
        private string _fullFilePath;
        public event Action<string> OnSavedWithName;

        #endregion

        #region Properties

        public KeeperState State { get => _state; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            Subscribes();
        }
        private void OnDestroy()
        {
            Unsubscribes();
        }

        #endregion

        #region Control Methods

        private void Subscribes()
        {
            AudioRecorder.Instance.OnRecordClipReady += SaveRecord;
        }
        private void Unsubscribes()
        {
            AudioRecorder.Instance.OnRecordClipReady -= SaveRecord;
        }
        public void SetRecordsName(string recordName) => _recordName = recordName;
        public void SaveRecord(AudioClip clip, float[] samplesData)
        {
            _state = KeeperState.Saving;

            _audioClipForSave = AudioClip.Create(_recordName, samplesData.Length, clip.channels, 44100, false);
            _audioClipForSave.SetData(samplesData, 0);

            string currentTime = DateTime.UtcNow.ToString("yyyy_MM_dd HH_mm_ss_ffff");
            _fileName = _recordName + " " + currentTime + ".wav";
            _fullFilePath = Path.Combine(Application.persistentDataPath, _fileName);

            if (File.Exists(_fullFilePath)) File.Delete(_fullFilePath);

            try { WriteWAVFile(_audioClipForSave, _fullFilePath); }
            catch (DirectoryNotFoundException) { Debug.LogError("Persistent Data Path not found!"); }
            OnSavedWithName?.Invoke(_fileName);

            _state = KeeperState.Standing;
        }
        public AudioClip LoadRecord(string fileName)
        {
            _state = KeeperState.Loading;
            string fullPath = Path.Combine(Application.persistentDataPath, fileName);
            AudioClip audioClip = WavUtility.ToAudioClip(fullPath);

            _state = KeeperState.Standing;
            return audioClip;
        }

        #endregion

        #region Calculation Methods


        private void WriteWAVFile(AudioClip clip, string filePath)
        {
            float[] clipData = new float[clip.samples];

            //Create the file.
            using (Stream fs = File.Create(filePath))
            {
                int frequency = clip.frequency;
                int numOfChannels = clip.channels;
                int samples = clip.samples;
                fs.Seek(0, SeekOrigin.Begin);

                //Header

                // Chunk ID
                byte[] riff = Encoding.ASCII.GetBytes("RIFF");
                fs.Write(riff, 0, 4);

                // ChunkSize
                byte[] chunkSize = BitConverter.GetBytes((HEADER_SIZE + clipData.Length) - 8);
                fs.Write(chunkSize, 0, 4);

                // Format
                byte[] wave = Encoding.ASCII.GetBytes("WAVE");
                fs.Write(wave, 0, 4);

                // Subchunk1ID
                byte[] fmt = Encoding.ASCII.GetBytes("fmt ");
                fs.Write(fmt, 0, 4);

                // Subchunk1Size
                byte[] subChunk1 = BitConverter.GetBytes(16);
                fs.Write(subChunk1, 0, 4);

                // AudioFormat
                byte[] audioFormat = BitConverter.GetBytes(1);
                fs.Write(audioFormat, 0, 2);

                // NumChannels
                byte[] numChannels = BitConverter.GetBytes(numOfChannels);
                fs.Write(numChannels, 0, 2);

                // SampleRate
                byte[] sampleRate = BitConverter.GetBytes(frequency);
                fs.Write(sampleRate, 0, 4);

                // ByteRate
                byte[] byteRate = BitConverter.GetBytes(frequency * numOfChannels * 2); // sampleRate * bytesPerSample*number of channels, here 44100*2*2
                fs.Write(byteRate, 0, 4);

                // BlockAlign
                ushort blockAlign = (ushort)(numOfChannels * 2);
                fs.Write(BitConverter.GetBytes(blockAlign), 0, 2);

                // BitsPerSample
                ushort bps = 16;
                byte[] bitsPerSample = BitConverter.GetBytes(bps);
                fs.Write(bitsPerSample, 0, 2);

                // Subchunk2ID
                byte[] datastring = Encoding.ASCII.GetBytes("data");
                fs.Write(datastring, 0, 4);

                // Subchunk2Size
                byte[] subChunk2 = BitConverter.GetBytes(samples * numOfChannels * 2);
                fs.Write(subChunk2, 0, 4);

                // Data

                clip.GetData(clipData, 0);
                short[] intData = new short[clipData.Length];
                byte[] bytesData = new byte[clipData.Length * 2];

                int convertionFactor = 32767;

                for (int i = 0; i < clipData.Length; i++)
                {
                    intData[i] = (short)(clipData[i] * convertionFactor);
                    byte[] byteArr = new byte[2];
                    byteArr = BitConverter.GetBytes(intData[i]);
                    byteArr.CopyTo(bytesData, i * 2);
                }

                fs.Write(bytesData, 0, bytesData.Length);
            }
        }

        #endregion
    }
}