using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rbody;
    float axisH = 0.0f;
    public float speed = 3.0f; //속도 상수값
    public float jump = 9.0f; //점프 상수값
    public LayerMask groundLayer;
    bool goJump = false;
    bool onGround = false;
    // Start is called before the first frame update
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        axisH = Input.GetAxisRaw("Horizontal"); //GetAxis와 GetAxisRaw의 차이점 : getAxis는 -1~1사이의 값이 연속적으로 변하지만 getAxisRaw는 -1,0,1 3가지로만 값을 받는다
        if (axisH > 0.0f)
        {
            Debug.Log("오른쪽 회전");
            transform.localScale = new Vector2(1, 1); //locaScale값으로 스프라이트 방향 변경
        }
        else if (axisH < 0.0f)
        {
            Debug.Log("왼쪽 회전");
            transform.localScale = new Vector2(-1, 1); //locaScale값으로 스프라이트 방향 변경
        }
        if (Input.GetButtonDown("Jump")) 
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        onGround = Physics2D.Linecast(transform.position,transform.position - (transform.up * 0.1f),groundLayer); 
        //Linecast(시작점,끝점,체크할 레이어) : 시작점과 끝점 사이를 잇는 선을 그리고 그 선에 체크할 레이어가 닿았으면 true, 아니면 false
        if(onGround || axisH != 0) //axisH != 0 얘만 있어도 됨(axisH가 0아닐땐 아래의 조건문의 실행되서 결론적으로 차이가 없음)
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);

        }
        if (onGround && goJump) // 지면에 닿았고 점프 메서드가 실행될때
        {
            Debug.Log("jump");
            Vector2 jumpPw = new Vector2(0, jump); 
            rbody.AddForce(jumpPw, ForceMode2D.Impulse); 
            //ForceMode2D.Impulse는 작은힘으로도 크게 가능하다(물리적으로 보면 이론상 0의 시간동안 무한대의 힘을 주는게 Impulse다)
            //자동차의 엔진이나 모터등을 정지해있던 상태에서 처음 구동시킬때랑 비슷한 예이다(캐피시터) 
            goJump = false; //2단 점프 방지
        }

    }
    public void Jump()
    {
        goJump = true;
        Debug.Log("button down");
    }

}
