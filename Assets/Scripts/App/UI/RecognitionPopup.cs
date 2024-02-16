using DG.Tweening;
using Kamen.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace CatTranslator.UI
{
    public class RecognitionPopup : Popup
    {
        #region Classes

        [Serializable] private class PointTask
        {
            #region PointTask Variables

            [SerializeField] private Image _pointBackground;
            [SerializeField] private Image _pointIcon;
            [SerializeField] private TextMeshProUGUI _text;

            #endregion

            #region PointTask Properties

            public Image PointBackground { get => _pointBackground; }
            public Image PoinIcon { get => _pointIcon; }
            public TextMeshProUGUI Text { get => _text; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private PointTask[] _pointTasks;
        [SerializeField] private TranslatorScreen _translatorScreen;

        [Header("Settings")]
        [SerializeField] private float _minDelayBetweenPoints;
        [SerializeField] private float _maxDelayBetweenPoints;
        [SerializeField] private float _durationFillPoint;
        [SerializeField] private Ease _easeFillPoint;

        #endregion

        #region Properties



        #endregion

        #region Control Methods

        public override void Show()
        {
            base.Show();

            for (int i = 0; i < _pointTasks.Length; i++)
            {
                _pointTasks[i].PoinIcon.gameObject.SetActive(false);
            }

            StartCoroutine(PointAnimate());
        }
        private IEnumerator PointAnimate()
        {
            yield return new WaitForSeconds(_showAnimation.Duration);
            yield return new WaitForSeconds(GetDelay());

            for (int i = 0; i < _pointTasks.Length; i++)
            {
                _pointTasks[i].PoinIcon.gameObject.SetActive(true);
                _pointTasks[i].PoinIcon.transform.localScale = Vector3.zero;
                _pointTasks[i].PoinIcon.transform.DOScale(1, _durationFillPoint).SetEase(_easeFillPoint);

                yield return new WaitForSeconds(_durationFillPoint);
                yield return new WaitForSeconds(GetDelay());
            }

            PopupManager.Instance.Hide("RecognitionPopup");
            _translatorScreen.CallShowResalt();
        }
        private float GetDelay() => Random.Range(_minDelayBetweenPoints, _maxDelayBetweenPoints);

        #endregion
    }
}