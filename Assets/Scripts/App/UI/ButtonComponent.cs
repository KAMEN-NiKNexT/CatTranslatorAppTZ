using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using CatTranslator.Save;

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
        [SerializeField] protected GameObject _holder;
        [SerializeField] protected Image _background;
        [SerializeField] protected TextMeshProUGUI _text;

        [Header("Settings")]
        [SerializeField] protected ColorsInfo _enableColors;
        [SerializeField] protected ColorsInfo _disableColors;
        [SerializeField] protected ButtonState _startState;

        [Header("Variables")]
        protected Button _button;
        protected ButtonState _currentState;

        #endregion

        #region Properties

        public ButtonState CurrentState { get => _currentState; }

        #endregion

        #region Control Methods

        public void Initialize()
        {
            _currentState = _startState;
            _button = GetComponent<Button>();
            CallChangeState(true);
        }
        public void CallChangeState(bool isFast = false)
        {
            if (_currentState == ButtonState.Enabled) ChangeState(_disableColors, isFast);
            else if (_currentState == ButtonState.Disabled) ChangeState(_enableColors, isFast);
        }
        private void ChangeState(ColorsInfo colorsInfo, bool isFast = false)
        {
            _background.DOColor(colorsInfo.BackgroundColor, isFast ? 0 :colorsInfo.SwitchDuration).SetEase(colorsInfo.SwitchEase).SetDelay(colorsInfo.SwitchDelay);
            _text.DOColor(colorsInfo.TextColor, isFast ? 0 :colorsInfo.SwitchDuration).SetEase(colorsInfo.SwitchEase).SetDelay(colorsInfo.SwitchDelay);

            if (_currentState == ButtonState.Enabled) _currentState = ButtonState.Disabled;
            else if (_currentState == ButtonState.Disabled) _currentState = ButtonState.Enabled;
            
        }

        #endregion

        #region SetUp Methods

        public virtual void AddMethodForButtonClick(UnityAction callback) => _button.onClick.AddListener(callback);
        public virtual void RemoveMethodForButtonClick(UnityAction callback) => _button.onClick.RemoveListener(callback);

        #endregion
    }
}

