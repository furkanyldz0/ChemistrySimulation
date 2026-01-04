using UnityEngine;

public class DeskManager : MonoBehaviour
{//scriptleri enabled false yapacaðýmýz script, butonlara bu script baðlý
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private SpiritLamp spiritLamp;

    private MainExperimentSelection mainExperimentSelection;
    private FireExperimentSelection fireExperimentSelection;
    private ShelfIngredientSelection shelfIngredientSelection;
    
    private void Start()
    {
        mainExperimentSelection = FindAnyObjectByType<MainExperimentSelection>();
        fireExperimentSelection = FindAnyObjectByType<FireExperimentSelection>();
        shelfIngredientSelection = FindAnyObjectByType<ShelfIngredientSelection>();
        LookAtMainExperimentTable();
    }

    public void LookAtMainExperimentTable() {
        cameraManager.ActivateMainExperimentTableCamera();
        uiManager.DisableLookAtMainExperimentTableButton();
        mainExperimentSelection.ScriptSetActive(true);
        fireExperimentSelection.ScriptSetActive(false); //metotta bug var, düzeltilecek
        shelfIngredientSelection.enabled = false;
        spiritLamp.enabled = false;
    }
    public void LookAtFireExperimentTable() {
        cameraManager.ActivateFireExperimentTableCamera();
        uiManager.DisableLookAtFireExperimentTableButton();
        mainExperimentSelection.ScriptSetActive(false);
        fireExperimentSelection.ScriptSetActive(true);
        shelfIngredientSelection.enabled = false;
        spiritLamp.enabled = true;
    }
    public void LookAtShelf() {
        cameraManager.ActivateShelfCamera();
        uiManager.DisableLookAtShelfButton();
        mainExperimentSelection.ScriptSetActive(false);
        fireExperimentSelection.ScriptSetActive(false);
        shelfIngredientSelection.enabled = true;
        spiritLamp.enabled = false;
    }

}
