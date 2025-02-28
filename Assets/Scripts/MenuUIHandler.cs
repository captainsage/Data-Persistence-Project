using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{

    public TMP_InputField inputfield;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void StartMainApp()
    {
        RealMainManager.Instance.ActivePlayer = inputfield.text;
        SceneManager.LoadScene(1);
    }

    public void ExitMainApp()
    {
        Debug.Log("checchek: " + (RealMainManager.Instance == null ? "mull" : "ok"));
        RealMainManager.Instance.SavePlayerData();

            #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
            #else
        Application.Quit(); // original code to quit Unity player
            #endif
    }

}
