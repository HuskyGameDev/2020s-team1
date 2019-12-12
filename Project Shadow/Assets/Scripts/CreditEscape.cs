using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditEscape : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.SwitchMusic("MenuBGM", "CreditMusic");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Loading Menu......");
            SceneManager.LoadScene("Menu");
            AudioManager.instance.SwitchMusic("CreditMusic", "MenuBGM");
        }
    }
}
