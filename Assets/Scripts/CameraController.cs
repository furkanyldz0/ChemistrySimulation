using UnityEngine;
using Cinemachine; // Cinemachine kütüphanesini eklemeyi unutma!
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private CinemachineVirtualCamera table1Camera;
    [SerializeField] private CinemachineVirtualCamera table2Camera;
    [SerializeField] private CinemachineVirtualCamera shelfCamera;
    [SerializeField] private Button lookAtTable1Button;
    [SerializeField] private Button lookAtTable2Button;
    [SerializeField] private Button LookAtShelfButton;
    [SerializeField] private Transform leftCorner1, leftCorner2, rightCorner1, rightCorner2;
    
    private void Start()
    {
        LookAtTable1();
    }

    public void LookAtShelf()
    {
        shelfCamera.Priority = 12;
        table1Camera.Priority = 11;
        table2Camera.Priority = 10;
        lookAtTable2Button.gameObject.SetActive(true);
        lookAtTable1Button.gameObject.SetActive(true);
        LookAtShelfButton.gameObject.SetActive(false);
        lookAtTable2Button.transform.position = leftCorner1.position;
        lookAtTable1Button.transform.position = leftCorner2.position;
    }

    public void LookAtTable1()
    {
        shelfCamera.Priority = 11;
        table1Camera.Priority = 12;
        table2Camera.Priority = 10;
        lookAtTable1Button.gameObject.SetActive(false);
        lookAtTable2Button.gameObject.SetActive(true);
        LookAtShelfButton.gameObject.SetActive(true);
        LookAtShelfButton.transform.position = rightCorner1.position;
        lookAtTable2Button.transform.position = leftCorner1.position;
    }
    public void LookAtTable2()
    {
        shelfCamera.Priority = 10;
        table1Camera.Priority = 11;
        table2Camera.Priority = 12;
        lookAtTable2Button.gameObject.SetActive(false);
        lookAtTable1Button.gameObject.SetActive(true);
        LookAtShelfButton.gameObject.SetActive(true);
        LookAtShelfButton.transform.position = rightCorner1.position;
        lookAtTable1Button.transform.position = rightCorner2.position;
    }
}