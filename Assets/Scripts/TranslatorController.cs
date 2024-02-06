using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;

public class TranslatorController : SingletonComponent<TranslatorController>
{
    #region Variables

    [Header("Objects")]
    [SerializeField] private GameObject _controlBar;

    #endregion

    #region Properties



    #endregion

    #region Unity Methods



    #endregion

    #region Control Methods

    public void StartPlay()
    {
        _controlBar.SetActive(false);
    }

    #endregion
}
