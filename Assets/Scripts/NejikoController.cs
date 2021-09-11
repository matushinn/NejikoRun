using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;

    CharacterController controller;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;

    [SerializeField] float gravity;
    [SerializeField] float speedZ;
    [SerializeField] float speedX; //横方向スピードのパラメータ
    [SerializeField] float speedJump;
    [SerializeField] float accelerationZ; //前進加速度のパラメータ

    // Start is called before the first frame update
    void Start()
    {
        //必要なコンポーネントを自動取得
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        //地上にいる場合のみ操作を行う
        if (controller.isGrounded) //接地しているかの判定
        {
            //Inputを検知して前に進める
            if  (Input.GetAxis("Vertical") > 0.0f)
            {
                //前進ペロシティの設定
                moveDirection.z = Input.GetAxis("Vertical") * speedZ;
            }
            else
            {
                moveDirection.z = 0;
            }

            //方向転換
            transform.Rotate(0, Input.GetAxis("Horizontal") * 3, 0);

            //ジャンプ
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = speedJump;
                animator.SetTrigger("jump");
            }
        }*/

        //デバック用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        //徐々に加速しZ方向に常に前進させる
        float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
        moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

        //X方向は目標のポジションまでの差分の割合で速度を計算
        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;


        //重力分の力をマイフレーム追加
        moveDirection.y -= gravity * Time.deltaTime; //重力の加算

        //キャラクターの移動実行
        Vector3 globalDirrection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirrection * Time.deltaTime);

        //移動後接地していたらY方向の速度はリセットする
        if (controller.isGrounded) moveDirection.y = 0;

        //速度が０以上なら走っているフラグをtrueにする
        animator.SetBool("run", moveDirection.z > 0.0f);

    }

    //左のレーンに移動を開始
    public void MoveToLeft()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--; //目標レーンの変更
    }

    //右のレーンに移動を開始
    public void MoveToRight()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane++; //目標レーンの変更
    }

    //ジャンプ関数
    public void Jump()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = speedJump;

            //ジャンプトリガー設定
            animator.SetTrigger("jump");
        }
    }

}
