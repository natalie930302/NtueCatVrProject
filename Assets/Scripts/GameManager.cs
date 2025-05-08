using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject VideoPlayer;
    private GameObject currentGrabbedObject;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        RegisterAllGrabEvents();
    }

    void RegisterAllGrabEvents()
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable[] allGrabbables = FindObjectsOfType<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        foreach (var grab in allGrabbables)
        {
            grab.selectEntered.AddListener(args => SetGrabbedObject(grab.gameObject));
            grab.selectExited.AddListener(args => ClearGrabbedObject());
        }
    }

    public void SetGrabbedObject(GameObject obj)
    {
        currentGrabbedObject = obj;
        Debug.Log("正在抓取：" + obj.name);
    }

    public void ClearGrabbedObject()
    {
        if (currentGrabbedObject != null)
        {
            Debug.Log("已釋放：" + currentGrabbedObject.name);
            currentGrabbedObject = null;
        }
    }

    public bool IsGrabbingSomething()
    {
        return currentGrabbedObject != null;
    }

    public GameObject GetCurrentGrabbedObject()
    {
        return currentGrabbedObject;
    }

    public void OuOTriggerFunc()
    {
        Debug.Log("放置物品！");
        VideoPlayer.SetActive(true);
    }
}
