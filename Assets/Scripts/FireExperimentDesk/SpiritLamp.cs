using System;
using UnityEngine;

[SelectionBase] //sahnede týklandýðýnda parenti seçer
public class SpiritLamp : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private Material fireMaterial;
    [SerializeField] private Material fireMaterialBackup;
    [SerializeField] private LayerMask interactableLayer;
    private FireExperimentSelection fireExperimentSelection;

    public event EventHandler<EventArgs> OnLightAction;

    private bool isBurning;

    private void Start() {
        ResetFlameColor();
        fireExperimentSelection = FindAnyObjectByType<FireExperimentSelection>();
        fireExperimentSelection.OnMetalStickHeld += FireExperimentSelection_OnMetalStickHeld;
        fireExperimentSelection.OnMetalStickReleased += FireExperimentSelection_OnMetalStickReleased;
    }

    private void FireExperimentSelection_OnMetalStickReleased(object sender, EventArgs e) {
        ResetFlameColor();
        Debug.Log("çubuk býrakýldý.");
    }

    private void FireExperimentSelection_OnMetalStickHeld(object sender, FireExperimentSelection.OnIngredientAddedEventArgs e) {
        ChangeFlameColor(e.labObject);
        Debug.Log("çubuk tutuluyor...");
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, interactableLayer)) {
                if (raycastHit.transform == transform) {
                    if (isBurning)
                        ExtinguishLamp();
                    else
                        LightLamp();
                }
            }
        }
    }

    private void ChangeFlameColor(LabObject labObject) {
        var labObjectFlameColor = labObject.GetLabObjectSO().color;
        float intensity = Mathf.Pow(2, 4f); //intensity'nin 4 olmasý için

        fireMaterial.color = labObjectFlameColor * intensity;
        //fireMaterial.color.a = labObjectFlameColor.a * intensity;

    }

    private void ResetFlameColor() {

        fireMaterial.color = fireMaterialBackup.color;
    }
    private void LightLamp() {
        //Debug.Log("Lamba yanýk");
        fireEffect.Play();
        OnLightAction?.Invoke(this, EventArgs.Empty); //malzeme kontrolünü beaker'da yapacaðýmýz için custom args yazmaya gerek yok
        isBurning = true;
    }

    private void ExtinguishLamp() {
        //Debug.Log("lamba sönük");
        fireEffect.Stop();
        isBurning = false;
    }
}
