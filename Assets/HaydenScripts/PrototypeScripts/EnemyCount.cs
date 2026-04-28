using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    [SerializeField] private Canvas enemyCountCanvas;
    [SerializeField] private BoxCollider triggerCollider;
    public float CurrentEnemyCount;

    private void Start()
    {
        enemyCountCanvas.enabled = false;
        triggerCollider.enabled = false;
    }

    private void Update()
    {
        if (CurrentEnemyCount > 0)
        {
            enemyCountCanvas.enabled = false;
            triggerCollider.enabled = false;
        }
        else if (CurrentEnemyCount <= 0)
        {
            enemyCountCanvas.enabled = true;
            triggerCollider.enabled = true;
        }
    }
}