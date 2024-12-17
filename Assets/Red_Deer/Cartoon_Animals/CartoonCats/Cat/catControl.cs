using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CatControl : MonoBehaviour
{
    private InputActionAsset asset;        // 用於儲存 InputAction 資料的變數
    private InputActionMap m_cat;          // 動作映射
    private InputAction m_cat_move;        // 動作: move

    // 初始化 InputActionAsset
    private void Awake()
    {
        asset = InputActionAsset.FromJson(@"{
            ""name"": ""catControl"",
            ""maps"": [
                {
                    ""name"": ""cat"",
                    ""id"": ""93b34cef-69a1-4c7b-a624-cf70e74528f8"",
                    ""actions"": [
                        {
                            ""name"": ""move"",
                            ""type"": ""Value"",
                            ""id"": ""71ff49db-fa4f-45b0-8456-f4a12bf06ca6"",
                            ""expectedControlType"": ""Vector2"",
                            ""processors"": """",
                            ""interactions"": """"
                        }
                    ],
                    ""bindings"": [
                        {
                            ""name"": ""2D Vector"",
                            ""id"": ""54e05d5c-22b6-46de-b0f2-efbcbf16761a"",
                            ""path"": ""2DVector"",
                            ""action"": ""move"",
                            ""isComposite"": true
                        },
                        {
                            ""name"": ""up"",
                            ""id"": ""5d2ee4df-e8a0-43b3-a857-78f9259a0882"",
                            ""path"": ""<Keyboard>/upArrow"",
                            ""action"": ""move"",
                            ""isComposite"": false,
                            ""isPartOfComposite"": true
                        },
                        {
                            ""name"": ""down"",
                            ""id"": ""7c314517-1b42-4865-baa5-3ef74afa04cc"",
                            ""path"": ""<Keyboard>/downArrow"",
                            ""action"": ""move"",
                            ""isComposite"": false,
                            ""isPartOfComposite"": true
                        },
                        {
                            ""name"": ""left"",
                            ""id"": ""6d9a52f1-2425-4d55-8d31-61c3dd968526"",
                            ""path"": ""<Keyboard>/leftArrow"",
                            ""action"": ""move"",
                            ""isComposite"": false,
                            ""isPartOfComposite"": true
                        },
                        {
                            ""name"": ""right"",
                            ""id"": ""fed19c1a-4850-43a8-bd9e-cdfeb59eff80"",
                            ""path"": ""<Keyboard>/rightArrow"",
                            ""action"": ""move"",
                            ""isComposite"": false,
                            ""isPartOfComposite"": true
                        }
                    ]
                }
            ],
            ""controlSchemes"": []
        }");

        // 初始化動作映射和動作
        m_cat = asset.FindActionMap("cat", throwIfNotFound: true);
        m_cat_move = m_cat.FindAction("move", throwIfNotFound: true);
    }

    private void OnEnable()
    {
        m_cat.Enable();  // 啟用 InputActionMap
    }

    private void OnDisable()
    {
        m_cat.Disable(); // 禁用 InputActionMap
    }

    void Update()
    {
        // 讀取 Move 動作的值
        Vector2 moveInput = m_cat_move.ReadValue<Vector2>();
        Debug.Log("Move Input: " + moveInput);

        // 範例邏輯：根據輸入移動物件
        transform.Translate(new Vector3(moveInput.x, 0, moveInput.y) * Time.deltaTime);
    }
}
