using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;
using DG.Tweening;
using Kamen;

namespace CatTranslator.UI
{
    public class OpenScreen : Kamen.UI.Screen
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private float _timeToTransitionNextScreen;
        [SerializeField] private string _toTransitionScreenName;

        [Header("Variables")]
        private Coroutine _timerCoroutine;

        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            if (_timerCoroutine != null) StopCoroutine(_timerCoroutine);
        }

        #endregion

        #region Control Methods

        public override void Transit(bool isShow, bool isForth, ScreenManager.TransitionType type, float duration, Ease curve, MyCurve myCurve)
        {
            base.Transit(isShow, isForth, type, duration, curve, myCurve);
            _timerCoroutine = StartCoroutine(TimerToTransit());
        }
        private IEnumerator TimerToTransit()
        {
            yield return new WaitForSeconds(_timeToTransitionNextScreen);
            ScreenManager.Instance.TransitionTo(_toTransitionScreenName);
        } 

        #endregion
    }
}