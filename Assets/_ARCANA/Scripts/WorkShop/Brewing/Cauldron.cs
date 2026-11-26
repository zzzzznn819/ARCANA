
using System.Collections.Generic;
using UnityEngine;

public class Cauldron : MonoBehaviour
{
    [Header("Potion Recipes")]
    public List<PotionRecipe> recipes;

    [Header("Potion Spawn Point")]
    public Transform potionSpawnPoint;

    private List<string> ingredients = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        Ingredient item = other.GetComponentInParent<Ingredient>();

        if (item == null)
            return;

        ingredients.Add(item.itemID);

        Debug.Log("加入材料：" + item.itemID);

        Destroy(item.gameObject);
    }


    public void BrewPotion()
    {
        Debug.Log("BrewPotion 已被呼叫");

        foreach (PotionRecipe recipe in recipes)
        {
            HashSet<string> current = new HashSet<string>(ingredients);
            HashSet<string> required = new HashSet<string>(recipe.ingredientIDs);

            if (current.SetEquals(required))
            {
                if (recipe.potionPrefab != null && potionSpawnPoint != null)
                {
                    Instantiate(
                        recipe.potionPrefab,
                        potionSpawnPoint.position,
                        potionSpawnPoint.rotation
                    );

                    Debug.Log("成功製作：" + recipe.potionPrefab.name);
                }

                ingredients.Clear();
                return;
            }
        }

        Debug.Log("配方錯誤，無法製作藥水！");
        ingredients.Clear();
    }
}
