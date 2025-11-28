using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float rango = 10f;     // Qué tan lejos puede buscar un nuevo punto
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false; // Para 2D
        NuevoDestino();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            NuevoDestino();
        }
    }

    void NuevoDestino()
    {
        Vector3 puntoRandom = PuntoAleatorio(transform.position, rango);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(puntoRandom, out hit, 2f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }

    // Genera un punto aleatorio dentro de un círculo
    Vector3 PuntoAleatorio(Vector3 origen, float distancia)
    {
        Vector2 random2D = Random.insideUnitCircle * distancia;
        return new Vector3(origen.x + random2D.x, origen.y + random2D.y, 0);
    }
}
