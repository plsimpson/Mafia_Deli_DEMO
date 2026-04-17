using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    private Renderer characterRenderer;
    private Color originalColor;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    public SpriteRenderer spriteRenderer;

    void Start()
    {
        //save the original color
        if (spriteRenderer != null)
        {
            originalColor = characterRenderer.material.color;
        }
        else
        {
            Debug.LogError("No Renderer found on the GameObject!");
        }
    }

    // Call this function when the character takes damage
    public void FlashRed()
    {
        // Stop any existing flash coroutine to prevent conflicts
        StopCoroutine(FlashRoutine());
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        // Change color to flash color
        characterRenderer.material.color = flashColor;

        // Wait for the specified duration
        yield return new WaitForSeconds(flashDuration);

        // Change back to the original color
        characterRenderer.material.color = originalColor;
    }
}