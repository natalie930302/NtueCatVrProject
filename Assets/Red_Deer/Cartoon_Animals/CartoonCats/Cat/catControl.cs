using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class catControl : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //水平軸
        float h = Input.GetAxis("Horizontal");
        //垂直軸
        float v = Input.GetAxis("Vertical");
        //向量
        Vector3 move = new Vector3(h, 0, v);
        //當按下鍵盤上下左右時
        if (move != Vector3.zero)
        {
            //移動
            transform.position += move * Time.deltaTime * 2;
            //旋轉
            transform.rotation = Quaternion.LookRotation(move);
            //播放動畫
            animator.SetBool("IsRun", true);
        }
        else
        {
            //停止播放動畫
            animator.SetBool("IsRun", false);
        }
    }
}
