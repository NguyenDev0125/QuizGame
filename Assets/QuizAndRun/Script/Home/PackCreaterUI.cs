using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public class PackCreaterUI : MonoBehaviour
{
    [SerializeField] InputField titleTxt;
    [SerializeField] InputField desTxt;
    [SerializeField] InputField questionTxt;
    [SerializeField] InputField timeLimitTxt;
    [SerializeField] InputField aTxt;
    [SerializeField] InputField bTxt;
    [SerializeField] InputField cTxt;
    [SerializeField] InputField dTxt;
    [SerializeField] InputField trueAnswer;
    [SerializeField] Button addBtn;
    [SerializeField] Button uploadBtn;
    [SerializeField] QuestionItemUI itemPb;
    [SerializeField] Transform root;
    [SerializeField] Button pickImageButton;
    [SerializeField] Image image;
    [SerializeField] Text pickFileTxt;
    [SerializeField] Sprite sprNormal;
    [SerializeField] Text statusTxt;

    private List<Question> listQuestionAdded;
    private PackCreater creater;
    private Pack pack;
    NativeFilePicker.Permission permission;
    private string remotePath = "QuizImages/";
    private string localImagePath = "";
    private void Awake()
    {
        addBtn.onClick.AddListener(AddQuestion);
        uploadBtn.onClick.AddListener(UploadPack);
        pickImageButton.onClick.AddListener(PickImage);
    }

    private void OnEnable()
    {
        creater = new PackCreater();
        pack = creater.CreateNewPack();
        listQuestionAdded = new List<Question>();
    }

    private void PickImage()
    {

        NativeFilePicker.Permission permisstion = NativeFilePicker.CheckPermission(false);
        if (permission == NativeFilePicker.Permission.Denied)
        {
            permission = NativeFilePicker.RequestPermission(false);
        }
        if (permission == NativeFilePicker.Permission.Granted)
        {
            statusTxt.text = "Status : Picking image";
            NativeFilePicker.PickFile((path) =>
            {
                if (path != null)
                {
                    localImagePath = path;
                    byte[] bytes = File.ReadAllBytes(path);
                    Texture2D text = new Texture2D(image.sprite.texture.width, image.sprite.texture.height);
                    text.LoadImage(bytes, false);
                    Sprite spr = Sprite.Create(text, new Rect(0, 0, text.width, text.height), Vector2.zero);
                    image.sprite = spr;
                    pickFileTxt.gameObject.SetActive(false);
                    localImagePath = path;
                    image.gameObject.SetActive(true);

                }
            }, "image/*");
        }

    }
    private void Refresh()
    {
        localImagePath = "";
        questionTxt.text = "";
        timeLimitTxt.text = "";
        trueAnswer.text = "";
        aTxt.text = "";
        bTxt.text = "";
        cTxt.text = "";
        cTxt.text = "";
        dTxt.text = "";
        image.sprite = sprNormal;
        pickFileTxt.gameObject.SetActive(true);
    }

    private void AddQuestion()
    {
        if (!CheckQuestionIsCorrect())
        {
            statusTxt.text = "Status : Invalid input";
        }
        else
        {
            if(localImagePath != "")
            {
                byte[] bytes = File.ReadAllBytes(localImagePath);
                statusTxt.text = "Status : Image uploading....";
                DatabaseManager.Instance.SaveImage(bytes, remotePath +"/"+ Random.Range(11111, 99999), (url) =>
                {
                    statusTxt.text = "Status : Image uploaded";
                    Question question = CreateQuestion();
                    question.imageUrl = url;
                    uploadBtn.gameObject.SetActive(true);
                    statusTxt.text = "Status : Image uploaded";
                    listQuestionAdded.Add(question);

                    QuestionItemUI item = Instantiate(itemPb);
                    item.SetText(questionTxt.text);
                    item.transform.SetParent(root);
                    item.transform.localScale = Vector3.one;

                    Refresh();
                }); ;
            }
            else
            {
                Question question = CreateQuestion();
                uploadBtn.gameObject.SetActive(true);
                listQuestionAdded.Add(question);
                statusTxt.text = "Status : Question added";

                QuestionItemUI item = Instantiate(itemPb);
                item.SetText(questionTxt.text);
                item.transform.SetParent(root);
                item.transform.localScale = Vector3.one;
                Refresh();
            }

        }
    }

    private Question CreateQuestion()
    {
        int timeLimit = int.Parse(timeLimitTxt.text);
        int trueIndex = 0;
        if (trueAnswer.text.ToLower() == "b") trueIndex = 1; else if (trueAnswer.text.ToLower() == "c") trueIndex = 2; else if (trueAnswer.text.ToLower() == "d") trueIndex = 3;

        Question newQuesstion = new Question();
        newQuesstion.questionContent = questionTxt.text;
        newQuesstion.trueAnswerIndex = trueIndex;
        newQuesstion.imageUrl = "";
        newQuesstion.listAnswer = new string[] { aTxt.text, bTxt.text, cTxt.text, dTxt.text };
        newQuesstion.LimitedTime = timeLimit;
        return newQuesstion;
    }
    private void UploadPack()
    {
        foreach (var item in listQuestionAdded)
        {
            creater.AddQuestion(item);
        }
        creater.UploadPack(titleTxt.text, desTxt.text, "");
        uploadBtn.gameObject.SetActive(false);
        statusTxt.text = "Status : Level uploaded.";


    }

    private bool CheckQuestionIsCorrect()
    {
        if (aTxt.text == "") return false;
        if (bTxt.text == "") return false;
        if (cTxt.text == "") return false;
        if (dTxt.text == "") return false;
        if (timeLimitTxt.text == "") return false;
        int o = 0;
        if (int.TryParse(timeLimitTxt.text, out o) == false || o == 0) return false;
        string t = trueAnswer.text.ToLower();
        if (t.Contains("a") || t.Contains("b") || t.Contains("c") || t.Contains("d")) return true;
        return false;
    }

    private bool CheckPackIsCorrect()
    {
        if (titleTxt.text == "") return false;
        if (desTxt.text == "") return false;
        if (creater.ListQuestion.Count == 0) return false;
        return true;
    }
}
