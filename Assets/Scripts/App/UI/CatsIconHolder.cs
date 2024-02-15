using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CatTranslator.UI
{
    [CreateAssetMenu(fileName = "Cats Icon Holder", menuName = "Cat Translator/UI/Cats Icon Holder", order = 1)]
    public class CatsIconHolder : ScriptableObject
    {
        #region Variables

        [SerializeField] private Sprite[] _icon;

        #endregion

        #region Control Methods

        public Sprite GetCertainIcon(int index)
        {
            if (index > _icon.Length - 1)
            {
                Debug.LogWarning("[CatsIconHolder] - Outside the array boundaries");
                return null;
            }

            return _icon[index];
        }
        public Sprite GetRandomIcon() => _icon[Random.Range(0, _icon.Length - 1)];

        #endregion
    }
}