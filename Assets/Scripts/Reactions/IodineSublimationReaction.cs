using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IodineSublimationReaction : MonoBehaviour
{
    private float life = 5f;
    private FireExperimentBeaker beaker;
    private Transform ingredient;
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

            Destroy(gameObject, life);
    }

    private void Update() {
        if (ingredient == null) return; //þeklin scale'ini sürekli sýfýrlanmasýn diye ekledik

        reactionDuration -= Time.deltaTime;
        if(reactionDuration >= 0f) {
            Debug.Log(ingredient.localScale);
            ingredient.localScale -= scaleRatePerSecond * Time.deltaTime;
        }
        else {
            //Debug.Log("þeklin scale'i sýfýrlandý.");
            ingredient.localScale = Vector3.zero;
            ingredient = null;
        }
    }

}
