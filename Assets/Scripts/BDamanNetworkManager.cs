using System.Collections;
using UnityEngine;
using Mirror;

namespace Assets.Scripts
{
    public class BDamanNetworkManager : NetworkManager
    {

        public override void OnServerAddPlayer(NetworkConnection conn)
        {
            Transform startPos = GetStartPosition();
            GameObject player = startPos != null
                ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
                : Instantiate(playerPrefab);

            NetworkServer.AddPlayerForConnection(conn, player);
            
            if(player.TryGetComponent<BDamanController>(out var controller))
            {
                controller.CmdSetOrientation(startPos.right, startPos.localEulerAngles.y);
            }
        }
    }
}