using UnityEngine;
using UnityEngine.AI;
using static DoublylLinkedList;
using static Nodo;

public class EnemyMovement : MonoBehaviour
{
    [Header("Puntos de patrulla (se agregan automáticamente)")]
    public Transform[] patrolPoints;

    private DoublyLinkedPatrolList patrolList = new DoublyLinkedPatrolList();
    private PatrolNode currentNode;
    private bool forward = true;

    private NavMeshAgent agent;
    public float reachDistance = 0.3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false; // Para 2D

        // Construir la lista doblemente enlazada
        foreach (Transform t in patrolPoints)
        {
            patrolList.AddNode(t.position);
        }

        currentNode = patrolList.head;

        if (currentNode != null)
            agent.SetDestination(currentNode.position);
    }

    void Update()
    {
        if (currentNode == null) return;

        // Revisar si ya llegó
        if (!agent.pathPending && agent.remainingDistance < reachDistance)
        {
            SeleccionarSiguienteNodo();
        }
    }

    void SeleccionarSiguienteNodo()
    {
        if (forward)
        {
            // Si hay siguiente nodo, avanzar
            if (currentNode.next != null)
            {
                currentNode = currentNode.next;
            }
            else
            {
                // Llegó al final, entonces, cambiar dirección
                forward = false;
                currentNode = currentNode.prev;
            }
        }
        else
        {
            // Si hay nodo previo, retroceder
            if (currentNode.prev != null)
            {
                currentNode = currentNode.prev;
            }
            else
            {
                // Llegó al inicio, entonces, cambiar dirección
                forward = true;
                currentNode = currentNode.next;
            }
        }

        agent.SetDestination(currentNode.position);
    }
}
