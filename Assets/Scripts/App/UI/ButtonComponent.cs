using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CatTranslator.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonComponent : MonoBehaviour
    {
        #region Enums

        public enum ButtonState
        {
            Enabled,
            Disabled
        }

        #endregion

        #region Classes

        [Serializable] public class ColorsInfo
        {
            #region ColorsInfo Variables

            [Header("Settings")]
            [SerializeField] private Color32 _backgroundColor;
            [SerializeField] private Color32 _textColor;
            [Space]
            [SerializeField] private float _switchDelay;
            [SerializeField] private float _switchDuration;
            [SerializeField] private Ease _switchEase;

            #endregion

            #region ColorInfo Properties

            public Color32 BackgroundColor { get => _backgroundColor; }
            public Color32 TextColor { get => _textColor; }

            public float SwitchDelay { get => _switchDelay; }
            public float SwitchDuration { get => _switchDuration; }
            public Ease SwitchEase { get => _switchEase; }

            #endregion
        }

        #endregion

        #region Variables

        [Header("Objects")]
        [SerializeField] private GameObject _holder;
        [SerializeField] private Image _background;
        [SerializeField] private TextMeshProUGUI _text;

        [Header("Settings")]
        [SerializeField] private ColorsInfo _enableColors;
        [SerializeField] private ColorsInfo _disableColors;
        [SerializeField] private ButtonState _startState;

        [Header("Variables")]
        private ButtonState _currentState;

        #endregion

        #region Properties

        public ButtonState CurrentState { get => _currentState; }

        #endregion

        #region Unity Methods

        private void Start()
        {
            _currentState = _startState;
            CallChangeState(true);
        }


        #endregion

        #region Control Methods


        public void CallChangeState(bool isFast)
        {
            if (_currentState == ButtonState.Enabled) ChangeState(_disableColors, isFast);
            else if (_currentState == ButtonState.Disabled) ChangeState(_enableColors, isFast);
        }
        private void ChangeState(ColorsInfo colorsInfo, bool isFast)
        {
            _background.DOColor(colorsInfo.BackgroundColor, isFast ? 0 :colorsInfo.SwitchDuration).SetEase(colorsInfo.SwitchEase).SetDelay(colorsInfo.SwitchDelay);
            _text.DOColor(colorsInfo.TextColor, isFast ? 0 :colorsInfo.SwitchDuration).SetEase(colorsInfo.SwitchEase).SetDelay(colorsInfo.SwitchDelay);

            if (_currentState == ButtonState.Enabled) _currentState = ButtonState.Disabled;
            else if (_currentState == ButtonState.Disabled) _currentState = ButtonState.Enabled;
        }

        #endregion
    }
}

