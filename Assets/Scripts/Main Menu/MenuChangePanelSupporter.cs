using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuChangePanelSupporter : MonoBehaviour
{
    public void ChangeVerifyScreen() {
        if (MainMenuController.Instance != null) {
            MainMenuController.Instance.ChangePanel(MainMenuPanelID.VerifyScreen);
        }
    }

    public void ChangeSignUp() {
        if (MainMenuController.Instance != null) {
            MainMenuController.Instance.ChangePanel(MainMenuPanelID.SignUp);
        }
    }

    public void ChangeLogIn() {
        if (MainMenuController.Instance != null) {
            MainMenuController.Instance.ChangePanel(MainMenuPanelID.LogIn);
        }
    }

    public void ChangeMainMenu() {
        if (MainMenuController.Instance != null) {
            MainMenuController.Instance.ChangePanel(MainMenuPanelID.MainMenu);
        }
    }

    public void ChangeModuleList() {
        if (MainMenuController.Instance != null) {
            MainMenuController.Instance.ChangePanel(MainMenuPanelID.ModuleList);
        }
    }
}
