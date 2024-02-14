using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using Kamen.UI;
using TMPro;
using CatTranslator.UI;

public class AddNewCatPopup : Popup
{
    #region Variables

    [Header("Prefabs")]
    [SerializeField] private GameObject _catCard;
    //old

    [Header("Objects")]
    [SerializeField] private GameObject _catCardHolder;
    //old
    [SerializeField] private TextMeshProUGUI _nameInputField;
    [SerializeField] private TextMeshProUGUI _ageInputField;
    [SerializeField] private TextMeshProUGUI _breedInputField;
    [SerializeField] private ButtonComponent _maleButton;
    [SerializeField] private ButtonComponent _femaleButton;

    [Header("Settings")]
    [SerializeField] private string _popupHideName;
    //old

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
