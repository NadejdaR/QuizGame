using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
  [SerializeField] private List<QuestionSO> _quizList;
  [SerializeField] private Text _questionInfoTxt;
  [SerializeField] private Image _questionInfoImg;
  [SerializeField] private List<Button> _answersBtn;
  [SerializeField] private List<Text> _answersTxt;
  [SerializeField] private PlayerStat _playerStat;
  [SerializeField] private List<Image> _lifeImg;
  [SerializeField] private Button _helpBtn;

  private QuestionSO _selectedQuestion;
  private bool _isGameOver;
  private bool _isAnswered;
  private int _lifes;
  private int _showingAns;
  private int _numberHiddenButtons = 2;
  private int _currentIndex;

  private void Start()
  {
    _showingAns = 5;
    _lifes = 3;
    _playerStat.correctAns = 0;
    _playerStat.wrongAns = 0;
    _helpBtn.onClick.AddListener(HelpClick);
    foreach (Button localBtn in _answersBtn)
    {
      Button btn = localBtn;
      localBtn.onClick.AddListener(() => OnClick(btn));
    }

    SelectQuestion();
  }

  private void HelpClick()
  {
    for (int i = 0; i < _answersBtn.Count; i++)
    {
      if (_numberHiddenButtons <= 0)
        continue;
      if (_selectedQuestion.Answers[i] == _selectedQuestion.CorrectAns)
        continue;

      _answersBtn[i].GetComponent<Button>().interactable = false;

      _numberHiddenButtons--;
    }
  }

  public void ShowAllButtons()
  {
    foreach (Button t in _answersBtn)
    {
      t.GetComponent<Button>().interactable = true;
      _numberHiddenButtons = 2;
    }
  }

  private void ReduceLife(int remainingLife)
  {
    _lifeImg[remainingLife].gameObject.SetActive(false);
  }

  private void SelectQuestion()
  {
    _currentIndex = Random.Range(0, _quizList.Count);
    _selectedQuestion = _quizList[_currentIndex];
    SetQuestion(_selectedQuestion);
  }

  private void SetQuestion(QuestionSO question)
  {
    ShowAllButtons();
    _questionInfoTxt.text = question.QuestionTxt;
    _questionInfoImg.sprite = question.QuestionImg;

    List<string> ansOptions = question.Answers;
    for (int i = 0; i < _answersBtn.Count; i++)
    {
      _answersTxt[i].text = ansOptions[i];
      _answersBtn[i].name = ansOptions[i];
    }

    _isAnswered = false;
  }

  private void OnClick(Button btn)
  {
    if (_isGameOver && _isAnswered)
      return;
    _isAnswered = true;
    bool val = Answer(btn.name);
  }

  private bool Answer(string selectedOption)
  {
    bool correct = false;
    if (_selectedQuestion.CorrectAns == selectedOption)
    {
      _playerStat.correctAns++;

      correct = true;
    }
    else
    {
      _playerStat.wrongAns++;
      _lifes--;
      ReduceLife(_lifes);
    }

    if (_lifes == 0)
    {
      GameEnd();
    }

    if (_isGameOver)
      return correct;
    if ((_playerStat.wrongAns + _playerStat.correctAns) < _showingAns)
      Invoke(nameof(SelectQuestion), 0.4f);
    else
      Invoke(nameof(GameEnd), 0.4f);

    _quizList.RemoveAt(_currentIndex);
    return correct;
  }

  private void GameEnd()
  {
    _isGameOver = true;
    SceneManager.LoadScene("Win");
  }
}