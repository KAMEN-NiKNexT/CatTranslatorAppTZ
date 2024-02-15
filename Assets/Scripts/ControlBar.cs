using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Kamen;
using Kamen.UI;

public class ControlBar : SingletonComponent<ControlBar>
{
    #region Classes

    [Serializable]
    public class ControlButtonInfo
    {
        #region ControlButtonInfo Variables

        [Header("Settings")]
        [SerializeField] private string _name;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private string _openScreenName;

        #endregion

        #region ControlButtonInfo Properties

        public string Name { get => _name; }
        public Image Icon { get => _icon; }
        public TextMeshProUGUI Text { get => _text; }
        public string OpenScreenName { get => _openScreenName; }

        #endregion

        #region ControlButtonInfo Methods

        public void ChangeEnable(ControlButtonViewSettings settings)
        {
            Icon.DOColor(settings.Color, settings.ChangeDuration).SetEase(settings.ChangeEase);
            Text.DOColor(settings.Color, settings.ChangeDuration).SetEase(settings.ChangeEase);
        }

        #endregion
    }
    [Serializable]
    public class ControlButtonViewSettings
    {
        #region ControlButtonViewSettings Variables

        [Header("Settings")]
        [SerializeField] private Color32 _color;
        [SerializeField] private float _changeDuration;
        [SerializeField] private Ease _changeEase;

        #endregion

        #region ControlButtonViewSettings Properties

        public Color32 Color { get => _color; }
        public float ChangeDuration { get => _changeDuration; }
        public Ease ChangeEase { get => _changeEase; }

        #endregion
    }

    #endregion

    #region Variables

    [SerializeField] private ControlButtonInfo[] _buttonsInfo;
    [SerializeField] private ControlButtonViewSettings _disableSettings;
    [SerializeField] private ControlButtonViewSettings _enableSettings;

    #endregion

    #region Unity Methods

    private void Start()
    {
        //ChangeCurrentMenu(_buttonsInfo[0]);
    }


    #endregion

    #region Control Methods

    public void Hide() => gameObject.SetActive(false);
    public void Show() => gameObject.SetActive(true);

    public void ChangeCurrentMenu(string buttonName) => ChangeCurrentMenu(GetControlButtonInfoByName(buttonName));
    public void ChangeCurrentMenu(ControlButtonInfo buttonInfo)
    {
        for (int i = 0; i < _buttonsInfo.Length; i++)
        {
            if (buttonInfo.Name == _buttonsInfo[i].Name)
            {
                _buttonsInfo[i].ChangeEnable(_enableSettings);
                ScreenManager.Instance.TransitionTo(buttonInfo.OpenScreenName);
            }
            else
            {
                _buttonsInfo[i].ChangeEnable(_disableSettings);
            }
        }
    }

    #endregion

    #region Calculate Methods

    private ControlButtonInfo GetControlButtonInfoByName(string name)
    {
        for (int i = 0; i < _buttonsInfo.Length; i++)
        {
            if (name == _buttonsInfo[i].Name) return _buttonsInfo[i];
        }

        return null;
    }

    #endregion
}
