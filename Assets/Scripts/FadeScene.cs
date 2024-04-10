using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class FadeScene : MonoBehaviour
{
    private Animator animator;
    private const string FADEOUT = "FadeOut";
    private int nextScene;

    private const int START = 0;
    private const int LEVEL_SELECT = 1;
    private const int GAME = 2;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        DialogueManager.Instance.StartGame += Selected_StartGame;
    }

    private void Selected_StartGame(object sender, System.EventArgs e)
    {
        nextScene = LEVEL_SELECT;
        animator.SetTrigger(FADEOUT);
    }

    public void OnBackButton()
    {
        nextScene = START;
        animator.SetTrigger(FADEOUT);
    }

    public void OnBeginButton()
    {
        nextScene = GAME;
        animator.SetTrigger(FADEOUT);
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(nextScene);
    }

}
