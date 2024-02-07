using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using Kamen.UI;

public class AddNewCatPopup : Popup
{
    #region Variables

    [Header("Prefabs")]
    [SerializeField] private GameObject _catCard;

    [Header("Objects")]
    [SerializeField] private GameObject _catCardHolder;

    [Header("Settings")]
    [SerializeField] private string _popupHideName;

    #endregion

    #region Properties



    #endregion

    #region Control Methods

    public override void Initialize()
    {
        base.Initialize();
        _rightPosition += new Vector3(0, -62.5f, 0);
    }

    public void AddNewCat()
    {
        GameObject newCatCard = Instantiate(_catCard, _catCardHolder.transform);
        PopupManager.Instance.Hide(_popupHideName);
    }

    #endregion
}
