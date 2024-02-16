using CatTranslator.Audio;
using Kamen;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CatTranslator.Control
{
    public class AIManager : SingletonComponent<AIManager>
    {
        #region Variables

        public event Action<bool> OnCatNoticed;

        #endregion

        #region Unity Methods

        private void Start()
        {
            AudioRecorder.Instance.OnRecordSplited += CallAI;
        }
        private void OnDestroy()
        {
            AudioRecorder.Instance.OnRecordSplited -= CallAI;
        }

        #endregion

        #region Control Methods

        private void CallAI(float[] data)
        {
            //AI Working
            //Finished calculation

            int i = Random.Range(0, 2);
            OnCatNoticed?.Invoke(i == 0 ? false : true);
        }

        #endregion
    }
}