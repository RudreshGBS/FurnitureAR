using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadALD : MonoBehaviour
{
    public ALD ALD;
    public GameObject ALDCanvas;

    private void Awake()
    {
        LoadData();
       
    }
    public void AppQuit() {
        Application.Quit();
    }

    void Save()
    {
        string json = JsonUtility.ToJson(ALD);
        Debug.Log(json);
        File.WriteAllText(Application.dataPath + "/ALD.json", json);
    }
    public void LoadData() {
        StartCoroutine("Load");
    }
    public IEnumerator Load()
    {
        string url = "https://rudreshgbs.github.io/FurnitureAR/ALD.json";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();
        if (!www.isNetworkError)
        {
            Debug.Log(www.downloadHandler.text);
            string json = www.downloadHandler.text;
            ALDModel ALData = JsonUtility.FromJson<ALDModel>(json);
            Debug.Log(ALData.isvalid);
            this.ALD.ALDModel = ALData;
            if (!ALD.ALDModel.isvalid)
            {
                ALDCanvas.SetActive(true);
            }
            else {
                ALDCanvas.SetActive(false);
            }
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
            if (!ALD.ALDModel.isvalid)
            {
                ALDCanvas.SetActive(true);
            }
            else
            {
                ALDCanvas.SetActive(false);
            }
        }       
    }

}
