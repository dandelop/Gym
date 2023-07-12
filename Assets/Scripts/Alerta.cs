using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alerta : MonoBehaviour
{
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        // Persigue al jugador
        agent.SetDestination(GameObject.FindWithTag("Player").transform.position);
    }
}
