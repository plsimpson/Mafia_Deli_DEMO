using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnTriggerAndEnemyCount : DemoSceneLoader
{
    [SerializeField] private BoxCollider triggerCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneLoader();
        }
    }
}