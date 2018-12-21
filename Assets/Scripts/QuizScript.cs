using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class QuizScript : MonoBehaviour {

    public GameObject QuestionInput;
    public static QAClass[] QaArr;
    //public GameObject AnswerPanel;
    public string[] questionList;
    public string[,] answerList;
    public int[] answerKey;
    int count = 0;
    

	// Use this for initialization
	void Start () {
        Cursor.visible = true;

        questionList = new string[7] {
            "How much Caffeine is consumed yearly in Metric Tons?",
            "What's adenosine role as explained by the video?", 
            "Which of the following is NOT a negative of caffiene consumption?",
            "How long is the video in question?",
            "Did the video help answer the questions? (Higher # meaning the video was helpful.)",
            "Was the lighting and color of the room an issue with the lecture? (Higher # meaning it was a bigger problem.)",
            "Would you prefer virtual lectures or in-person lectures? (Higher # meaning meaning you prefer virtual)"
        };
        answerList = new string[7,4] { 
            {"10", "10,000", "100,000", "1,000,000"},
            {"Produced by Caffeine", "Heartbeat Regulator", "Speeds up Brain Signals", "Causes Sleepiness"}, 
            {"Frequent Urination", "Quicker Heart Rate", "Increased Parkinson's Risk", "Needing Higher Quantities"},
            {"3 Minutes", "5 Minutes", "7 Minutes", "10 Minutes"},
            {"1", "2", "3", "4"},
            {"1", "2", "3", "4"},
            {"1", "2", "3", "4"}
        };
        answerKey = new int[3] { 2, 2, 1 };
        QaArr = new QAClass[questionList.Length + 1];
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void SubmitAnswer() {
        QaArr[count] = ReadQuestionAndAnswer(QuestionInput);
        if (count < questionList.Length)
        {
            SetNextQuestion(QuestionInput, count);
            count++;
        }
        else {
            FinishQuiz(QuestionInput);
        }
    }

    public IEnumerator CloseAfterTime(float t)
    {
        yield return new WaitForSeconds(t);
        Application.Quit();
    }

QAClass ReadQuestionAndAnswer(GameObject questionGroup) {
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
        q.GetComponent<Text>().text = "Thank you for your participation! Press Esc to quit.";
        a.SetActive(false);

        WriteString();

        //StartCoroutine(CloseAfterTime(2));
        Application.Quit();
    }

    static void WriteString()
    {
        //string path = "Assets/Resources/answer.txt";
        string path = Application.persistentDataPath + "/SurveyResults.txt";

        //Write some text to the test.txt file
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine("Custom Classroom = " + BuildScript.customBuilt);
        writer.WriteLine("Seat Distance = " + ScreenVisable.dist);
        writer.WriteLine("Screen Watch Time = " + ScreenVisable.screenTime);
        writer.WriteLine("Screen Total Time = " + ScreenVisable.totalTime);
        writer.WriteLine("Screen Watch Ratio = " + (float)(ScreenVisable.screenTime) / (float)(ScreenVisable.totalTime));

        foreach (QAClass q in QaArr) {
            writer.WriteLine("Question: " + q.Question);
            writer.WriteLine("Answer: " + q.Answer);
        }
        writer.Close();

        SendEmail("test", path);

        //File.Copy(path, Application.streamingAssetsPath + "/UploadThis.txt", true);
        //File.WriteAllText(Application.streamingAssetsPath + "/UploadThis.txt", path.)

        //Re-import the file to update the reference in the editor
        //AssetDatabase.ImportAsset(path);
        Resources.Load(path);
        //TextAsset asset = (TextAsset)Resources.Load("test");

        //Print the text from the file
        //Debug.Log(asset.text);
    }

    static void SendEmail(String body, String path) {

        MailMessage mail = new MailMessage();
        mail.From = new MailAddress("SquidSnacktime@gmail.com");
        mail.To.Add("SquidSnacktime@gmail.com");
        //mail.To.Add("qwerty526526@gmail.com");
        //mail.To.Add("al958@scarletmail.rutgers.edu");
        mail.Subject = "Lecture Simulator Results";
        mail.Body = body;
        mail.Attachments.Add(new Attachment(path));

        SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
        smtpServer.Port = 587;
        smtpServer.Credentials = new System.Net.NetworkCredential("SquidSnacktime@gmail.com", "qwerty526andy526") as ICredentialsByHost;
        smtpServer.EnableSsl = true;
        //ServicePointManager.ServerCertificateValidationCallback =
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
            return true;
        };
        smtpServer.Send(mail);
    }
}

[System.Serializable]
public class QAClass{
    public string Question = "";
    public string Answer =  "";
}
