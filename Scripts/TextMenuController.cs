using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class TextMenuController : MonoBehaviour
{
    public TMP_Text[] menuOptions; // Array of TextMeshPro texts for "NEW GAME," "OPTIONS," "EXIT"
    private int selectedIndex = 0;

    void Start()
    {
        UpdateMenuSelection();
        Debug.Log("Starting in MainMenu");
    }
    void Update()
    {
        // Navigate up/down with arrow keys or W/S
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex--;
            if (selectedIndex < 0) selectedIndex = menuOptions.Length - 1;
            UpdateMenuSelection();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex++;
            if (selectedIndex >= menuOptions.Length) selectedIndex = 0;
            UpdateMenuSelection();
        }

        // Select with Enter or Space
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            HandleSelection();
        }
    }

    void UpdateMenuSelection()
    {
        Color highlightedColor;
        ColorUtility.TryParseHtmlString("#FECB95", out highlightedColor); 
        Color normalColor;
        ColorUtility.TryParseHtmlString("#ED7A00", out normalColor);
        for (int i = 0; i < menuOptions.Length; i++)
        {
            if (i == selectedIndex)
            {
                menuOptions[i].color = highlightedColor;
                
            }
            else
            {
                menuOptions[i].color = normalColor;
                
            }
        }
    }
   
    void HandleSelection()
    {
        switch (selectedIndex)
        {
            case 0: // NEW GAME
                SceneManager.LoadScene("Bomberman"); 
                break;
            case 1: // OPTIONS
                Debug.Log("Options menu not implemented yet!");
                break;
            case 2: // EXIT
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false; // For testing in Editor
#endif
                break;
        }
    }
}