using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour {

    public GameObject QuestionInput;
    public QAClass[] QaArr;
    public GameObject AnswerPanel;
    public string[] questionList;
    public string[,] answerList;
    public int[] answerKey;
    int count = 0;
    

	// Use this for initialization
	void Start () {
        questionList = new string[3] { "Which one is an irrational number?", "Which Character is not in Smash?", "State Capitol of Nebraska?" };
        answerList = new string[3,4] { { "4", "Pi", "7/12", "-328.6"},{"Bowser Jr.", "Piranha Plant", "Geno", "Zelda"}, {"Lincoln", "Osbourn", "Omaha", "Nebraska City"} };
        answerKey = new int[3] { 2, 2, 1 };
        QaArr = new QAClass[questionList.Length + 1];
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SubmitAnswer() {
        QaArr[count] = readQuestionAndAnswer(QuestionInput);
        if (count < questionList.Length)
        {
            SetNextQuestion(QuestionInput, count);
            count++;
        }
        else {
            FinishQuiz(QuestionInput);
        }
    }

    QAClass readQuestionAndAnswer(GameObject questionGroup) {
        QAClass result = new QAClass();

        GameObject q = questionGroup.transform.Find("Question").gameObject;
        GameObject a = questionGroup.transform.Find("Answer").gameObject;

        result.Question = q.GetComponent<Text>().text;
        if (a.GetComponent<ToggleGroup>() != null)
        {
            for (int i = 0; i < a.transform.childCount; i++)
            {
                if (a.transform.GetChild(i).GetComponent<Toggle>().isOn)
                {
                    result.Answer = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                    break;
                }
            }
        }
        else if (a.GetComponent<InputField>() != null) {
            result.Answer = a.transform.Find("Text").GetComponent<Text>().text;
        }         
        return result;
    }

    void SetNextQuestion(GameObject questionGroup, int count) {
        GameObject q = questionGroup.transform.Find("Question").gameObject;
        GameObject a = questionGroup.transform.Find("Answer").gameObject;

        q.GetComponent<Text>().text = questionList[count];
        for (int i = 0; i < a.transform.childCount; i++)
        {
            a.transform.GetChild(i).Find("Label").GetComponent<Text>().text = answerList[count,i];
        }
    }

    void FinishQuiz(GameObject questionGroup) {
        GameObject q = questionGroup.transform.Find("Question").gameObject;
        GameObject a = questionGroup.transform.Find("Answer").gameObject;
        q.GetComponent<Text>().text = "Thank you for your participation!";
        a.SetActive(false);
    }
}

[System.Serializable]
public class QAClass{
    public string Question = "";
    public string Answer =  "";
}
