using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ModeSelectionUI : MonoBehaviour
{
    [Header("Other UIs")]
    public MainMenuUI mainMenuUI;
    [Space(3)]

    public CanvasGroup canvasGroup;
    public CanvasGroup informationPanel;
    public TextMeshProUGUI informationText;

    [Space]
    public float informationPanelOpenSpeed = 2.5f;
    public float openSpeed = 2.5f;

    private bool open = false;
    private bool infoPanelOpen = false;

    private void Update()
    {
        UpdateUI();
        UpdateInfoPanel();
    }

    void UpdateUI()
    {
        float newAlpha = open ? 1.0f : 0.0f;
        canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, newAlpha, Time.deltaTime * openSpeed);
        canvasGroup.interactable = open;
        canvasGroup.blocksRaycasts = open;
    }

    void UpdateInfoPanel()
    {
        float newAlpha = infoPanelOpen ? 1.0f : 0.0f;
        informationPanel.alpha = Mathf.Lerp(informationPanel.alpha, newAlpha, Time.deltaTime * informationPanelOpenSpeed);
    }

    public void HoverGamemode(Gamemode gamemode)
    {
        informationText.text = gamemode.description;

        infoPanelOpen = true;
    }

    public void UnhoverGamemode()
    {
        infoPanelOpen = false;
    }

    public void SelectGamemode(Gamemode gamemode)
    {
        GamemodeType type = gamemode.type;
        ObstacleSpawner.currentGamemode = type;
        SceneManager.LoadScene("Game", LoadSceneMode.Single);
    }

    public void Back()
    {
        mainMenuUI.Open(true);
        Open(false);
    }

    public void Open(bool open_)
    {
        open = open_;
    }
}
