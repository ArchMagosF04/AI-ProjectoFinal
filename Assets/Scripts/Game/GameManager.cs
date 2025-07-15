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

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
