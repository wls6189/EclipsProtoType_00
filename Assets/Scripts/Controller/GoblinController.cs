using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinController : MonoBehaviour
{
    //CapsuleCollider2D collider2d;
    SpriteRenderer spr;
    Rigidbody2D rigid;
    Animator goblinAnim;
    UIManager cameraShake;
    void Start()
    {
        //collider2d = GetComponent<CapsuleCollider2D>();
        spr = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        goblinAnim = GetComponent<Animator>();

        cameraShake = GameObject.FindWithTag("MainCamera").GetComponent<UIManager>();
        target = GameObject.Find("Player");
    }

    void Update()
    {
       
    }
    
    //1. 몬스터 체력,피격 관련 변수==================
    public int Maxhp;
    public int curhp;
    private GameObject target;
    int dirc_x;
    //==============================================
    public void OnDamaged() 
    {
        //targetPos: 적이 오브젝트로 부터 충돌한 위치 
        curhp--;
        spr.color = new Color(1, 0, 0, 1);// 몬스터 빨간 색으로 변경

        if (transform.position.x > target.transform.position.x) //2. ================플레이어 좌표 값을 이용하여 피격구현 코드========================
        {
            dirc_x = 1;
        }
        else
        {
            dirc_x = -1;
        }
        rigid.AddForce(new Vector2(dirc_x, 0) * 3.0f, ForceMode2D.Impulse);
        //============================================================================================================================================
        Invoke("OffDamaged", 0.5f); //0.5초 뒤 몬스터 원래 색으로 변경      
        if (curhp <= 0)
        {
            OnDie();
        }
    }

    public void CriticalDamaged()
    {
        curhp -= 2;
        spr.color = new Color(1, 0, 0, 1);
        if (transform.position.x > target.transform.position.x) //2. ================플레이어 좌표 값을 이용하여 피격구현 코드========================
        {
            dirc_x = 1;
        }
        else
        {
            dirc_x = -1;
        }
        rigid.AddForce(new Vector2(dirc_x, 0) * 3.0f, ForceMode2D.Impulse);
        //============================================================================================================================================
        Invoke("OffDamaged", 0.5f); //0.5초 뒤 몬스터 원래 색으로 변경
        if (curhp <= 0)
        {
            OnDie();
        }
    }

    void OffDamaged()
    {
        spr.color = new Color(1, 1, 1, 1);
    }


    public GameObject soulobj;
    void OnDie()
    {
        //AudioManager.instance.PlaySfx(AudioManager.Sfx.bear_Die);
        //ScoreUI.enemyScore -= 1;
        Debug.Log("죽음");
        Instantiate(soulobj, transform.position, transform.rotation);
        goblinAnim.SetBool ("DeadGoblin",true);
    }

    private void OnDestroy()
    {
        Debug.Log("DESTROY");
        Destroy(gameObject);
    }
}
