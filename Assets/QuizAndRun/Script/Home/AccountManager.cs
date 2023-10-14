using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    [SerializeField] HomeUIController homeUIController;
    [SerializeField] GameObject loginPanel;
    [SerializeField] Button loginBtn;
    [SerializeField] Button registerBtn;
    [SerializeField] InputField username;
    [SerializeField] InputField password;
    [SerializeField] Text statusTxt;

    [SerializeField] GameObject accInfoPanel;
    [SerializeField] Text usernameTxt;
    [SerializeField] Text lvTxt;

    private const string ID_KEY = "id";
    private const string USERNAME_KEY = "username";
    private const string PASSWORD_KEY = "password";
    private const string EMAIL_KEY = "email";
    private const string SCORE_KEY = "score";
    private const string ISADMIN_KEY = "isadmin";

    private const string AccountPath = "Accounts/";

    private void Start()
    {
        loginBtn.onClick.AddListener(() => Login(username.text, password.text));
        registerBtn.onClick.AddListener(() => Register(username.text, password.text));
        if (GetPrefKey(USERNAME_KEY) != "" && GetPrefKey(PASSWORD_KEY) != "")
        {
            username.text = GetPrefKey(USERNAME_KEY);
            password.text = GetPrefKey(PASSWORD_KEY);
        }
    }
    private void Login(string username, string password)
    {
        statusTxt.text = "Wait for login....";
        DatabaseManager.Instance.GetDictionaryData(AccountPath, (users) =>
        {
            foreach (var user in users)
            {
                if (user[USERNAME_KEY] == username && user[PASSWORD_KEY] == password)
                {
                    statusTxt.text = "Login success";
                    SavePrefKey(ID_KEY, user[ID_KEY]);
                    SavePrefKey(USERNAME_KEY, user[USERNAME_KEY]);
                    SavePrefKey(PASSWORD_KEY, user[PASSWORD_KEY]);
                    SavePrefKey(EMAIL_KEY, user[EMAIL_KEY]);
                    SavePrefKey(SCORE_KEY, user[SCORE_KEY]);
                    SavePrefKey(ISADMIN_KEY, user[ISADMIN_KEY]);
                    accInfoPanel.SetActive(true);
                    usernameTxt.text = user[USERNAME_KEY];
                    lvTxt.text = "Score : " + user[SCORE_KEY];
                    loginPanel.SetActive(false);
                    homeUIController.OpenMenuPanel(user[ISADMIN_KEY] == "1");
                    return;
                }
            }
            statusTxt.text = "Login failed";
        });
    }
    private void Register(string username, string password)
    {

        if (!CheckValidInput())
        {
            statusTxt.text = "Bro ?:)";
        }
        statusTxt.text = "Wait for register....";
        DatabaseManager.Instance.GetDictionaryData(AccountPath, (users) =>
        {
            foreach (var user in users)
            {
                if (user[USERNAME_KEY] == username)
                {
                    statusTxt.text = "Username exist";
                    return;
                }
            }
            User userData = new User(Random.Range(1111, 9999).ToString(), username, password, "", "0", "0");
            Dictionary<string, string> data = new Dictionary<string, string>();
            data.Add(ID_KEY, userData.id);
            data.Add(USERNAME_KEY, userData.username);
            data.Add(PASSWORD_KEY, userData.password);
            data.Add(EMAIL_KEY, userData.email);
            data.Add(SCORE_KEY, userData.score);
            data.Add(ISADMIN_KEY, userData.isadmin);
            DatabaseManager.Instance.SaveDictionary(AccountPath + userData.id, data);
            statusTxt.text = "Register success !";
        });
    }



    private bool CheckValidInput()
    {
        if (username.text == "" && password.text == "") return false;
        string[] filter = new string[]
        {
            " ngu ",
            " lon ",
            " loz ",
            " dit "
        };
        foreach (string s in filter)
        {
            if (username.text.Contains(s)) return false;
        }
        return true;
    }
    private string GetPrefKey(string key)
    {
        return PlayerPrefs.GetString(key, "");
    }

    private void SavePrefKey(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }

}
