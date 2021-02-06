using Mirror;
using Cinemachine;
using UnityEngine;

public class CameraActivateOnStartAutority : NetworkBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _virtualCamera = default;

    public override void OnStartAuthority()
    {
        _virtualCamera.gameObject.SetActive(true);
    }
}
