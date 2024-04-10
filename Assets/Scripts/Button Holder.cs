using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] private Button Button;

    private void Start()
    {
        Button.GetComponentInChildren<TMP_Text>().text = "";
    }
}
