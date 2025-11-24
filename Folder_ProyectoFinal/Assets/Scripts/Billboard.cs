using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] Transform target;
    void Update()
    {
        if (target != null)
        {
            transform.LookAt(target);
        }
    }
}
