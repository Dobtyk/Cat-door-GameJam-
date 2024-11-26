using UnityEngine;

public class Cat : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    public void Show(GameObject obj)
    {
        if (!animator.GetBool("Set Up"))
        {
            audioSource.Play();
        }
        obj.SetActive(true);
        animator.SetBool("Set Up", true);
    }
}
