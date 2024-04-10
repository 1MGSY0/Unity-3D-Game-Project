using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionDisplay : MonoBehaviour
{

    private TMP_Text mission;
    private GameObject player;
    [SerializeField] private int missionNo;
    [SerializeField] private int initialMissionComplete;

    private void Awake()
    {
        mission = GetComponent<TMP_Text>();
        player = GameObject.Find("Player");
        missionNo = player.GetComponent<PlayerController>().totalMission;
        initialMissionComplete = player.GetComponent<PlayerController>().missionComplete;

    }

    void Start()
    {
        PlayerController.Instance.MissionUpdate += Instance_missionUpdate;
        mission.text = "Mission: " + initialMissionComplete.ToString() + "/" + missionNo.ToString();
    }
    private void Instance_missionUpdate(object sender, PlayerController.MissionUpdateEventArgs e)
    {
        mission.text = "Mission: " + e.missionComplete.ToString() + "/" + missionNo.ToString();
    }
}
