using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine.AI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private Vector3 destination;

    public Vector3 min, max;

    public NavMeshAgent agent;

    public Animator animator;

    private bool alerta = false;
    
    private bool ataque = false;

    private float angleVision = 120;
    
    void Start()
    {
        // Se genera un punto aleatorio dentro de un rango
        destination = RandomDestination();
        
        // Se mueve al punto generado
        GetComponent<NavMeshAgent>().SetDestination(destination);
        
        // Recuperamos componentes
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetFloat("velocidad", agent.velocity.magnitude);
        
        animator.SetFloat("distancia", Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position));
        
        Vector3 PlayerPosition = GameObject.FindWithTag("Player").transform.position - transform.position;
        
        Vector3 directionToPlayer = PlayerPosition - transform.position;
        
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        
        if (angle < angleVision/2)
        {
            if (Physics.Raycast(transform.position, directionToPlayer, out var hit, 30))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    Debug.Log("Alerta");
                    alerta = true;
                    StopCoroutine(patrol());
                    StartCoroutine(alert());
                }
            }
        }
        else
        {
            Debug.Log("Patrulla");
            alerta = false;
            StopCoroutine(alert());
            StartCoroutine(patrol());
        }
    }

    // Genera un punto aleatorio dentro de un rango
    Vector3 RandomDestination()
    {
        return new Vector3(Random.Range(min.x, max.x), 0, Random.Range(min.z, max.z));
    }

    // Corrutina de ataque
    private IEnumerator attack()
    {
        while (ataque)
        {
            agent.transform.LookAt(GameObject.FindWithTag("Player").transform.position);
            
            //Si la distancia entre el enemigo y el jugador es menor que 2
            if( Vector3.Distance(transform.position, GameObject.FindWithTag("Player").transform.position) < 2)
            {
               
            }
            agent.transform.LookAt(GameObject.FindWithTag("Player").transform.position);
        }
        yield return new WaitForEndOfFrame();
    }
    
    private IEnumerator alert()
    {
        while (alerta)
        {
            agent.transform.LookAt(GameObject.FindWithTag("Player").transform.position);
            
            // Manda al enemigo a por el jugador
            agent.SetDestination(GameObject.FindWithTag("Player").transform.position);
            
            yield return new WaitForEndOfFrame();
        }

    }

    private IEnumerator patrol()
    {
        // Se genera un punto aleatorio dentro de un rango
        Vector3 randomPoint = Random.insideUnitSphere * 50;
        
        // Se le añade la posición del enemigo para que el punto esté en su rango
        NavMeshHit hit;
        
        // Se comprueba si el punto generado está dentro del NavMesh
        NavMesh.SamplePosition(randomPoint, out hit, 50, 1);
        
        // Se comprueba si el punto generado está dentro del NavMesh
        destination = hit.position;
        
        // Se espera 3 segundos
        yield return new WaitForSeconds(3);

        // Se mueve al punto generado
        agent.SetDestination(destination);
        
        yield return new WaitForEndOfFrame();
    }

    private void OnDrawGizmos()
    {
        // Ángulo de visión del enemigo
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, angleVision/2, 0) * transform.forward * 30);
        Gizmos.DrawRay(transform.position, Quaternion.Euler(0, -angleVision/2, 0) * transform.forward * 30);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            alerta = true;
            StopCoroutine(patrol());
            StartCoroutine(alert());
        }*/
    }
    
    private void OnTriggerExit(Collider other)
    {/*
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player lost");
            alerta = false;
            StopCoroutine(alert());
            StartCoroutine(patrol());
        }*/
    }
}