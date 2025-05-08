using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("grabInteracableObject"))
        {
            GameManager.Instance.OuOTriggerFunc();
        }
    }
}
