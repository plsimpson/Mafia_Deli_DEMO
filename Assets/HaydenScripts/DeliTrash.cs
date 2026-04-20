using UnityEngine;

public class DeliTrash : MonoBehaviour
{
    [SerializeField] private OrderController OrderController;

    public void ClearList()
    {
        Debug.Log("Button clicked!");

        if (OrderController == null)
        {
            Debug.LogError("OrderController is NULL");
            return;
        }

        OrderController.beingBuilt.Clear();
    }
}
