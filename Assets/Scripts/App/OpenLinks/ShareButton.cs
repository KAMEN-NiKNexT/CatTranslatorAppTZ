using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace CatTranslator.Control
{
    [RequireComponent(typeof(Button))]
    public class ShareButton : MonoBehaviour
    {
        #region Variables

        private Button _button;

        #endregion

        #region Unity Methods

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Share);
        }

        #endregion

        public void Share()
        {
            StartCoroutine(TakeScreenshotAndShare());
        }

        private IEnumerator TakeScreenshotAndShare()
        {
            yield return new WaitForEndOfFrame();

            Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            ss.Apply();

            string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
            File.WriteAllBytes(filePath, ss.EncodeToPNG());

            // To avoid memory leaks
            Destroy(ss);

            new NativeShare().AddFile(filePath)
                .SetSubject("")
                .SetText("")
                .SetUrl("")
                .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
                .Share();
        }
    }
}