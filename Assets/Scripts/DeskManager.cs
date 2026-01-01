using UnityEngine;

public class DeskManager : MonoBehaviour
{//scriptleri enabled false yapacaðýmýz script, butonlara bu script baðlý
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SpiritLamp spiritLamp;

    private MainExperimentSelection mainExperimentSelection;
    
    private void Start()
    {
        mainExperimentSelection = FindAnyObjectByType<MainExperimentSelection>();
        LookAtMainExperimentTable();
    }

    public void LookAtMainExperimentTable() {
        cameraManager.ActivateMainExperimentTableCamera();
        uiManager.DisableLookAtMainExperimentTableButton();
        mainExperimentSelection.ScriptSetActive(true);
        spiritLamp.enabled = false;
    }
    public void LookAtFireExperimentTable() {
        cameraManager.ActivateFireExperimentTableCamera();
        uiManager.DisableLookAtFireExperimentTableButton();
        mainExperimentSelection.ScriptSetActive(false);
        spiritLamp.enabled = true;
    }
    public void LookAtShelf() {
        cameraManager.ActivateShelfCamera();
        uiManager.DisableLookAtShelfButton();
        mainExperimentSelection.ScriptSetActive(false);
        spiritLamp.enabled = false;
    }

}
