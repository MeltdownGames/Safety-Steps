using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public static bool open;

    public bool lerpOpen;
    public float openSpeed = 5f;

    private CanvasGroup cg;

    private void Start()
    {
        cg = GetComponent<CanvasGroup>();
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
        Debug.LogError("Menu functionality not finished in code yet."); // remove
        return; // remove
        SceneManager.LoadScene("Menu", LoadSceneMode.Single); // rename to whatever the menu is now
        open = false;
    }
}
