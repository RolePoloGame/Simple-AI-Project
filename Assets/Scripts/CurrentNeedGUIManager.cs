using Assets.Scripts.Core;
using Assets.Scripts.Core.Utilities;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class CurrentNeedGUIManager : Singleton<CurrentNeedGUIManager>
    {
        #region Properties & Fields
        [SerializeField]
        private TextMeshProUGUI m_TMPText;
        #endregion

        #region Public methods
        /// <summary>
        /// Sets text on a Current Need GUI with a given color
        /// </summary>
        public void SetText(string text, Color color)
        {
            SetText(text.Color(color));
        }
        /// <summary>
        /// Sets text on a Current Need GUI
        /// </summary>
        public void SetText(string text)
        {
            if (m_TMPText == null) return;
            m_TMPText.SetText(text);
        }
        #endregion
    }
}