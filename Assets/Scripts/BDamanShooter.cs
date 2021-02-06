using UnityEngine;
using Mirror;

namespace BDaman
{
    public class BDamanShooter : NetworkBehaviour
    {
        [SerializeField] private InputReader inputReader = default;
        [SerializeField] private GameObject ballPrefab = default;
        [SerializeField] private Transform shootingPoint = default;
        public override void OnStartAuthority()
        {
            inputReader.FireEvent += CmdShoot;
        }

        public override void OnStopAuthority()
        {
            inputReader.FireEvent -= CmdShoot;
        }

        [Command]
        private void CmdShoot()
        {
            GameObject ball = Instantiate(ballPrefab, shootingPoint.position, transform.rotation);
            NetworkServer.Spawn(ball);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            const float radius = .5f;
            Gizmos.DrawWireSphere(shootingPoint.position, radius);

            Gizmos.color = Color.blue;
            const float lineDistance = 5f;
            Gizmos.DrawLine(shootingPoint.position, shootingPoint.position + transform.forward * lineDistance);
        }
#endif
    }

}