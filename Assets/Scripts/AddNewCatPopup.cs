using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using Kamen.UI;
using TMPro;
using UnityEngine.Events;
using CatTranslator.Save;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;

namespace CatTranslator.UI
{
    public class AddNewCatPopup : Popup
    {
        #region Variables

        [Header("Prefabs")]
        [SerializeField] private GameObject _catCard;
        //old

        [Header("Objects")]
        [SerializeField] private Image _catIcon;
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private TMP_InputField _ageInputField;
        [SerializeField] private TMP_InputField _breedInputField;
        [SerializeField] private GenderButtonComponent[] _genderButtons;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _addPhotoButton;

        [Header("Settings")]
        [SerializeField] private CatsIconHolder _catsIconHolder;
        [SerializeField] private string _popupHideName;
        //old

        [Header("Variables")]
        private CatProfileData.CatGender _currentChoosenGender;
        private string _photoPath = "";
        private Sprite _photoSprite;
        public event Action<CatProfileData> OnAddedNewCatProfile;
        private int _currentInbuiltCatIcon;

        #endregion

        #region Properties



        #endregion

        #region Unity Methods

        private void OnDestroy()
        {
            UnscubribeFromAll();
        }

        #endregion

        #region Control Methods

        public override void Initialize()
        {
            base.Initialize();
            SetUpButtons();
            _currentChoosenGender = GetGenderFromButtons();
            SetUpSaveButton();
            SetUpAddPhotoButton();
            //_rightPosition += new Vector3(0, -62.5f, 0);
        }
        public override void Show()
        {
            base.Show();
            ClearFields();
        }
        private void ClearFields()
        {
            (_catIcon.sprite, _currentInbuiltCatIcon) = _catsIconHolder.GetRandomIcon();

            _nameInputField.text = "";
            _ageInputField.text = "";
            _breedInputField.text = "";

            if (_currentChoosenGender == CatProfileData.CatGender.Female) GenderButtonClick(CatProfileData.CatGender.Male);
        }
        private void UnscubribeFromAll()
        {
            for (int i = 0; i < _genderButtons.Length; i++)
            {
                _genderButtons[i].RemoveMethodGenderForButtonClick(GenderButtonClick);
            }
            _saveButton.onClick.RemoveListener(SaveNewCat);
            _addPhotoButton.onClick.RemoveListener(AddOwnPhoto);
        }

        #endregion

        #region Save Button Methods

        private void SetUpSaveButton()
        {
            Debug.Log(_saveButton);
            Debug.Log(_saveButton.onClick);
            _saveButton.onClick.AddListener(SaveNewCat);
        }
        private void SaveNewCat()
        {
            CatProfileData newCatProfileData = new CatProfileData (
                _photoPath, 
                _nameInputField.text, 
                _ageInputField.text == "" ? 0 : int.Parse(_ageInputField.text), 
                _breedInputField.text, 
                _currentChoosenGender, 
                _currentInbuiltCatIcon );

            OnAddedNewCatProfile?.Invoke(newCatProfileData);
            PopupManager.Instance.Hide(_popupHideName);
        }

        #endregion

        #region Add Photo Methods

        private void SetUpAddPhotoButton() => _addPhotoButton.onClick.AddListener(AddOwnPhoto);
        public void AddOwnPhoto()
        {
#if UNITY_ANDROID || UNITY_IOS
            NativeGallery.GetImageFromGallery((path) =>
            {
                if (PhotoExist(path)) 
                {
                    _catIcon.sprite = _photoSprite;
                    _photoPath = path;
                }
                
            });
#else
            // Do something else, since the plugin does not work inside the editor
#endif
        }
        private bool PhotoExist(string path)
        {
            Texture2D texture = NativeGallery.LoadImageAtPath(path);
            if (texture == null) return false;

            _photoSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            return true;
        }

        #endregion

        #region Gender Buttons Methods

        private void SetUpButtons()
        {
            for (int i = 0; i < _genderButtons.Length; i++)
            {
                _genderButtons[i].Initialize();
                _genderButtons[i].AddMethodForGenderButtonClick(GenderButtonClick);
            }
        }
        public void GenderButtonClick(CatProfileData.CatGender newGender)
        {
            if (_currentChoosenGender == newGender) return;

            _currentChoosenGender = newGender;
            for (int i = 0; i < _genderButtons.Length; i++)
            {
                _genderButtons[i].CallChangeState();
            }
        }
        private CatProfileData.CatGender GetGenderFromButtons()
        {
            for (int i = 0; i < _genderButtons.Length; i++)
            {
                if (_genderButtons[i].CurrentState == ButtonComponent.ButtonState.Enabled) return _genderButtons[i].ChoosenGender;
            }

            return CatProfileData.CatGender.Male;
        }

        #endregion

        #region Calculate Methods



        #endregion
    }
}