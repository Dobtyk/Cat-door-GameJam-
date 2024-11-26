using UnityEngine;

public class ButtonBack : MonoBehaviour
{
    public Animator animator;

    public void Back(GameObject obj)
    {
        obj.SetActive(false);
        animator.SetBool("Set Up", false);
    }
}
