using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject TitleScreen;
    [SerializeField] private GameObject HowToPlayScreen;
    [SerializeField] private InputField usernameInputField;
    [SerializeField] private Button continueButton;


    public static string currentUsername; //<-- this will be used to reference the username on the game over screen

    private void Start()
    {
        //on startup, check if player has played before and if so, have their previous username entered already

        if(!PlayerPrefs.HasKey("Username")) { return; } //<-- return if first time player

        string previousUsername = PlayerPrefs.GetString("Username");
        usernameInputField.text = previousUsername;
        UsernameInputUpdate(previousUsername); //<--make sure to update interactability of continue button as well
    }

    public void UsernameInputUpdate(string username)
    {
        //this makes sure the username has a non-space character in it in order to be usable
        bool usableName = false;
        for (int i = 0; i < username.Length; i++)
        {
            if (username[i] != ' ')
            {
                usableName = true;
            }
        }

        //this makes sure the first character isn't a space in order for name to be usable
        if (username.Length != 0)
        {
            if (username[0] == ' ')
            {
                usableName = false;
            }
        }

        //after name checks are done, this enables button to begin game if name is usable, otherwise it disables the button
        if (usableName)
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }

    public void Continue()
    {
        //before loading the game scene, save the username to static variable and playerprefs
        currentUsername = usernameInputField.text;
        PlayerPrefs.SetString("Username", currentUsername);

        SceneManager.LoadScene(1);
    }

    public void HowToPlayToggle()
    {
        //toggles between main menu and instructions screen
        TitleScreen.SetActive(!TitleScreen.activeSelf);
        HowToPlayScreen.SetActive(!HowToPlayScreen.activeSelf);
    }
}
