using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DisplayNameSelection : MonoBehaviour {
    [SerializeField] private LayerMask interactableLayer = new LayerMask();
    [SerializeField] private TextMeshProUGUI displayText;

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, interactableLayer)) {
            DisplayName(raycastHit.transform);
        }
        else { //raycast bir þey algýlamazsa
            HideName();
        }

    }

    private void DisplayName(Transform selection) {
        if(selection.TryGetComponent<LabObject>(out LabObject labObject)) {
            displayText.SetText(labObject.GetLabObjectSO().objectName);
        }
        else {
            return;
        }
            //displayText.SetText();
        displayText.gameObject.SetActive(true);
        displayText.transform.position = Input.mousePosition;
    }

    private void HideName() {
        displayText.gameObject.SetActive(false);
    }

}
