using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IodineSublimationReaction : MonoBehaviour
{
    private FireExperimentBeaker beaker;
    private Transform ingredient;
    private float life = 30f; //duman iyice daðýlsýn
    private float reactionDuration;
    private Vector3 scaleRatePerSecond;

    private void Start()
    {
        Debug.Log("Ýyot Süblimleþme Tepkimesi");
        beaker = FireExperimentBeaker.Instance; 
        ingredient = beaker?.GetFirstIngredient();
        //mor duman ve oynatmasý eklenecek

        if (beaker != null && ingredient != null) {
            reactionDuration = beaker.reactionDuration;
            scaleRatePerSecond = ingredient.localScale / reactionDuration;
        }
        else Debug.Log(this + " baþladý ama beaker ve ingredient boþ!");

        transform.position += Vector3.up * 0.01f; //dumanýn beherde az mesafe daha yüksekten baþlamasý için
        Destroy(gameObject, life);
    }

    private void Update() {
        if (ingredient == null) return; //þeklin scale'ini sürekli sýfýrlanmasýn diye ekledik

        reactionDuration -= Time.deltaTime;
        if(reactionDuration >= 0f) {
            //Debug.Log(ingredient.localScale);
            ingredient.localScale -= scaleRatePerSecond * Time.deltaTime;
        }
        else {
            //Debug.Log("þeklin scale'i sýfýrlandý.");
            ingredient.localScale = Vector3.zero;
            ingredient = null;
        }
    }

}
