using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class ShelfIngredientSelection : MonoBehaviour
{
    [SerializeField] private LayerMask interactableLayer = new LayerMask();

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) 
            && Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, interactableLayer)) {
            if(raycastHit.transform.TryGetComponent<LabObject>(out LabObject labObject)) {
                MoveLabObjectToDesk(labObject);
            }
        }

    }

    private void MoveLabObjectToDesk(LabObject labObject) {
        labObject.transform.position = labObject.GetLabObjectSO().deskPosition;
    }

}
