using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TutorialUI : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;

    public void StartTutorial()
    {
        audioSource.Play();
        animator.SetTrigger("Play");

        StartCoroutine(_StartTutorial());
        IEnumerator _StartTutorial()
        {
            yield return new WaitUntil(() => !audioSource.isPlaying);
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        StartTutorial();
    }
}
