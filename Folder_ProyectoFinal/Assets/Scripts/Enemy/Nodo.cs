using UnityEngine;

public class Nodo : MonoBehaviour
{
    [System.Serializable]
    public class PatrolNode
    {
        public Vector3 position;
        public PatrolNode next;
        public PatrolNode prev;

        public PatrolNode(Vector3 pos)
        {
            position = pos;
        }
    }
}
