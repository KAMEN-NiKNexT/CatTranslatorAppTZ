using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kamen;
using CatTranslator.UI;
using CatTranslator.Save;

namespace CatTranslator.Control
{
    public class CatIconLoader : SingletonComponent<CatIconLoader>
    {
        #region Variables

        [Header("Settings")]
        [SerializeField] private CatsIconHolder _catIconHolder;

        [Header("Variables")]
        private Dictionary<string, Sprite> _cachedSprites = new Dictionary<string, Sprite>();
        private Texture2D _loadedTexture;
        private Sprite _createdSprite;

        #endregion

        #region Control Methods

        public Sprite LoadIcon(CatProfileData catProfileData)
        {
            if (catProfileData.IsUseInbuiltIcons) return _catIconHolder.GetCertainIcon(catProfileData.InbuiltIconIndex);
            if (_cachedSprites.ContainsKey(catProfileData.PhotoPath)) return _cachedSprites[catProfileData.PhotoPath];

            _loadedTexture = NativeGallery.LoadImageAtPath(catProfileData.PhotoPath);
            if (_loadedTexture == null) return _catIconHolder.GetCertainIcon(catProfileData.InbuiltIconIndex);

            _createdSprite = Sprite.Create(_loadedTexture, new Rect(0, 0, _loadedTexture.width, _loadedTexture.height), Vector2.zero);
            _cachedSprites.Add(catProfileData.PhotoPath, _createdSprite);
            return _createdSprite;
        }

        #endregion
    }
}