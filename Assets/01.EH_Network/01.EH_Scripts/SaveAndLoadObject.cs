using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

//System.IO : File IO, System.Text : Encoding

[System.Serializable]
public class ObjectInfo
{
    public int type;
    public Transform tr;
}

[System.Serializable]
public class SaveInfo
{
    public int type;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
}

[System.Serializable]
public class JsonList
{
    public List<SaveInfo> data;

}

public class SaveAndLoadObject : MonoBehaviour
{
    public List<ObjectInfo> objectList = new List<ObjectInfo>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Make Object with Shape, Scale, Transform, Rotation
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Set Random Primitive Shape(0 ~ 3)
            int type = Random.Range(0, 4);

            //Type Cast int to enum 'PrimitiveType'
            GameObject go = GameObject.CreatePrimitive((PrimitiveType)type);

            //Set Random Scale, Transform, Rotation
            go.transform.localScale = Vector3.one * Random.Range(0.5f, 2.0f);
            go.transform.position = Random.insideUnitSphere * Random.Range(1.0f, 20.0f);
            go.transform.rotation = Random.rotation;

            //Save the Object Info to List
            ObjectInfo info = new ObjectInfo();
            info.type = type;
            info.tr = go.transform;

            objectList.Add(info);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            List<SaveInfo> saveInfoList = new List<SaveInfo>();

            //Get Info based on objectList
            for(int i = 0; i < objectList.Count; i++)
            {
                SaveInfo saveInfo = new SaveInfo();
                saveInfo.type = objectList[i].type;
                saveInfo.pos = objectList[i].tr.position;
                saveInfo.rot = objectList[i].tr.rotation;
                saveInfo.scale = objectList[i].tr.localScale;

                saveInfoList.Add(saveInfo);
            }

            //Make JsonData using saveInfoList
            JsonList jsonList = new JsonList();
            jsonList.data = saveInfoList;
            string jsonData = JsonUtility.ToJson(jsonList, true);
            print(jsonData);

            //Save the jsonData to file
            FileStream file = new FileStream(Application.dataPath + "/objectInfo.txt", FileMode.Create);
            //print(Application.dataPath + "/myInfo.txt");

            //Type Cast string to byte array
            byte[] byteData = Encoding.UTF8.GetBytes(jsonData);

            //Write the byteData to file
            file.Write(byteData, 0, byteData.Length);

            //Close the file
            file.Close();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            FileStream file = new FileStream(Application.dataPath + "/objectInfo.txt", FileMode.Open);

            byte[] byteData = new byte[file.Length];

            //Read file byteData
            file.Read(byteData, 0, byteData.Length);

            //Close the file
            file.Close();

            //Type Cast string to byteData
            string jsonData = Encoding.UTF8.GetString(byteData);

            //Parse jsonData string to myInfo
            JsonList jsonList = JsonUtility.FromJson<JsonList>(jsonData);

            //Create Object as jsonList.data count
            for(int i = 0; i < jsonList.data.Count; i++)
            {
                //Type Cast int to enum 'PrimitiveType'
                GameObject go = GameObject.CreatePrimitive((PrimitiveType)jsonList.data[i].type);

                //Set Random Scale, Transform, Rotation
                go.transform.localScale = jsonList.data[i].scale;
                go.transform.position = jsonList.data[i].pos;
                go.transform.rotation = jsonList.data[i].rot;
            }
        }
    }
}
