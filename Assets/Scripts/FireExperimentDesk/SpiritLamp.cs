using UnityEngine;

[SelectionBase] //sahnede týklandýðýnda parenti seçer
public class SpiritLamp : MonoBehaviour
{
    [SerializeField] private ParticleSystem fireEffect;
    [SerializeField] private LayerMask interactableLayer;

    private bool isBurning;

    /*
    1-FireExperimentBeaker scripti oluþturulacak
    2-spiritlamp scripti her lamba açýldýðýnda event tetikleyecek, 
        bu eventle beher scripti de malzemeleri kontrol edecek
    3-malzemeler tam ise iyot süblimleþmesi beaker scriptinde baþlatýlacak
    4-selection scripti yazýldýðýnda (veya var olan scipt düzenlenerek,
        ama coroutine ile yapýcam) alev çubuðu scripti yazmak yerine tip
        kontrolü yapýlabilir
    5-raf kýsmýnda malzemelerin hepsi dizilecek, objelerin üstüne geldiðinde ismiyle
        beraber açýklamasý yer alacak ve üzerine bir kere týklandýðýnda "þuraya koy:ana
        veya ateþ deneyi masasý" þeklinde seçenekler çýkacak.
        --sývý ile ateþ deneyi þimdilik olmayacaðýndan onlarda çýkmayabilir
    6-katý malzemeler ilk kullanýldýðýnda yok olacak (raftan tekrar alýnabilir), sývýlar
        ise masada kalacak, sað klik ile rafa tekrar konabilir
        
     */

    private void Start() {
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

    private void LightLamp() {
        //Debug.Log("Lamba yanýk");
        fireEffect.Play();
        isBurning = true;
    }

    private void ExtinguishLamp() {
        //Debug.Log("lamba sönük");
        fireEffect.Stop();
        isBurning = false;
    }
}
