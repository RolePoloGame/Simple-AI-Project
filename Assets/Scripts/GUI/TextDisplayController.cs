using System.Collections;
using TMPro;
using UnityEngine;

public class TextDisplayController : EnhancedBehaviour
{
    #region Properties & Fields
    [SerializeField]
    private AIController m_AIController;

    [SerializeField]
    private TextMeshProUGUI m_TMPText;

    [SerializeField]
    private float m_FadeTime = .25f;

    private CanvasGroup m_CanvasGroup;
    private CanvasGroup CanvasGroup => m_CanvasGroup == null ? m_CanvasGroup = GetComponent<CanvasGroup>() : m_CanvasGroup;

    private float m_Time = 0.0f;
    #endregion

    #region Unity Methods
    void OnEnable() => AttachToAIController();

    void OnDisable() => DettachToAIController();
    #endregion

    #region Public methods
    public bool DisplayText(string text, float time)
    {
        m_Time = time;
        StopAllCoroutines();
        StartCoroutine(FadeText(text));
        return true;
    }
    #endregion

    #region Private Methods
    private void DettachToAIController() => m_AIController.DettachTextDisplayController();

    private void AttachToAIController() => m_AIController.AttachTextDisplayController(this);

    private IEnumerator FadeText(string text)
    {
        float fadeTimer;

        m_TMPText.SetText(text);

        //fadde in
        fadeTimer = m_FadeTime;
        CanvasGroup.alpha = 0.0f;
        while (fadeTimer > 0.0f)
        {
            fadeTimer -= Time.deltaTime;
            CanvasGroup.alpha = 1.0f - (fadeTimer / m_FadeTime);
            yield return null;
        }
        CanvasGroup.alpha = 1.0f;

        yield return new WaitForSeconds(m_Time);


        //Fade out
        fadeTimer = m_FadeTime;
        while (fadeTimer > 0.0f)
        {
            fadeTimer -= Time.deltaTime;
            CanvasGroup.alpha = (fadeTimer / m_FadeTime);
            yield return null;
        }
        CanvasGroup.alpha = 0.0f;
        m_TMPText.SetText(string.Empty);
    }
    #endregion
}

