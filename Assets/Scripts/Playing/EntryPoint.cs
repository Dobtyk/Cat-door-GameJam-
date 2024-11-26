using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    public List<ButtonDoor> buttons;
    public List<int> answer;
    public DoorScript.Door door;
    public int nextLevel;
    List<int> _answerUser = new();

    public int deadScene;
    public bool isDeadScene;
    public TMP_Text timerText;
    public int _timeTimer;
    public bool _badScene;
    public TMP_Text badSceneText;
    int _badSceneVictory = -1;
    int _time;

    void Start()
    {
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
        var answerBad = new List<int> { 8, 0 };
        for (var i = 0; i < answer.Count; i++)
        {
            if (_badSceneVictory == 0 && _answerUser[i] == answerBad[i])
            {
                continue;
            }
            if (_answerUser[i] != answer[i])
            {
                isCorrect = false;
            }
        }
        if (isCorrect)
        {

            if (!_badScene)
            {
                door.OpenDoor();
                Invoke(nameof(Load), 0.5f);
                return;
            }
            _badSceneVictory += 1;
            if (_badSceneVictory == 3)
            {
                door.OpenDoor();
                Invoke(nameof(Load), 0.5f);
                return;
            }
            badSceneText.text = new List<string> { "5 + æ1 - æ2 = 13", "9 * æ1 = 36", "12 * æ1 - æ2 = 29" }[_badSceneVictory];
            if (_badSceneVictory == 0)
            {
                answer = new() { 9, 1 };
            }
            else if (_badSceneVictory == 1)
            {
                answer = new() { 4 };
            }
            else if (_badSceneVictory == 2)
            {
                answer = new() { 3, 7 };
            }
            Red();
            Invoke(nameof(Resest), 0.5f);
            _answerUser = new();
        }
        else
        {
            if (isDeadScene)
            {
                Red();
                Invoke(nameof(Load2), 0.5f);
            }
            else if (Random.Range(1, 11) <= 3)
            {
                Red();
                Invoke(nameof(Load1), 0.5f);
                _answerUser = new();
            }
            else
            {
                Red();
                Invoke(nameof(Load2), 0.5f);
            }
        }
    }

    private void Load2()
    {
        SceneManager.LoadScene(2);
    }

    private void Load1()
    {
        SceneManager.LoadScene(deadScene);
    }

    private void Load()
    {
        SceneManager.LoadScene(nextLevel);
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
