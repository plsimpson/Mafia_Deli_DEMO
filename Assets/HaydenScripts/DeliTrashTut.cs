using UnityEngine;

public class DeliTrashTut : MonoBehaviour
{
    [SerializeField] private Tutorial Tutorial;

    public void ClearList()
    {
        Debug.Log("Button clicked!");

        if (Tutorial == null)
        {
            Debug.LogError("Tutorial is NULL");
            return;
        }

        Tutorial.beingBuilt.Clear();
    }
}
