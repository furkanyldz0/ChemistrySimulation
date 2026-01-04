using DG.Tweening;
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class FireExperimentSelection : MonoBehaviour {
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public LabObject labObject;
    }
    [SerializeField] private LayerMask interactableLayer = new LayerMask();
    [SerializeField] private Transform addingPositionTransform;
    private Transform selectedIngredient, highlight;

    private float addingTime = .5f;
    private float addingTimeCounter;

    private Vector3 lastIngredientPosition;
    private Quaternion lastIngredientRotation;

    private bool isAdding;

    private void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            if (!EventSystem.current.IsPointerOverGameObject() &&
                Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity, interactableLayer)) {
                highlight = raycastHit.transform;
                if (highlight.TryGetComponent<LabObject>(out LabObject labObject)) { //highlight.CompareTag("Item")
                    if (!isAdding) {
                        if (!labObject.GetLabObjectSO().isLiquid)
                            AddIngredient(highlight);
                        /*else
                            AddReusableIngredient(highlight);
                         sývýlar için, alkol lambasý ile yapýlacak deneylerde sývý
                         kullanýmý þuanlýk yok */
                    }
                }
            }
            else { 
                highlight = null;
            }
        }

        HandleAddingIngredient();
    }

    private void AddIngredient(Transform highlight) {
        selectedIngredient = highlight;

        float scaleMultiplier = 0.5f;
        var objectScale = selectedIngredient.localScale;
        objectScale = new Vector3(objectScale.x * scaleMultiplier, objectScale.y * scaleMultiplier, objectScale.z * scaleMultiplier); //ateþ deneyindeki beher küçük olduðu için malzemeler büyük geliyor, küçültme iþlemi yapýlmasý gerekiyor
        selectedIngredient.localScale = objectScale;

        selectedIngredient.position = addingPositionTransform.position;

        AdjustRigidbodies(selectedIngredient);

        addingTimeCounter = addingTime / 4;
        isAdding = true;

        Debug.Log("Katý malzeme ekleniyor... " + this.ToString());
    }

    private void AdjustRigidbodies(Transform transform) {
        var rb = transform.GetComponent<Rigidbody>();

        if (transform.GetComponent<LabObject>().GetLabObjectSO().hasMultipleMeshes) {
            Destroy(rb); //parent'deki rb
            Destroy(transform.GetComponent<Collider>()); //parent'de collider olmak zorunda

            BoxCollider c = transform.AddComponent<BoxCollider>();
            c.size = new Vector3(0.01f, 0.01f, 0.01f);

            var colliders = transform.GetComponentsInChildren<Collider>();
            foreach (Collider col in colliders) {
                if (col.transform == transform) continue;

                col.enabled = true;
                col.transform.AddComponent<Rigidbody>();
            }
        }
        else {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    private void HandleAddingIngredient() {
        if (isAdding) {
            addingTimeCounter -= Time.deltaTime;
            if (selectedIngredient != null && addingTimeCounter <= 0f) {
                //malzemenin masadaki konumunu ve rotasyonuna geri döndürüyor
                if (selectedIngredient.TryGetComponent(out LabObject labObject)
                    && labObject.GetLabObjectSO().isLiquid) {
                    selectedIngredient.position = lastIngredientPosition;
                    selectedIngredient.rotation = lastIngredientRotation;
                }

                Debug.Log("Malzeme eklendi!");

                TryAddingIngredientToBeaker(selectedIngredient);
                selectedIngredient = null;
                isAdding = false;

            }
        }
    }
    private void TryAddingIngredientToBeaker(Transform selectedIngredient) { //karýþým hazýrlanmak istiyorsa
        if (selectedIngredient.TryGetComponent(out LabObject labObject)) {   //muhakkak nesnelerde labobject scripti olmalý
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs { //yoksa malzeme eklendi sayýlmaz
                labObject = labObject
            });
        }
    }

    public void ScriptSetActive(bool check) {
        if (check) {
            enabled = true;
            //Debug.Log("MainExperimentSelection.enabled = true");
        }
        else
            StartCoroutine(DisableScriptRoutine(addingTimeCounter)); //malzemeyi koyarken deaktive olmasýný engellemek için routine kullanýyoruz

    }
    private IEnumerator DisableScriptRoutine(float duration) {
        yield return new WaitForSeconds(duration);
        enabled = false;
        //Debug.Log("MainExperimentSelection.enabled = false");
    }//bug var!
     //eðer sývý dökerken hemen masa deðiþtirip ana masaya geri döndüðünde script false kalýyor

}
