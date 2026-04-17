using System.Collections;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    public float health = 100f; // Accessible by derived classes and in the Inspector
    protected float speed = 2f; // Accessible only by derived classes within the code

    public SpriteRenderer characterRenderer;
    private Color originalColor;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private Coroutine flashCoroutine;

    void Start()
    {
        //save original color
        if (characterRenderer != null)
        {
            originalColor = Color.white;
        }
        else
        {
            Debug.LogError("No Renderer found on the GameObject!");
        }
    }

    // Call this function when the character takes damage
    public void FlashRed()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }
        flashCoroutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        // Change color to flash color
        characterRenderer.color = flashColor;

        // Wait for the specified duration
        yield return new WaitForSeconds(flashDuration);

        // Change back to the original color
        characterRenderer.color = Color.white; ;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        FlashRed();
        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log("Character Died");
    }
}