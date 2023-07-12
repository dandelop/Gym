using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Ataque : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        _agent.transform.LookAt(GameObject.FindWithTag("Player").transform.position);
    }
    
    void Update()
    {
        _animator.Play("Attack");
    }
}
