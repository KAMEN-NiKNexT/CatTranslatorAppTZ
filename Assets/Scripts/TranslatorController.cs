using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class TranslatorController : SingletonComponent<TranslatorController>
{
    #region Variables

    [Header("Screen type - 1")]
    [SerializeField] private GameObject _catProfile;
    [SerializeField] private GameObject _exclusiveFeatures;
    [SerializeField] private ControlBar _controlBar;
    [SerializeField] private TextMeshProUGUI _startTranslatorText;
    [SerializeField] private Image _startTranslatorIcon;
    [SerializeField] private GameObject _startTranslatorBackground;

    [Header("Screen type - 2")]
    [SerializeField] private GameObject _audioGoingIcon;
    [SerializeField] private GameObject _audioGoingBackground;
    [SerializeField] private Button _stopRecordingButton;
    [SerializeField] private GameObject _audioLine;

    #endregion

    #region Properties



    #endregion

    #region Unity Methods



    #endregion

    #region Control Methods

    public void StartPlay()
    {
        _catProfile.SetActive(false);
        _exclusiveFeatures.SetActive(false);
        _controlBar.gameObject.SetActive(false);
        _startTranslatorText.gameObject.SetActive(false);
        _startTranslatorBackground.SetActive(false);
        _audioGoingIcon.SetActive(false);

        _startTranslatorIcon.transform.DOScale(0, 0.3f).SetEase(Ease.OutCubic).OnComplete(() =>
        {
            _startTranslatorIcon.gameObject.SetActive(false);
            _audioGoingIcon.transform.localScale = Vector3.zero;
            _audioGoingIcon.SetActive(true);

            _audioGoingIcon.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutCubic);
        });

        _audioGoingBackground.SetActive(true);
        _stopRecordingButton.gameObject.SetActive(true);
        _audioLine.SetActive(true);
    }

    #endregion
}
