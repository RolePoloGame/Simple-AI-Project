using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class GUIButton : EnhancedBehaviour, IPointerClickHandler
{
    private static GUIButton m_Selected = null;

    [SerializeField]
    private int m_ButtonIndex;

    private CanvasGroup m_Selection;
    private CanvasGroup Selection => m_Selection == null ? m_Selection = GetComponentInChildren<CanvasGroup>() : m_Selection;

    private TextMeshProUGUI m_TMPText;
    private TextMeshProUGUI TMPText => m_TMPText == null ? m_TMPText = GetComponentInChildren<TextMeshProUGUI>() : m_TMPText;

    private void OnEnable()
    {
        TMPText.SetText((m_ButtonIndex + 1).ToString());
    }

    public void Select()
    {
        if (m_Selected != null)
            m_Selected.Deselect();
        Selection.alpha = 1.0f;
        CameraManager.Instance.SwitchCamera(m_ButtonIndex);
        m_Selected = this;
    }

    public void Deselect()
    {
        Selection.alpha = 0.0f;
    }

    public void OnPointerClick(PointerEventData eventData) => Select();
}
