using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using MiniJSON;
#if WINDOWS_UWP
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
#else
using System.Text;
#endif

public class SettingFileManager : MonoBehaviour
{

    string settingFilePath;

    string settingFileName = "setting.json";

    string folderpath = "";

    public Dictionary<string, object> settingData;

    // 設定ファイル読み込み完了イベント
    public delegate void OnCompleteDelegate(Dictionary<string, object> result);
    public event OnCompleteDelegate CompleteHandler;

    void Start()
    {
        // Invoke("Launch", 1);
    }

    public void Launch()
    {
        Debug.Log("SettingFileManager Launch()");

        settingData = new Dictionary<string, object>();

#if WINDOWS_UWP
        // HoloLensのときは 自分のアプリフォルダの直下
        folderpath = ApplicationData.Current.LocalFolder.Path;
#else
        // Unityのときは Assets 直下
        folderpath = Application.dataPath;
#endif

        settingFilePath = Path.Combine(folderpath, settingFileName);

        Debug.Log("settingFilePath : " + settingFilePath);


        // ファイル存在
        if (!File.Exists(settingFilePath))
        {
            Debug.Log("File.Exists : NG");

            Dictionary<string, object> jsonData = new Dictionary<string, object>();
            jsonData["json_data_version"] = "1.0.0";
            jsonData["endpoint"] = "http://localhost:1880/api/";

            settingData = jsonData;

            WriteFile(jsonData);

            ReadFile();

        } else
        {
            Debug.Log("File.Exists : OK");

            ReadFile();
        }

        // 値の補完
        Debug.LogFormat("json_version : {0}", settingData["json_data_version"]);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WriteFile(Dictionary<string, object> jsonData)
    {
        Debug.Log("WriteFile");

        string jsonDataString = Json.Serialize(jsonData);

#if WINDOWS_UWP
        Debug.Log("WINDOWS_UWP WriteFile");
        var localFolder = ApplicationData.Current.LocalFolder;
        var file = localFolder.CreateFileAsync(settingFileName, CreationCollisionOption.ReplaceExisting).GetAwaiter().GetResult();
        FileIO.WriteTextAsync(file, jsonDataString, UnicodeEncoding.Utf8).GetAwaiter().GetResult();
#else
        Debug.Log("Unity WriteFile");

        var file = new FileInfo(settingFilePath);
        using (var sw = file.CreateText())
        {
            sw.WriteLine(jsonDataString);
        }
#endif
    }

    public void ReadFile()
    {
        string jsonDataString = "";

        Debug.Log("ReadFile");

#if WINDOWS_UWP

        var file = ApplicationData.Current.LocalFolder.GetFileAsync(settingFileName).GetAwaiter().GetResult();

        // if (file == null) return false;

        jsonDataString = FileIO.ReadTextAsync(file, UnicodeEncoding.Utf8).GetAwaiter().GetResult();
#else
        var fi = new FileInfo(settingFilePath);
        try
        {
            using (var sr = new StreamReader(fi.OpenRead(), Encoding.UTF8))
            {
                jsonDataString = sr.ReadToEnd();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
#endif

        Debug.Log(jsonDataString);

        Dictionary<string, object> _settingData = Json.Deserialize(jsonDataString) as Dictionary<string, object>;

        setSettingData(_settingData);

        // コールバック実行
        CompleteHandler?.Invoke(settingData);

    }

    public void setSettingData(Dictionary<string, object> __settingData)
    {
        settingData = __settingData;
    }
}
