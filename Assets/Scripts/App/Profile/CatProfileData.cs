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

        [SerializeField] private string _name;
        [SerializeField] private string _photoPath;
        [SerializeField] private int _age;
        [SerializeField] private string _breed;
        [SerializeField] private CatGender _gender;
        [SerializeField] private List<string> _audioFileNames = new List<string>();

        #endregion

        #region Properties

        public string Name { get => _name; }
        public string Photo { get => _photoPath; }
        public int Age { get => _age; }
        public string Breed { get => _breed; }
        public CatGender Gender { get => _gender; }
        public List<string> AudioFileNames { get => _audioFileNames; }

        #endregion

        #region Constructor

        public CatProfileData(string name, string photoPath, int age, string breed, CatGender gender)
        {
            _name = name;
            _photoPath = photoPath;
            _age = age;
            _breed = breed;
            _gender = gender;
        }

        #endregion
    }
}
