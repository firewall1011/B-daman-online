using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private TransformListSO spawnList = default;

    private void Awake()
    {
        if (spawnList != null)
            spawnList += transform;
        else
            Debug.LogError("assign spawnList object to " + name);
    }

    private void OnDestroy()
    {
        if (spawnList != null)
            spawnList -= transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * transform.localScale.z * 2f);
    }
}
