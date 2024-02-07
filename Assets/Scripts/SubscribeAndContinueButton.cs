using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen.UI;

public class SubscribeAndContinueButton : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private string _screenName;
    [SerializeField] private GameObject _controlBar;

    public void Click()
    {
        ScreenManager.Instance.TransitionTo(_screenName);
        ControlBar.Instance.ChangeCurrentMenu("Translator");
        _controlBar.gameObject.SetActive(true);

    }
}
