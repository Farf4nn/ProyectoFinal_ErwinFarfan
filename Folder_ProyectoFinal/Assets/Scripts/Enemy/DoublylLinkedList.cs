using UnityEngine;
using static Nodo;

public class DoublylLinkedList : MonoBehaviour
{
    [System.Serializable]
    public class DoublyLinkedPatrolList
    {
        public PatrolNode head;
        public PatrolNode tail;

        public void AddNode(Vector3 pos)
        {
            PatrolNode n = new PatrolNode(pos);

            if (head == null)
            {
                head = n;
                tail = n;
            }
            else
            {
                tail.next = n;
                n.prev = tail;
                tail = n;
            }
        }
    }
}
