using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainExperimentTableCamera;
    [SerializeField] private CinemachineVirtualCamera fireExperimentTableCamera;
    [SerializeField] private CinemachineVirtualCamera shelfCamera;

    public void ActivateShelfCamera()
    {
        shelfCamera.Priority = 12;
        mainExperimentTableCamera.Priority = 11;
        fireExperimentTableCamera.Priority = 10;
    }

    public void ActivateMainExperimentTableCamera()
    {
        shelfCamera.Priority = 11;
        mainExperimentTableCamera.Priority = 12;
        fireExperimentTableCamera.Priority = 10;
    }

    public void ActivateFireExperimentTableCamera()
    {
        shelfCamera.Priority = 10;
        mainExperimentTableCamera.Priority = 11;
        fireExperimentTableCamera.Priority = 12;
    }
}