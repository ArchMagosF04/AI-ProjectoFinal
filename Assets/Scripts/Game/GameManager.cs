using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Team Leaders")]
    [SerializeField] private LeaderComponent redAdmiral;
    [SerializeField] private LeaderComponent blueAdmiral;

    public LeaderComponent RedAdmiral => redAdmiral;
    public LeaderComponent BlueAdmiral => blueAdmiral;

    [Header("VictoryScreens")]
    [SerializeField] private GameObject redTeamVictory;
    [SerializeField] private GameObject blueTeamVictory;


    [field: SerializeField] public int redTeamCount { get; private set; }
    [field: SerializeField] public int blueTeamCount { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        redTeamVictory?.SetActive(false);
        blueTeamVictory?.SetActive(false);
    }

    public void RegisterAsRedTeam() => redTeamCount++;
    public void RegisterAsBlueTeam() => blueTeamCount++;

    public void UnregisterFromRedTeam()
    {
        redTeamCount--;

        Debug.Log("Red units remaining:" + redTeamCount);

        if (redTeamCount <= 0)
        {
            Debug.Log("Red Team Lost");
            blueTeamVictory?.SetActive(true);
        }
    }

    public void UnregisterFromBlueTeam()
    {
        blueTeamCount--;

        Debug.Log("Blue units remaining:" + blueTeamCount);

        if (blueTeamCount <= 0)
        {
            Debug.Log("Blue Team Lost");
            redTeamVictory?.SetActive(true);
        }
    }
}
