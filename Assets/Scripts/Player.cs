
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;
    private bool firstClick = true;
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }
    
    void Update()     // Completar con firstClick
    {   
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.distance <= 2)
                {
                    _agent.SetDestination(hit.point);
                }
            }
        }
    }
}
