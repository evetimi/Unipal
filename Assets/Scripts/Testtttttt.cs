using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Networking;

public class Testtttttt : MonoBehaviour
{
    public string api = "admin_userSignup.php";
    public TestObject obj;
    // public string apiUrl = "http://13.60.15.202/api/download_file.php"; // Replace with the URL to your PHP script
    // public string filename = "example.txt"; // Replace with the filename you want to download
    // public string moduleId = "your_module_id"; // Replace with the module ID
    // public string weekNumber = "your_week_number"; // Replace with the week number

    [Button]
    public async void Test() {
        ApiSendObject<TestObject> send = new ApiSendObject<TestObject>() {
            body = obj
        };
        var receive = await UnipalClient.DoPostRequestAsync<TestObject, string>(api, send);
        Debug.Log($"Success: {receive.receiveMessageSuccess} ; failed: {receive.failedMessage} ; body: {receive.receivedMessage.body}");
    }

    // [Button]
    // public void TestDownload() {
    //     StartCoroutine(qqqqqq());
    // }

    IEnumerator qqqqqq()
    {
        // string url = $"{apiUrl}?filename={filename}&module_id={moduleId}&week_number={weekNumber}";
        // UnityWebRequest www = UnityWebRequest.Get(url);
        // yield return www.SendWebRequest();

        // if (www.result == UnityWebRequest.Result.Success)
        // {
        //     // Save the file to disk
        //     string filePath = Path.Combine(Application.dataPath, filename);
        //     File.WriteAllBytes(filePath, www.downloadHandler.data);
        //     Debug.Log(www.responseCode);
        //     Debug.Log("File downloaded successfully. Location: " + filePath);
        // }
        // else
        // {
        //     Debug.LogError($"Failed to download file. Error: {www.error}");
        // }
    }
}

[System.Serializable]
public struct TestObject {
    public string name;
    public string surname;
    public string email;
    public string address;
    public string cellphone;
    public DateTime dob;
}