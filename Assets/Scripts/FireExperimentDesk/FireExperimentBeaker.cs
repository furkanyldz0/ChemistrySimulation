using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FireExperimentBeaker : MonoBehaviour 
{
    public static FireExperimentBeaker Instance { get; private set; }

    [SerializeField] private SpiritLamp spiritLamp;
    [SerializeField] private FireExperimentSelection fireExperimentSelection;
    [SerializeField] private List<RecipeSO> allRecipes;

    public float reactionDuration { get; private set; } = 1f;

    private RecipeSO currentRecipe;

    private List<LabObject> labObjects = new List<LabObject>();

    private void Start()
    {
        if (Instance != null) {
            Debug.LogError("Sahnedew birden fazla var!: " + this);
        }
        Instance = this;

        spiritLamp.OnLightAction += SpiritLamp_OnLightAction;
        fireExperimentSelection.OnIngredientAdded += FireExperimentSelection_OnIngredientAdded;

    }

    private void SpiritLamp_OnLightAction(object sender, System.EventArgs e) {
        Debug.Log("alkol lambasý yakýldý! malzemeler kontrol ediliyor...");
        CheckRecipes();
    }
    private void FireExperimentSelection_OnIngredientAdded(object sender, FireExperimentSelection.OnIngredientAddedEventArgs e) {
        AddIngredient(e.labObject);
    }

    private void Update() {
    }

    private void AddIngredient(LabObject labObject) {
        if (!labObjects.Contains(labObject)) //ayný malzemeden iki defa koymasýn
            labObjects.Add(labObject);

        //CheckRecipes();
    }


    private void CheckRecipes() { //ekstra malzeme varsa tepkime gerçekleþmez, düzeltilebilir
        List<LabObjectSO> currentLabObjectsSO = new List<LabObjectSO>();

        foreach (LabObject labObject in labObjects) {
            currentLabObjectsSO.Add(labObject.GetLabObjectSO());
        }

        foreach (RecipeSO recipe in allRecipes) {
            // LINQ Logic:
            // 1. Is there any ingredient in the recipe that is NOT in the beaker?
            bool missingIngredients = recipe.requiredIngredients.Except(currentLabObjectsSO).Any();

            // 2. Is there any ingredient in the beaker that is NOT in the recipe?
            bool extraIngredients = currentLabObjectsSO.Except(recipe.requiredIngredients).Any();

            // Check if lists are identical (no missing, no extra, same count)
            if (!missingIngredients && !extraIngredients && currentLabObjectsSO.Count == recipe.requiredIngredients.Count) {
                //PerformReaction(recipe); tepkime süresini beklemek istiyoruz, o yüzden bool ile aktif edeceðiz
                StartReaction(recipe);
                return; // Stop checking after finding a match
            }
        }
    }

    private void StartReaction(RecipeSO recipe) {
        currentRecipe = recipe;
        StartCoroutine(PerformActionCoroutine(2f, currentRecipe));
    }

    private IEnumerator PerformActionCoroutine(float performDuration, RecipeSO currentRecipe) {
        yield return new WaitForSeconds(performDuration);
        PerformReaction(currentRecipe);
    }

    private void PerformReaction(RecipeSO recipe) {
        Debug.Log("Tepkime baþladý: " + recipe.recipeName);
        float yOffset = 0.11f; //güncellenecek, y'de alçak olmasý gerekiyor
        Vector3 reactionPos = transform.position;
        reactionPos.y += yOffset;

        if (recipe.reaction != null) {
            Instantiate(recipe.reaction, reactionPos, Quaternion.identity);
        }

        StartCoroutine(ResetBeakerCoroutine(reactionDuration)); //2sn sonra gerçekleþsin, deðiþkenden tepkimenin de haberi olmasý gerekiyor
    }

        private IEnumerator ResetBeakerCoroutine(float duration) {
            yield return new WaitForSeconds(duration);

        foreach (LabObject ingredient in labObjects) {
            if (!ingredient.GetLabObjectSO().isLiquid)
                Destroy(ingredient.gameObject); //tekrar kullanýlmayan maddeler yok olsun
        }
        //ResetLiquid(); //içindeki sývýnýn sýfýrlanmasýný tepkime scriptleri halletsin
        labObjects.Clear(); //tepkime olunca beherdeki tüm maddeler sýfýrlansýn(sadece liste olarak)
        currentRecipe = null;
    }

    public Transform GetFirstIngredient() { //sadece iyot süblimleþmesi deneyi için
        Transform firstIngredient = null;

        foreach (LabObject ingredient in labObjects) {
            if (!ingredient.GetLabObjectSO().isLiquid)
                firstIngredient = ingredient.transform; //tekrar kullanýlmayan maddeler yok olsun
        }

        return firstIngredient;
    }

}
