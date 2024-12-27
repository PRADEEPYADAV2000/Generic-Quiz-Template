using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public Sprite questionImage;
        public Button[] options;
        public int correctAnswerIndex;
    }




    public Question[] questions;

        
    private int currentQuestionIndex = 0;   

    public Image questionImage;            
    public Text feedbackText;               
    public Transform correctListParent;     
    public Transform wrongListParent;       
    public GameObject listItemPrefab;      

    private List<string> correctAnswersList = new List<string>(); 
    private List<string> wrongAnswersList = new List<string>();   

    void Start()
    {
        ShowQuestion();
    }

    public void ShowQuestion()
    {
       
        Question currentQuestion = questions[currentQuestionIndex];

        
        questionImage.sprite = currentQuestion.questionImage;

        
        for (int i = 0; i < currentQuestion.options.Length; i++)
        {
            int index = i;  
            Button optionButton = currentQuestion.options[i];
            optionButton.gameObject.SetActive(true); 
            optionButton.onClick.RemoveAllListeners();
            optionButton.onClick.AddListener(() => CheckAnswer(index, currentQuestion));
        }

      
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }

    void CheckAnswer(int selectedAnswerIndex, Question currentQuestion)
    {
      
        foreach (Button button in currentQuestion.options)
        {
            button.gameObject.SetActive(false); 
        }

       
        if (selectedAnswerIndex == currentQuestion.correctAnswerIndex)
        {
            feedbackText.text = "Correct!";
            correctAnswersList.Add("Question " + (currentQuestionIndex + 1) + ": Correct");
        }
        else
        {
            feedbackText.text = "Wrong!";
            wrongAnswersList.Add("Question " + (currentQuestionIndex + 1) + ": Wrong");
        }

        
        UpdateLists();

       
        Invoke("NextQuestion", 1.5f); 
    }

    void NextQuestion()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex >= questions.Length)
        {
            Debug.Log("Quiz Finished!");
            currentQuestionIndex = 0;  
        }

       
        ShowQuestion();
    }

    void UpdateLists()
    {
        if (questions.Length - 1 == currentQuestionIndex) 
        {
            SceneManager.LoadScene("Menu");
            return;
        }
        
        foreach (Transform child in correctListParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in wrongListParent)
        {
            Destroy(child.gameObject);
        }

       
        foreach (string correctAnswer in correctAnswersList)
        {
            GameObject listItem = Instantiate(listItemPrefab, correctListParent);
            listItem.GetComponent<Text>().text = correctAnswer;
        }

        
        foreach (string wrongAnswer in wrongAnswersList)
        {
            GameObject listItem = Instantiate(listItemPrefab, wrongListParent);
            listItem.GetComponent<Text>().text = wrongAnswer;
        }
    }


public void MenuScene(string Scene)
    {
       SceneManager.LoadScene(Scene);
    }
}
