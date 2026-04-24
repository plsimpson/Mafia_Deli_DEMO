using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sandwich", menuName = "Scriptable Objects/Sandwich")]
public class Sandwich : ScriptableObject
{
    public List<Ingredient> Ingredients = new List<Ingredient>();
}