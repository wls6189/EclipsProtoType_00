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
    
    //1. ���� ü��,�ǰ� ���� ����==================
    public int Maxhp;
    public int curhp;
    private GameObject target;
    int dirc_x;
    //==============================================
    public void OnDamaged() 
    {
        //targetPos: ���� ������Ʈ�� ���� �浹�� ��ġ 
        curhp--;
        spr.color = new Color(1, 0, 0, 1);// ���� ���� ������ ����

        if (transform.position.x > target.transform.position.x) //2. ================�÷��̾� ��ǥ ���� �̿��Ͽ� �ǰݱ��� �ڵ�========================
        {
            dirc_x = 1;
        }
        else
        {
            dirc_x = -1;
        }
        rigid.AddForce(new Vector2(dirc_x, 0) * 3.0f, ForceMode2D.Impulse);
        //============================================================================================================================================
        Invoke("OffDamaged", 0.5f); //0.5�� �� ���� ���� ������ ����      
        if (curhp <= 0)
        {
            OnDie();
        }
    }

    public void CriticalDamaged()
    {
        curhp -= 2;
        spr.color = new Color(1, 0, 0, 1);
        if (transform.position.x > target.transform.position.x) //2. ================�÷��̾� ��ǥ ���� �̿��Ͽ� �ǰݱ��� �ڵ�========================
        {
            dirc_x = 1;
        }
        else
        {
            dirc_x = -1;
        }
        rigid.AddForce(new Vector2(dirc_x, 0) * 3.0f, ForceMode2D.Impulse);
        //============================================================================================================================================
        Invoke("OffDamaged", 0.5f); //0.5�� �� ���� ���� ������ ����
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
        Debug.Log("����");
        Instantiate(soulobj, transform.position, transform.rotation);
        goblinAnim.SetBool ("DeadGoblin",true);
    }

    private void OnDestroy()
    {
        Debug.Log("DESTROY");
        Destroy(gameObject);
    }
}
