using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

//https://jsonplaceholder.typicode.com/

[System.Serializable]
public class JsonList<T>
{
    public List<T> data;

}

[System.Serializable]
public struct MandaraInfo
{
    //public int _id;
    //public int url;
    public string userName;
    public string comment;
    //public string createDate;
    public byte[] image;

    /*
    {
    "_id":"655dbd00d64e55cef6cc6245"
    "url":"https://hi-medi.s3.ap-northeast-2.amazonaws.com/hi-medi/1700642047881_mandara.jpeg"
    "userName":"김종완"
    "comment":"아이오에"
    "createDate":"2023-11-22"
    }
    */
}

[System.Serializable]
public struct CommentInfo
{
    public int postId;
    public int id;
    public string name;
    public string email;
    public string body;

    /*
    {
    "postId": 1,
    "id": 1,
    "name": "id labore ex et quam laborum",
    "email": "Eliseo@gardner.biz",
    "body": "laudantium enim quasi est quidem magnam voluptate ipsam eos\ntempora quo necessitatibus\ndolor quam autem quasi\nreiciendis et nam sapiente accusantium"
    }
    */
}

[System.Serializable]
public struct SignUpInfo
{
    public string userName;
    public string comment;
    //public string birthday;
    //public int age;
}

public enum RequestType
{
    GET,
    POST,
    PUT,
    DELETE,
    TEXTURE
}

//Class for HTTP Request/Response
public class HttpInfo
{
    //public int requestType;
    public RequestType requestType;
    public string url = "";
    public string body;
    public Action<DownloadHandler> onReceive;
    
    public void Set(RequestType rType, string u, Action<DownloadHandler> callback, bool useDefaultUrl = true)
    {
        requestType = rType;
        if (useDefaultUrl) url = "http://hi-medi.site/api/v1/mandara";
        //if (useDefaultUrl) url = "http://172.17.116.16:9000/";
        //if(useDefaultUrl) url = "http://172.17.116.16:9000/api/v1/mandara";

        url += u;
        onReceive = callback;
        //Debug.Log("HttpManager : " + requestType + ", " + url + ", " + onReceive);
    }
}

public class HttpManager : MonoBehaviour
{
    static HttpManager instance;

    public static HttpManager Get()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("HttpStudy");
            go.AddComponent<HttpManager>();
        }
        return instance;
    }
    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SendRequest(HttpInfo httpInfo)
    {
        StartCoroutine(CoSendRequest(httpInfo));
    }

    IEnumerator CoSendRequest(HttpInfo httpInfo)
    {
        //print(httpInfo.url);

        UnityWebRequest req = null;

        switch (httpInfo.requestType)
        {
            case RequestType.GET:  //서버로 데이터 요청(Data Request to Server)
                req = UnityWebRequest.Get(httpInfo.url);
                //print(req);
                break;
            case RequestType.POST:  //서버로 데이터 전송(Data Transfer for Server)
                req = UnityWebRequest.Post(httpInfo.url, httpInfo.body);
                byte[] byteBody = Encoding.UTF8.GetBytes(httpInfo.body);
                req.uploadHandler = new UploadHandlerRaw(byteBody);

                //Header
                req.SetRequestHeader("Content-Type", "application/json");
                break;
            case RequestType.PUT:
                req = UnityWebRequest.Put(httpInfo.url, httpInfo.body);
                break;
            case RequestType.DELETE:
                req = UnityWebRequest.Delete(httpInfo.url);
                break;
            case RequestType.TEXTURE:
                req = UnityWebRequestTexture.GetTexture(httpInfo.url);
                break;
        }

        //Loading ...

        //req = UnityWebRequest.Get("https://jsonplaceholder.typicode.com/comments?postId=1");
        yield return req.SendWebRequest();

        //Request Success
        if(req.result == UnityWebRequest.Result.Success)
        {
            print("Network reply : " + req.downloadHandler.text); //req.downloadHandler : 서버에서 전송해주는 데이터
            if(httpInfo.onReceive != null)
            {
                httpInfo.onReceive(req.downloadHandler);
            }
        }
        //Request Fail
        else
        {
            print("Network Error : " + req.error);
        }

        //Quit Loading ...
    }

   
}
