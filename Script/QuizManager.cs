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

          // Array of questions for the quiz
    private int currentQuestionIndex = 0;   // To keep track of the current question

    public Image questionImage;             // Image UI element for the question
    public Text feedbackText;               // Optional feedback text (Correct/Wrong)
    public Transform correctListParent;     // Parent object for correct answers list
    public Transform wrongListParent;       // Parent object for wrong answers list
    public GameObject listItemPrefab;       // Prefab for list item (Text element)

    private List<string> correctAnswersList = new List<string>(); // List of correct answers
    private List<string> wrongAnswersList = new List<string>();   // List of wrong answers

    void Start()
    {
        ShowQuestion();
    }

    public void ShowQuestion()
    {
        // Get the current question
        Question currentQuestion = questions[currentQuestionIndex];

        // Set the question image
        questionImage.sprite = currentQuestion.questionImage;

        // Enable answer buttons and set listeners for the current question
        for (int i = 0; i < currentQuestion.options.Length; i++)
        {
            int index = i;  // Local copy for the button click listener
            Button optionButton = currentQuestion.options[i];
            optionButton.gameObject.SetActive(true);  // Ensure buttons are enabled
            optionButton.onClick.RemoveAllListeners();
            optionButton.onClick.AddListener(() => CheckAnswer(index, currentQuestion));
        }

        // Clear feedback text before showing new question
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }

    void CheckAnswer(int selectedAnswerIndex, Question currentQuestion)
    {
        // Disable all answer buttons for this question
        foreach (Button button in currentQuestion.options)
        {
            button.gameObject.SetActive(false);  // Hide buttons after selection
        }

        // Check if the answer is correct or wrong
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

        // Update the list UI
        UpdateLists();

        // Automatically move to the next question after a short delay
        Invoke("NextQuestion", 1.5f); // 1.5 seconds delay to show feedback
    }

    void NextQuestion()
    {
        currentQuestionIndex++;

        if (currentQuestionIndex >= questions.Length)
        {
            Debug.Log("Quiz Finished!");
            currentQuestionIndex = 0;  // Reset to the first question (or end the quiz)
        }

        // Show the next question
        ShowQuestion();
    }

    void UpdateLists()
    {
        if (questions.Length - 1 == currentQuestionIndex) 
        {
            SceneManager.LoadScene("Menu");
            return;
        }
        // Clear existing list items
        foreach (Transform child in correctListParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in wrongListParent)
        {
            Destroy(child.gameObject);
        }

        // Populate the correct answers list
        foreach (string correctAnswer in correctAnswersList)
        {
            GameObject listItem = Instantiate(listItemPrefab, correctListParent);
            listItem.GetComponent<Text>().text = correctAnswer;
        }

        // Populate the wrong answers list
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
