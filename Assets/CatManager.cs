using UnityEngine;
using UnityEngine.InputSystem;

public class CatInputManager : MonoBehaviour
{
    private Animator catAnimator;  // 貓的 Animator 組件
    private Transform catRoot;     // 貓的 Transform（移動的根物件）
    private InputManager input;   // 輸入系統
    private Vector2 moveInput;     // 儲存輸入值

    [Header("移動參數")]
    [SerializeField] private float moveSpeed = 3f; // 移動速度

    private void Awake()
    {
        // 自動抓取組件
        catAnimator = GetComponent<Animator>();
        catRoot = transform;

        // 初始化輸入系統
        input = new InputManager();

        input.cat.move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        input.cat.move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();

    private void Update()
    {
        bool isRunning = moveInput != Vector2.zero;

        // 移動物件
        if (isRunning)
        {
            MoveCat();
        }

        // 同步播放跑步動畫
        catAnimator.SetBool("IsRun", isRunning);
    }

    private void MoveCat()
    {
        // 移動方向
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        // 旋轉貓物件朝向移動方向
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

            // 增加一個新的旋轉速度參數
            float rotationSpeed = 500f; // 調整這個值來控制旋轉速度
            catRoot.rotation = Quaternion.RotateTowards(catRoot.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // 移動貓物件
        catRoot.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }

}