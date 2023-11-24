using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] //직렬화
public struct UserInfo
{
    public string userName;
    public int age;
    public float height;
    public bool gender;
    public List<string> favoriteFood;
}

public struct CharacterInfo
{
    public string charName;
    public int idx;
    public bool isGoalKipper;
    public List<Image> charImages;
}

[System.Serializable]
public struct FriendInfo
{
    public List<UserInfo> data;
}

public class JsonStudy : MonoBehaviour
{
    //My Info
    public UserInfo myInfo;
    //User Lists
    public List<UserInfo> friendList = new List<UserInfo>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            myInfo = new UserInfo();

            myInfo.userName = "최은혜" + i;
            myInfo.age = 23;
            myInfo.height = 163.2f;
            myInfo.gender = true;
            myInfo.favoriteFood = new List<string>();
            myInfo.favoriteFood.Add("치킨");
            myInfo.favoriteFood.Add("피자");

            friendList.Add(myInfo);
        }
        //To Make friendList Key
        FriendInfo info = new FriendInfo();
        info.data = friendList;

        string s = JsonUtility.ToJson(info, true);
        print(s);

        //JsonDataTest test = new JsonDataTest();
        //test.jsonData = myInfo;

        //string s = JsonUtility.ToJson(test, true);
        //print(s);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            //Make myInfo to Json format
            string jsonData = JsonUtility.ToJson(myInfo, true);
            //print(jsonData);

            //Save the jsonData to file
            FileStream file = new FileStream(Application.dataPath + "/myInfo.txt", FileMode.Create);
            //print(Application.dataPath + "/myInfo.txt");
            
            //Type Cast string to byte array
            byte[] byteData = Encoding.UTF8.GetBytes(jsonData);

            //Write the byteData to file
            file.Write(byteData, 0, byteData.Length);

            //Close the file
            file.Close();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            FileStream file = new FileStream(Application.dataPath + "/myInfo.txt", FileMode.Open);
            byte[] byteData = new byte[file.Length];

            //Read file byteData
            file.Read(byteData, 0, byteData.Length);

            //Close the file
            file.Close();

            //Type Cast string to byteData
            string jsonData = Encoding.UTF8.GetString(byteData);
            //print(jsonData);
            //Parse jsonData string to myInfo
            myInfo = JsonUtility.FromJson<UserInfo>(jsonData);
            
            //print(myInfo);
        }
    }
}
