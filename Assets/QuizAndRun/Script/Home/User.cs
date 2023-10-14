using UnityEngine;

public class User : MonoBehaviour
{
    public string id;
    public string username;
    public string password;
    public string email = "";
    public string score = "";
    public string isadmin = "";
    public User(string id,string username , string password , string email = "" , string score = "", string isadmin = "0")
    {
        this.id = id;
        this.username = username;
        this.password = password;
        this.email = email;
        this.score = score;
        this.isadmin = isadmin;
    }

}
