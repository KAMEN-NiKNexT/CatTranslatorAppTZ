using Kamen.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Kamen
{
    [RequireComponent(typeof(Button))]
    public class OpenPopupButton : MonoBehaviour
    {
        #region Variables

        [SerializeField] private string _popupID;

        #endregion

        #region Unity Methods

        private void Start()
        {
            gameObject.GetComponent<Button>().onClick.AddListener(Click);
        }

        #endregion

        #region Click Methods

        private void Click() => PopupManager.Instance.Show(_popupID);

        #endregion
    }
}
