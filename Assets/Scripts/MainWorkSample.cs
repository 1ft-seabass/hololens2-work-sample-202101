using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWorkSample : MonoBehaviour
{
    SettingFileManager settingFileManager;

    string UnityAppVersion = "1.0.1";

    void Start()
    {
        // 少し待たないと LogManager でログが出ない
        Invoke("Launch", 0.5f);
    }

    void Launch()
    {
        Debug.Log("MainWorkSample Launch()");

        Debug.LogFormat("UnityAppVersion:{0}", UnityAppVersion);

        settingFileManager = GameObject.Find("SettingFileManager").GetComponent<SettingFileManager>();

        settingFileManager.CompleteHandler += OnSettingFileManagerComplete;

        settingFileManager.Launch();
    }
    private void OnSettingFileManagerComplete(Dictionary<string, object> result)
    {
        Debug.Log("OnSettingFileManagerComplete");
    }

    void Update()
    {
        
    }
}
