using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Cat Translate Result Variant", menuName = "Cat Translator/Locales/Cat Translate Result Variant", order = 1)]
public class CatTranslateResultVariant : ScriptableObject
{
    #region Enums

    public enum LocaleName
    {
        English,
        Russian,
        Spanish,
        Portuguese
    }

    #endregion

    #region Classes

    [Serializable] public class LocaleInfo
    {
        #region LocaleInfo Variables

        [SerializeField] private LocaleName _locale;
        [SerializeField] private string _phrase;

        #endregion

        #region LocalInfo Properties

        public LocaleName Locale { get => _locale; }
        public string Phrases { get => _phrase; }

        #endregion
    }

    #endregion

    #region Variables

    [SerializeField] private LocaleInfo[] _localesInfo;

    #endregion

    #region Properties

    public LocaleInfo[] LocalesInfo { get => _localesInfo; }

    #endregion
}