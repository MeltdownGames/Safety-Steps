using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen Instance { get; private set; }

    public static bool open;

    public bool lerpOpen;
    public float openSpeed = 5f;

    private CanvasGroup cg;
    private AudioSource deathMessage;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
        deathMessage = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float alpha = open ? 1.0f : 0.0f;
        cg.alpha = lerpOpen ? Mathf.Lerp(cg.alpha, alpha, Time.deltaTime * openSpeed) : alpha;
        cg.interactable = open;
        cg.blocksRaycasts = open;
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        open = false;
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        open = false;
    }

    public void PlayerDied()
    {
        open = true;
        deathMessage.Play();
        Leaderboard.Instance.UpdateUserScore();
    }

    public void OpenClose(bool _open)
    {
        open = _open;
    }
}
