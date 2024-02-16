using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CatTranslator.Control
{
    [RequireComponent(typeof(Button))]
    public class OpenLinkButton : MonoBehaviour
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private string _androidURL;
        [SerializeField] private string _iosURL;
        private string _url;

        #endregion

        #region Unity Methods

        private void Start()
        {
            Initialize();
        }

        #endregion

        #region Control Methods

        private void Initialize()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(Click);
            _url = Application.platform == RuntimePlatform.IPhonePlayer ? _iosURL : _androidURL;
        }
        private void Click()
        {
            Application.OpenURL(_url);
        }

        #endregion
    }
}