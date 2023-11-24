using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviourPunCallbacks
{
    public bool isNextScene = false;
    int curScene;
    // Start is called before the first frame update
    void Start()
    {
        curScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    public void LoadSceneButton()
    {
        //isNextScene = true;

        if(curScene >= 5)
        {
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.LeaveLobby();
        }

        SceneManager.LoadScene(curScene + 1);
    }
}
