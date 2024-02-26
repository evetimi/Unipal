using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Sirenix.OdinInspector;

public class LogInButton : MonoBehaviour
{
    [SerializeField] private TMP_Text _usernameText;
    [SerializeField] private TMP_Text _passwordText;

    public void Accept() {
        // TODO: Look for LoginController
        LoginController.Instance.Login(_usernameText.text, _passwordText.text);
    }
}
