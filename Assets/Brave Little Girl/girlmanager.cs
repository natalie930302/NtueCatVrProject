using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class girlmanager : MonoBehaviour
{
    private Animator girlAnimator;  // 貓的 Animator 組件
    private Transform girlRoot;     // 貓的 Transform（移動的根物件）
    private GirlControl controls;   // 輸入系統
    private Vector2 moveInput;     // 儲存輸入值

    [Header("移動參數")]
    [SerializeField] private float moveSpeed = 3f; // 移動速度

    private void Awake()
    {
        // 自動抓取組件
        girlAnimator = GetComponent<Animator>();
        girlRoot = transform;

        // 初始化輸入系統
        controls = new GirlControl();

        controls.girl.move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.girl.move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void Update()
    {
        bool isWalking = moveInput != Vector2.zero;
        // ttest console
        Debug.Log("isWalking: " + isWalking);

        // 移動物件
        if (isWalking)
        {
            Movegirl();
        }

        // 同步播放跑步動畫
        girlAnimator.SetBool("IsWalk", isWalking);
    }

    private void Movegirl()
    {
        // 移動方向
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y).normalized;

        // 旋轉貓物件朝向移動方向
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            girlRoot.rotation = Quaternion.RotateTowards(girlRoot.rotation, toRotation, moveSpeed * 100 * Time.deltaTime);
        }

        // 移動貓物件
        girlRoot.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
    }
}