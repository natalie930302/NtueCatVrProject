using UnityEngine;
using UnityEngine.InputSystem; // **** 引入 Input System ****
using UnityEngine.UI;
using TMPro; // 引入 TextMeshPro

public class SimpleHandController : MonoBehaviour
{
    // --- Input Action References ---
    [Header("Input Actions")]
    public InputActionReference moveActionRef;
    public InputActionReference grabActionRef;
    public InputActionReference placeActionRef;

    [Header("Movement Settings")]
    public float moveSpeed = 5.0f;

    // --- 抓取相關 ---
    private GameObject potentialGrabTarget = null;
    private GameObject heldObject = null;

    // --- 計分相關 ---
    [Header("Scoring")]
    public int pointsForGoodZone = 15;
    public int pointsForBadZone = -1;
    // public int pointsForDrop = 0;
    public float scoringRadius = 2.0f;

    private Collider handCollider;

    //UI menu
    public GameManager gameManager; // 引用 GameManager 腳本
    // [Header("UI Options")]
    // public GameObject optionPanel; // Example field for UI Menu
    // public Button grabButton; // Example field for UI Button
    // public Button placeButton; // Example field for UI Button
    // public TextMeshProUGUI optionTitleText; // Example field for UI Text

    void Awake() // 使用 Awake 確保 Action References 被檢查
    {
        // 檢查 Input Action References 是否已設定
        if (moveActionRef == null || moveActionRef.action == null) Debug.LogError("Move Action Reference not set!", this);
        if (grabActionRef == null || grabActionRef.action == null) Debug.LogError("Grab Action Reference not set!", this);
        if (placeActionRef == null || placeActionRef.action == null) Debug.LogError("Place Action Reference not set!", this);

        handCollider = GetComponent<Collider>();
        if (handCollider == null) Debug.LogError("SimpleHandController requires a Collider.", this);
        // 仍然建議 Trigger 用於鍵盤滑鼠測試
        if (handCollider != null && !handCollider.isTrigger)
        {
             Debug.LogWarning("PlayerHand's Collider is not 'Is Trigger'. OnTriggerEnter/Exit recommended.", this);
        }

        if (gameManager == null)
    {
        gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found in the scene. Please assign it in the Inspector.", this);
        }
    }

        // // 設定 UI 元件的初始狀態
        // if (optionPanel != null) Debug.LogError("Option Panel not set!", this);
        // else optionPanel.SetActive(false); // 隱藏選單面板
        // if (grabButton != null) Debug.LogError("Grab Button not set!", this);
        // if (placeButton != null) Debug.LogError("Place Button not set!", this);

        
    }

    void OnEnable()
    {
        // 啟用 Actions 並註冊事件回呼 (用於按鈕類型)
        if (grabActionRef != null && grabActionRef.action != null)
        {
            grabActionRef.action.Enable();
            grabActionRef.action.performed += OnGrabPerformed; // 按下時觸發
        }
        if (placeActionRef != null && placeActionRef.action != null)
        {
            placeActionRef.action.Enable();
            placeActionRef.action.performed += OnPlacePerformed; // 按下時觸發
        }
         if (moveActionRef != null && moveActionRef.action != null)
        {
             moveActionRef.action.Enable(); // 移動 Action 只需要啟用即可在 Update 中讀取
        }
    }

    void OnDisable()
    {
        // 取消註冊事件回呼並禁用 Actions
        if (grabActionRef != null && grabActionRef.action != null)
        {
            grabActionRef.action.performed -= OnGrabPerformed;
            grabActionRef.action.Disable();
        }
        if (placeActionRef != null && placeActionRef.action != null)
        {
            placeActionRef.action.performed -= OnPlacePerformed;
            placeActionRef.action.Disable();
        }
         if (moveActionRef != null && moveActionRef.action != null)
        {
             moveActionRef.action.Disable();
        }
    }

    void Update()
    {
        // --- 處理移動 (從 Action 讀取值) ---
        HandleMovement();
        // Grab 和 Place 的邏輯現在由事件回呼觸發，不再需要在 Update 中檢查按鍵
    }

    void HandleMovement()
    {
         if (moveActionRef == null || moveActionRef.action == null || !moveActionRef.action.enabled) return;

        // 讀取 Move Action 的 Vector2 值
        Vector2 moveInput = moveActionRef.action.ReadValue<Vector2>();
        Vector3 movement = new Vector3(moveInput.x, 0.0f, moveInput.y);
        transform.Translate(movement * moveSpeed * Time.deltaTime, Space.World);
    }

    // --- Input Action 事件回呼 ---
    private void OnGrabPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Grab action performed");
        TryGrab();
    }

    private void OnPlacePerformed(InputAction.CallbackContext context)
    {
         Debug.Log("Place action performed");
         TryPlace();
    }


    // --- 將 Grab/Place 邏輯封裝 ---
    private void TryGrab()
    {
        bool canGrab = heldObject == null && potentialGrabTarget != null;
        if (canGrab)
        {
            GrabObject(potentialGrabTarget);
            if (gameManager != null)
            {
                Debug.Log("GameManager found. Calling options().");
                if (gameManager.optionsUI != null)
                {
                    gameManager.options();
                }
                else
                {
                    Debug.LogError("optionsUI is null in GameManager.", gameManager);
                }
            }
            else
            {
                Debug.LogError("GameManager reference is null. Cannot call options().", this);
            }
        }
        else if (heldObject != null)
        {
            Debug.Log("Already holding an object. Perform 'Place' action first.");
        }
        else
        {
            Debug.Log("Nothing in range to grab.");
        }
    }

    private void TryPlace()
    {
         if (heldObject != null)
        {
            PlaceObject();
        }
        else
        {
            Debug.Log("Not holding any object to place.");
        }
    }


    // --- Trigger 偵測 (保持不變) ---
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grabbable") && heldObject == null)
        {
            Debug.Log("Hand entered range of grabbable: " + other.gameObject.name);
            potentialGrabTarget = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == potentialGrabTarget)
        {
            Debug.Log("Hand left range of grabbable: " + other.gameObject.name);
            potentialGrabTarget = null;
        }
    }

    // --- 抓取與放置 (保持不變) ---
    void GrabObject(GameObject objectToGrab)
    {
        heldObject = objectToGrab;
        potentialGrabTarget = null;
        heldObject.transform.SetParent(this.transform);
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
        heldObject.transform.localPosition = Vector3.up * 1.0f;
        Debug.Log("Grabbed: " + heldObject.name);
    }

    void PlaceObject()
    {
        Debug.Log("Attempting to place: " + heldObject.name);
        GameObject objectToPlace = heldObject;
        Rigidbody rb = objectToPlace.GetComponent<Rigidbody>();
        objectToPlace.transform.SetParent(null);
        if (rb != null) rb.isKinematic = false;
        heldObject = null;
        CheckPlacementScore(objectToPlace);
    }

    // --- 檢查放置分數邏輯 (保持不變) ---
    void CheckPlacementScore(GameObject placedObject)
    {
         Collider placedCollider = placedObject.GetComponent<Collider>();
         if (placedCollider == null) return;
         PerformScoreCheck(placedObject, placedCollider);
         // StartCoroutine(CheckScoreAfterDelay(placedObject, 0.05f)); // 可選延遲
    }

    void PerformScoreCheck(GameObject placedObject, Collider placedCollider) // placedCollider 參數現在不是必需，但保留著也無妨
    {
        Debug.Log($"--- PerformScoreCheck (Proximity Check) for {placedObject.name} ---");

        // 基本檢查
        if (placedObject == null) { Debug.LogError("Placed object is null!"); return; }
        if (ScoreManager.Instance == null) { Debug.LogError("ScoreManager instance is null!"); return; }

        // 獲取物品放置後的位置
        Vector3 placedPosition = placedObject.transform.position;
        bool scored = false;

        Debug.Log($"Placed object position: {placedPosition}. Checking zones within radius: {scoringRadius}");

        // --- 檢查與所有 "GoodZone" 的距離 ---
        GameObject[] goodZones = GameObject.FindGameObjectsWithTag("GoodZone"); // 找到場景中所有 Tag 為 "GoodZone" 的物件
        Debug.Log($"Found {goodZones.Length} GoodZone(s).");

        foreach (GameObject zone in goodZones)
        {
            // 計算物品位置與區域中心點的距離
            // 注意：zone.transform.position 是區域物件的中心點
            float distance = Vector3.Distance(placedPosition, zone.transform.position);
            Debug.Log($"  Distance to GoodZone '{zone.name}' at {zone.transform.position}: {distance}");

            // 如果距離小於等於我們設定的計分半徑
            if (distance <= scoringRadius)
            {
                Debug.Log($"!!!!!! NEAR GoodZone ({zone.name})! Distance <= {scoringRadius} !!!!!!");
                ScoreManager.Instance.ModifyScore(pointsForGoodZone);
                Debug.Log($"!!!!!! Score modified by {pointsForGoodZone}. Should be updated. !!!!!!");
                scored = true;
                break; // 找到一個符合條件的就得分，不再檢查其他 GoodZone
            }
        }

        // --- 如果還沒在 GoodZone 得分，再檢查與所有 "BadZone" 的距離 ---
        if (!scored)
        {
            GameObject[] badZones = GameObject.FindGameObjectsWithTag("BadZone"); // 找到場景中所有 Tag 為 "BadZone" 的物件
            Debug.Log($"Found {badZones.Length} BadZone(s).");

            foreach (GameObject zone in badZones)
            {
                float distance = Vector3.Distance(placedPosition, zone.transform.position);
                Debug.Log($"  Distance to BadZone '{zone.name}' at {zone.transform.position}: {distance}");

                if (distance <= scoringRadius)
                {
                    Debug.Log($"!!!!!! NEAR BadZone ({zone.name})! Distance <= {scoringRadius} !!!!!!");
                    ScoreManager.Instance.ModifyScore(pointsForBadZone);
                    Debug.Log($"!!!!!! Score modified by {pointsForBadZone}. Should be updated. !!!!!!");
                    scored = true;
                    break; // 找到一個符合條件的就給分，不再檢查其他 BadZone
                }
            }
        }

        // --- Log 最終結果 ---
        if (scored)
        {
            Debug.Log($"--- PerformScoreCheck (Proximity): Scored processed for {placedObject.name} ---");
        }
        else
        {
            // 如果距離所有 GoodZone 和 BadZone 都太遠
            Debug.Log($"--- PerformScoreCheck (Proximity): {placedObject.name} dropped too far from any designated zone (Radius: {scoringRadius}). No score change. ---");
        }
        Debug.Log("--- PerformScoreCheck (Proximity) Finished ---");
    }


    // void PerformScoreCheck(GameObject placedObject, Collider placedCollider)
    // {
    //     if (placedCollider == null || ScoreManager.Instance == null) return;

    //     // 取得放置物體的邊界
    //     Bounds currentBounds = placedCollider.bounds;

    //     // 使用 OverlapBox 檢查重疊區域
    //     Collider[] overlappingColliders = Physics.OverlapBox(
    //         currentBounds.center,
    //         currentBounds.extents / 2, // 減小範圍避免過大
    //         placedObject.transform.rotation,
    //         Physics.DefaultRaycastLayers,
    //         QueryTriggerInteraction.Collide
    //     );

    //     bool scored = false;

    //     // 檢查是否進入 GoodZone
    //     foreach (Collider col in overlappingColliders)
    //     {
    //         if (col.CompareTag("GoodZone"))
    //         {
    //             Debug.Log($"{placedObject.name} placed in Good Zone! Points: +{pointsForGoodZone}");
    //             ScoreManager.Instance.ModifyScore(pointsForGoodZone);
    //             scored = true;
    //             break;
    //         }
    //     }

    //     // 如果沒得分，檢查是否進入 BadZone
    //     if (!scored)
    //     {
    //         foreach (Collider col in overlappingColliders)
    //         {
    //             if (col.CompareTag("BadZone"))
    //             {
    //                 Debug.Log($"{placedObject.name} placed in Bad Zone! Points: {pointsForBadZone}");
    //                 ScoreManager.Instance.ModifyScore(pointsForBadZone);
    //                 scored = true;
    //                 break;
    //             }
    //         }
    //     }

    //     // 如果仍未得分，顯示沒有放置在指定區域
    //     if (!scored)
    //     {
    //         Debug.Log($"{placedObject.name} dropped outside designated zones. No score change.");
    //     }
    // }
}