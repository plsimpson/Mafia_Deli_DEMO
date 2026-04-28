using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTriggerAndEnemyCount : DemoSceneLoader
{
    [SerializeField] private BoxCollider triggerCollider;
    [SerializeField] private EnemyCount enemyCount;
    //This will be a script soon

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && enemyCount.CurrentEnemyCount <= 0)
        {
            SceneLoader();
        }
    }
}