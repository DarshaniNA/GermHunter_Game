using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    #region Variables
    private Question[] questions = null;
    public Question[] Questions { get { return questions; } }
   
    //private Data data = new Data();

    [SerializeField] GameEvent events = null;

    [SerializeField] Animator timerAnimator = null;
    [SerializeField] TextMeshProUGUI timerText = null;
    [SerializeField] Color timerHalfWayOutColor = Color.yellow;
    [SerializeField] Color timerAlmostOutColor = Color.red;
    private Color timerDefaultColor = Color.white;

    private List<AnswerData> PickAnswers = new List<AnswerData>();
    private List<int> FinishQuestions = new List<int>();
    private int currentQuestion = 0;

    private int timerStateParaHash = 0;

    private IEnumerator IE_WaitTillNextRound = null;
    private IEnumerator IE_StartTimer = null;

    private bool IsFinished
    {
        get
        {
            return (FinishQuestions.Count < Questions.Length) ? false : true;
        }
    }
    #endregion
    private void OnEnable()
    {
        events.UpdateQuestionAnswer += UpdateAnswers;
    }

    private void OnDisable()
    {
        events.UpdateQuestionAnswer -= UpdateAnswers;
    }

    private void Awake()
    {
        events.CurrentFinalScore = 0;
    }

    private void Start()
    {
        events.StartupHighscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);

        timerDefaultColor = timerText.color;
        LoadQuestions();
        //LoadData();

        timerStateParaHash = Animator.StringToHash("TimerState");

        var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        UnityEngine.Random.InitState(seed);

        Display();
    }

    //Function that is called to update new selected answers
    public void UpdateAnswers(AnswerData newAnswer)
    {
        //ans type in single
        if (Questions[currentQuestion].GetAnswerType == Question.AnswerType.Single)
        {
            foreach (var answer in PickAnswers)
            {
                if (answer != newAnswer)
                {
                    answer.Reset();
                }
            }
            //clear already picked ans
            PickAnswers.Clear();
            PickAnswers.Add(newAnswer);
        }
        //ans are not single
        else
        {
            bool alreadyPicked = PickAnswers.Exists(x => x == newAnswer);
            if (alreadyPicked)
            {
                PickAnswers.Remove(newAnswer);
            }
            else
            {
                PickAnswers.Add(newAnswer);
            }
        }
    }

    //function that is called to clear pickanswers list
    public void EraseAnswers()
    {
        PickAnswers = new List<AnswerData>();
    }

    //function that is called to display new question
    void Display()
    {
        EraseAnswers();
        var question = GetRandomQuestion();

        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);
        } else
        {
            Debug.LogWarning("Oops!! Something went wrong while trying to desplay new Question UI Data");

        }
        if (question.UseTimer)
        {
            UpdateTimer(question.UseTimer);
        }
    }


    // called to accept picked answers and check/display the result 
    public void Accept()
    {
        UpdateTimer(false);
        bool isCorrect = CheckAnswers();
        FinishQuestions.Add(currentQuestion);

        UpdateScore((isCorrect) ? Questions[currentQuestion].AddScore : -Questions[currentQuestion].AddScore);

        if (IsFinished)
        {
            //events.level++;
            //if (events.level > GameEvent.maxLevel)
            //{
            //    events.level = 1;
            //}
            SetHighScore();
        }

        var type 
            = (IsFinished)
            ? TuteUIManager.ResolutionScreenType.Finish
            : (isCorrect) ? TuteUIManager.ResolutionScreenType.Correct 
            : TuteUIManager.ResolutionScreenType.Incorrect;

        if (events.DisplayResolutionScreen != null)
        {
            //events.DisplayResolutionScreen?.Invoke(type, Questions[currentQuestion].AddScore);
            events.DisplayResolutionScreen(type, Questions[currentQuestion].AddScore);
        }

        //TuteAudioManager.Instance.PlaySound((isCorrect) ? "CorrectSFX" : "IncorrectSFX");

        if (type != TuteUIManager.ResolutionScreenType.Finish) {
            if (IE_WaitTillNextRound != null)
            {
                StopCoroutine(IE_WaitTillNextRound);
            }
            IE_WaitTillNextRound = WaitTillNextRound();
            StartCoroutine(IE_WaitTillNextRound);
        }

    }

    void UpdateTimer(bool state)
    {
        switch (state)
        {
            case true:
                IE_StartTimer = StartTimer();
                StartCoroutine(IE_StartTimer);

                timerAnimator.SetInteger(timerStateParaHash, 2);
                break;
            case false:
                if (IE_StartTimer != null)
                {
                    StopCoroutine(IE_StartTimer);
                }
                timerAnimator.SetInteger(timerStateParaHash, 1);
                break;
        }
    }

    IEnumerator StartTimer()
    {
        var totalTime = Questions[currentQuestion].Timer;
        var timeLeft = totalTime;

        timerText.color = timerDefaultColor;
        while (timeLeft > 0)
        {
            timeLeft--;

            //TuteAudioManager.Instance.PlaySound("CountdownSFX");
            if (timeLeft < totalTime / 2 && timeLeft > totalTime / 4)
            {
                timerText.color = timerHalfWayOutColor;
            }
            if (timeLeft < totalTime / 4)
            {
                timerText.color = timerAlmostOutColor;
            }
            timerText.text = timeLeft.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        Accept();
    }

    IEnumerator WaitTillNextRound()
    {
        yield return new WaitForSeconds(GameUtility.ResolutionDelayTime);
        Display();
    }

    Question GetRandomQuestion()
    {
        var randomIndex = GetRandomQuestionIndex();
        currentQuestion = randomIndex;

        return Questions[currentQuestion];
    }

    //to check currently picked answers and return result
    bool CheckAnswers()
    {
        //if (CompareAnswers() == false)
        if (!CompareAnswers())
        {
            return false;
        }
        return true;
    }

    //to compare picked answers with question correct answers
    bool CompareAnswers()
    {
        if (PickAnswers.Count > 0)
        {
            List<int> c = Questions[currentQuestion].GetCorrectAnswers();
            List<int> p = PickAnswers.Select(x => x.AnswerIndex).ToList();

            var f = c.Except(p).ToList();
            var s = p.Except(c).ToList();

            return !f.Any() && !s.Any();
        }
        return false;
    }

    int GetRandomQuestionIndex()
    {
        var random = 0;
        if (FinishQuestions.Count < Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, Questions.Length);
            } while (FinishQuestions.Contains(random) || random == currentQuestion);
        }
        return random;
    }

    // to load all questions from the Resources folder
    void LoadQuestions()
    {
        Object[] objs = Resources.LoadAll("Questions", typeof(Question));
        questions = new Question[objs.Length];

        for (int i = 0; i < objs.Length; i++)
        {
            questions[i] = (Question)objs[i];
        }
    }

    //void LoadData()
    //{
    //    data = Data.Fetch(Path.Combine(GameUtility.FileDir, GameUtility.FileName + events.level + ".xml"));

    //}

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    //to set new lighscorre if game score is higher
    private void SetHighScore()
    {
        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);
        if (highscore < events.CurrentFinalScore)
        {
            PlayerPrefs.SetInt(GameUtility.SavePrefKey, events.CurrentFinalScore);
        }
    }

    //to update score and score UI
    private void UpdateScore(int add)
    {
        events.CurrentFinalScore += add;
        if (events.CurrentFinalScore < 0) { events.CurrentFinalScore = 0; }
        if (events.ScoreUpdated != null)
        {
            events.ScoreUpdated();
        }
    }
}
