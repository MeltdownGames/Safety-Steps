using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Header("Other UIs")]
    public ModeSelectionUI modeSelectionUI;
    [Space(3)]

    public CanvasGroup canvasGroup;

    [Space]
    public float openSpeed = 2.5f;

    private bool open = true;

    private void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        float newAlpha = open ? 1.0f : 0.0f;
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, newAlpha, Time.deltaTime * openSpeed);
        canvasGroup.interactable = open;
        canvasGroup.blocksRaycasts = open;
    }

    public void Play()
    {
        modeSelectionUI.Open(true);
        Open(false);
    }

    public void Open(bool open_)
    {
        open = open_;
    }
}