using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskManager : MonoBehaviour
{//scriptleri enabled false yapacaðýmýz script, butonlara bu script baðlý
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private UIManager uiManager;
    
    private void Start()
    {
        LookAtMainExperimentTable();
    }

    public void LookAtMainExperimentTable() {
        cameraManager.ActivateMainExperimentTableCamera();
        uiManager.DisableLookAtMainExperimentTableButton();
    }
    public void LookAtFireExperimentTable() {
        cameraManager.ActivateFireExperimentTableCamera();
        uiManager.DisableLookAtFireExperimentTableButton();
    }
    public void LookAtShelf() {
        cameraManager.ActivateShelfCamera();
        uiManager.DisableLookAtShelfButton();
    }

}
