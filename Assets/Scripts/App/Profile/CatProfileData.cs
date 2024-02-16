using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace CatTranslator.Save
{
    [Serializable] public class CatProfileData
    {
        #region Enums

        public enum CatGender
        {
            Male,
            Female
        }

        #endregion

        #region Variables

        [SerializeField] private string _photoPath;
        [SerializeField] private string _name;
        [SerializeField] private int _age;
        [SerializeField] private string _breed;
        [SerializeField] private CatGender _gender;
        [Space]
        [SerializeField] private bool _isUseInbuiltIcons;
        [SerializeField] private int _inbuiltIconIndex;
        [Space]
        [SerializeField] private List<string> _audioFileNames = new List<string>();

        #endregion

        #region Properties

        public string PhotoPath { get => _photoPath; }
        public string Name { get => _name; }
        public int Age { get => _age; }
        public string Breed { get => _breed; }
        public CatGender Gender { get => _gender; }

        public bool IsUseInbuiltIcons { get => _isUseInbuiltIcons; }
        public int InbuiltIconIndex { get => _inbuiltIconIndex; }

        public List<string> AudioFileNames { get => _audioFileNames; }

        #endregion

        #region Constructor

        public CatProfileData(string photoPath, string name, int age, string breed, CatGender gender, int inbuiltIconIndex)
        {
            _photoPath = photoPath;
            _name = name == "" ? "Unknown cat" : name;
            _age = age;
            _breed = breed;
            _gender = gender;

            if (photoPath == "")
            {
                _isUseInbuiltIcons = true;
                _inbuiltIconIndex = inbuiltIconIndex;
            }
        }

        #endregion
    }
}
