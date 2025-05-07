using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject optionsUI;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        optionsUI.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void options()
    {
        if (optionsUI != null)
        {
            optionsUI.SetActive(true);
            Debug.Log("optionsUI is now active: " + optionsUI.activeSelf);
        }
        else
        {
            Debug.LogError("optionsUI is not assigned in GameManager.");
        }
    }
}
