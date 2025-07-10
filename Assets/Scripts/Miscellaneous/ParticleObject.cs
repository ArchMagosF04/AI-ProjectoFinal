using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
