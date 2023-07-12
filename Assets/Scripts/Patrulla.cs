using UnityEngine.AI;
using UnityEngine;
using Random = UnityEngine.Random;

public class Patrulla : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;

    public Vector3 coordinates;
    public Vector2 mapSize;

    void Start()
    {
        // Recogemos el componente de navegación
        agent = GetComponent<NavMeshAgent>();
        
        // Obtenemos el tamaño del mapa
        //mapSize = Generator2.instance.getMapSize();

        // Generamos un punto nuevo en el mapa
        NewRoutePoint();
        
        // Mandamos al enemigo a ese punto
        agent.SetDestination(coordinates);
    }

    void Update()
    {
        // Cuando ha llegado al punto, generamos uno nuevo
        if (Vector3.Distance(transform.position, coordinates) < 3)
        {
            NewRoutePoint();
            agent.SetDestination(coordinates);
        }
    }
    
    private void NewRoutePoint() 
    {
        // Obtención de coordenadas
        float pointX = Random.Range(0, (int)mapSize.x * 5);
        float pointZ = Random.Range(0, (int)mapSize.y * 5);
        
        // Comprobamos que el punto está dentro del mapa
        NavMeshHit hit;
        
        NavMesh.SamplePosition(new Vector3(pointX, transform.position.y, pointZ), out hit, 50, 1);
        
        // Asignamos las nuevas coordenadas
        coordinates = hit.position;
    }
}