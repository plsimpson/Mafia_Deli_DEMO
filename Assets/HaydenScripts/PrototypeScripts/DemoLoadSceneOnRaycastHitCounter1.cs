using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DemoLoadSceneOnRaycastHitCounter1 : MonoBehaviour
{
    [Tooltip("Name of the scene to load when the Counter is hit. Add the scene to __Build Settings__.")]
    public string sceneName = "YourSceneName";

    [Tooltip("Maximum distance for the raycast.")]
    public float maxDistance = 100f;

    [Tooltip("Layer mask used by the raycast (default = Everything).")]
    public LayerMask layerMask = Physics.DefaultRaycastLayers;

    [Tooltip("If true, clicks over UI will be ignored.")]
    public bool blockIfPointerOverUI = true;

    // Prevent multiple load attempts while loading
    private bool isLoading;

    void Update()
    {
        if (isLoading) return;

        if (Input.GetMouseButtonDown(0))
        {
            // Ignore clicks over UI if requested
            if (blockIfPointerOverUI && EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            if (Camera.main == null)
            {
                Debug.LogWarning("DemoLoadSceneOnRaycastHitCounter1: Camera.main is null.");
                return;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Ensure trigger colliders are included
            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask, QueryTriggerInteraction.Collide))
            {
                if (hit.collider != null && hit.collider.CompareTag("Counter"))
                {
                    Debug.Log($"DemoLoadSceneOnRaycastHitCounter1: Hit Counter collider '{hit.collider.name}' at {hit.point} — attempting to load scene '{sceneName}'.");
                    StartCoroutine(LoadSceneCoroutine(sceneName));
                }
            }
        }
    }

    private IEnumerator LoadSceneCoroutine(string name)
    {
        if (isLoading) yield break;
        if (string.IsNullOrEmpty(name))
        {
            Debug.LogWarning("DemoLoadSceneOnRaycastHitCounter1: sceneName is empty. Set a scene in the inspector or add it to __Build Settings__.");
            yield break;
        }

        // Resolve build index for more reliable loading
        int buildIndex = -1;
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string n = Path.GetFileNameWithoutExtension(path);
            if (n == name)
            {
                buildIndex = i;
                break;
            }
        }

        if (buildIndex < 0)
        {
            Debug.LogError($"DemoLoadSceneOnRaycastHitCounter1: Scene '{name}' not found in __Build Settings__ > __Scenes In Build__. Add it there or fix the sceneName.");
            yield break;
        }

        isLoading = true;

        // Use async load so Unity has time to process; prevents strange timing issues
        AsyncOperation op = SceneManager.LoadSceneAsync(buildIndex);
        op.allowSceneActivation = true;

        // Optional: wait until done (keeps isLoading true until load finishes)
        while (!op.isDone)
            yield return null;
    }
}
