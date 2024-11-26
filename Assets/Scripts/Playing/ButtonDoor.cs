using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonDoor : MonoBehaviour
{
    public bool isPressed;
    public UnityEvent Clicked;
    public Sprite red;
    public Image main;
    public Sprite black;
    public Sprite normal;

    public void Click()
    {
        isPressed = true;
        main.sprite = black;
        Clicked.Invoke();
    }
}
