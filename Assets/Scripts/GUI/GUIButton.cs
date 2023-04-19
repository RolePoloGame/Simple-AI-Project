using Assets.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.GUI
{
    public class GUIButton : EnhancedBehaviour, IPointerClickHandler
    {
        #region Properties & Fields
        private static GUIButton m_Selected = null;

        [SerializeField]
        private int m_ButtonIndex;

        private CanvasGroup m_Selection;
        private CanvasGroup Selection => m_Selection == null ? m_Selection = GetComponentInChildren<CanvasGroup>() : m_Selection;

        private TextMeshProUGUI m_TMPText;
        private TextMeshProUGUI TMPText => m_TMPText == null ? m_TMPText = GetComponentInChildren<TextMeshProUGUI>() : m_TMPText;
        #endregion

        #region Unity methods
        private void OnEnable()
        {
            TMPText.SetText((m_ButtonIndex + 1).ToString());
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Calls <see cref="CameraManager"/> to switch camera, changes it's background to selected color and sets self as current <see cref="m_Selected"/> 
        /// </summary>
        public void Select()
        {
            if (m_Selected != null)
                m_Selected.Deselect();
            Selection.alpha = 1.0f;
            CameraManager.Instance.SwitchCamera(m_ButtonIndex);
            m_Selected = this;
        }
        /// <summary>
        /// Clears the background's selected color 
        /// </summary>
        public void Deselect()
        {
            Selection.alpha = 0.0f;
        }

        public void OnPointerClick(PointerEventData eventData) => Select(); 
        #endregion
    }
}