using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using CatTranslator.Save;

namespace CatTranslator.UI
{
    public class CatProfilePanel : ButtonComponent
    {
        #region Variables

        [Header("Objects")]
        [SerializeField] private Image _icon;

        [Header("Variables")]
        private CatProfileData _catProfileData;
        public event Action<CatProfileData> OnChoosed;

        #endregion

        #region Control Methods

        public void SetUpProfileInfo(CatProfileData catProfileData)
        {
            _catProfileData = catProfileData;

            _icon.sprite = LoadIcon(_catProfileData.PhotoPath);
            _text.text = _catProfileData.Name;

            _button.onClick.AddListener(Click);
        }
        private void Click()
        {
            OnChoosed?.Invoke(_catProfileData);
        }
        private Sprite LoadIcon(string path)
        {
            if (path == "") return _icon.sprite;

            Texture2D texture = NativeGallery.LoadImageAtPath(path);
            if (texture == null) return null;

            return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        }

        #endregion
    }
}