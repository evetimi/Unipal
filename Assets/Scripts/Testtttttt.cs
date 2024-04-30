using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Testtttttt : MonoBehaviour
{
    public string api = "admin_userSignup.php";
    public TestObject obj;

    [Button]
    public async void Test() {
        string uri = await UnipalClient.DoPostRequestAsync(api, obj);
        Debug.Log(uri);
    }
}

[System.Serializable]
public struct TestObject {
    public string name;
    public string surname;
    public string email;
    public string address;
    public string cellphone;
}