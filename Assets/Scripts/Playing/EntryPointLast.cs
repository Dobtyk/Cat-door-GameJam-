using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EntryPointLast : MonoBehaviour
{
    public List<ButtonDoor> buttons;
    public List<int> answer;
    public DoorScript.Door door;
    public int nextLevel;
    List<int> _answerUser = new();

    public List<Image> gg = new();
    public Sprite Sprite0;
    public Sprite Sprite1;
    public Sprite Sprite2;
    public Sprite Sprite3;
    public Sprite Sprite4;
    public Sprite Sprite5;
    public Sprite Sprite6;
    public Sprite Sprite7;
    public Sprite Sprite8;
    public Sprite Sprite9;

    public int deadScene;
    public bool isDeadScene;
    public TMP_Text timerText;
    public TMP_Text counter;
    int count = 4;
    public int _timeTimer;
    int _time;

    void Start()
    {
        counter.text = $"{count}";
        _timeTimer += (int)Time.time;
        var random = Random.Range(0, 2);
        if (random == 0)
        {
            _timeTimer += Random.Range(0, 15);
        }
        else
        {
            _timeTimer -= Random.Range(0, 15);
        }
    }

    void Update()
    {
        _time = (int)Time.time;
        if (_timeTimer - _time <= 0)
        {
            SceneManager.LoadScene(2);
        }
        else if (_timeTimer - _time >= 60 && _timeTimer - _time <= 69)
        {
            timerText.text = $"1:0{_timeTimer - _time - 60}";
        }
        else if (_timeTimer - _time >= 60)
        {
            timerText.text = $"1:{_timeTimer - _time - 60}";
        }
        else if (_timeTimer - _time <= 9)
        {
            timerText.text = $"0:0{_timeTimer - _time}";
        }
        else
        {
            timerText.text = $"0:{_timeTimer - _time}";
        }
    }

    public void Check(int numberButton)
    {
        var isCorrect = true;
        if (!_answerUser.Contains(numberButton))
        {
            _answerUser.Add(numberButton);
        }
        if (_answerUser.Count != answer.Count)
        {
            return;
        }
        for (var i = 0; i < answer.Count; i++)
        {
            if (_answerUser[i] != answer[i])
            {
                isCorrect = false;
            }
        }
        if (isCorrect && count == 1)
        {
            door.OpenDoor();
            SceneManager.LoadScene(nextLevel);
            return;
        }
        else if (isCorrect)
        {
            count -= 1;
            counter.text = $"{count}";
            _answerUser = new();
            answer = new List<List<int>> { new() { 3 }, new() { 2, 7 }, new() { 0, 5, 9 } }[count - 1];
            if (count == 3)
            {
                gg[0].sprite = Sprite0;
                gg[1].sprite = Sprite5;
                gg[2].sprite = Sprite9;
                gg[3].enabled = false;
            }
            else if (count == 2)
            {
                gg[0].sprite = Sprite2;
                gg[1].sprite = Sprite7;
                gg[2].enabled = false;
            }
            else if (count == 1)
            {
                gg[0].sprite = Sprite3;
                gg[1].enabled = false;
            }
        }
        else
        {
            Red();
            Invoke(nameof(Resest), 0.5f);
            if (isDeadScene)
            {
                Load2();
            }
            Load1();
            _answerUser = new();
        }
    }

    private void Load1()
    {
        SceneManager.LoadScene(deadScene);
    }

    private static void Load2()
    {
        SceneManager.LoadScene(2);
    }

    private void Resest()
    {
        foreach (var item in _answerUser)
        {
            buttons[item].main.sprite = buttons[item].normal;
        }
    }

    private void Red()
    {
        foreach (var item in _answerUser)
        {
            buttons[item].main.sprite = buttons[item].red;
        }
    }
}
