using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]private GameObject _menu;
    [SerializeField]private GameObject _settingsScreen;

    
    // Start is called before the first frame update
    void Start()
    {
        _menu = GameObject.Find("Menue");
        _settingsScreen = GameObject.Find("Settings Screen");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSettingsButton()
    {
        
    }

    public void OnExitButton()
    {

    }


}
