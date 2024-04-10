using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class BackToStart : MonoBehaviour
{
    //Packages
    private CustomActions input;
    private const string BACK = "Exit";
    private const string NEXT = "Interactive";
    private int prevScene = 0;
    private int nextScene = 2;

    private void Awake()
    {
        input = new CustomActions();
        AssignInputs();
    }
    private void AssignInputs()
    {
        input.UI.Click.performed += ctx => Clicked();
    }


    private void Clicked()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition), out hit, 100))
        {
            if (hit.collider.CompareTag(BACK))
            {
                SceneManager.LoadScene(prevScene);
            }
            if (hit.collider.CompareTag(NEXT))
            {
                SceneManager.LoadScene(nextScene);
            }
        }

    }
    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }


}
