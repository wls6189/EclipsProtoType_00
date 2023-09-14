using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animSyals;
    CameraController  cameraShake;
     CapsuleCollider2D colliderPlayer;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animSyals = GetComponent<Animator>();
        colliderPlayer = GetComponent<CapsuleCollider2D>();
        cameraShake = Camera.main.GetComponent<CameraController  >();
    }



    public float curSpeed; //���� �������� 2���� �����Ͽ���.
    public float jumpForce = 8f;
    private bool isGrounded; //���� ���� bool���� 
    void Update()
    {
        
        PlayerMove(); 
        PlayerJump();
        PlayerAttack();
        SkillAttack();
        


        if (atkcount == 2)
        {
           // iscrit = true;
            critTime += Time.deltaTime;
            if(critTime < 3.0f && Input.GetKey(KeyCode.X))
            {
                iscrit = true;
            }
            else if(critTime > 3.0f)
            {
                iscrit = false;
                critTime = 0.0f;
            }
            
        }
    }

    public bool isRun = false; //�޸��� �ִ��� ������ Ȯ���ϴ� ���� 
    void PlayerMove()
    {

        ////AddForce�� ����Ͽ��� ������ ���� ���� ���� ���� �� �ӵ��� �������Ƿ� �̵� �ӵ� �����ϱ�
        //if (rigid.velocity.x > maxSpeed) // maxSpeed = 2
        //{
        //    rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        //}
        //else if (rigid.velocity.x < maxSpeed * (-1))
        //{
        //    rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        //}

        float horizontalInput = Input.GetAxis("Horizontal");
       
        if(horizontalInput > 0)
        {
            Vector2 moveDirection = new Vector2(horizontalInput, 0);
            rigid.velocity = new Vector2(moveDirection.x * curSpeed, rigid.velocity.y);
            //transform.localScale = new Vector3(1, 1, 1);
            transform.eulerAngles = new Vector3(0, 0, 0);
            animSyals.SetFloat("positionX", moveDirection.x);
            isRun = true;
        }
        else if(horizontalInput < 0)
        {
            Vector2 moveDirection = new Vector2(-horizontalInput, 0);
            rigid.velocity = new Vector2(-moveDirection.x * curSpeed, rigid.velocity.y);
            //transform.localScale = new Vector3(-1, 1, 1);
            transform.eulerAngles = new Vector3(0, 180, 0);
            animSyals.SetFloat("positionX", moveDirection.x);
            isRun = true;
        }
        else
        {
            isRun = false;
        }
    }
    void PlayerJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
           // rigid.velocity = new Vector2(rigid.velocity.x, jumpForce);
             rigid.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }



    private int playerMaxHp = 3;
    public int playerCurHp = 3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //2.���� ���� �ڵ�� ���� ���� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animSyals.SetBool("IsGround", false);
        }

        if (collision.gameObject.tag == "Enemy")
        {       
            if(playerCurHp > 0)
            {
                playerCurHp--;
                FindObjectOfType<LifeCount>().LoseLife();
            }
            if (playerCurHp <= 0)
            {
                FindObjectOfType<LevelManager>().Restart();
            }


        }

      
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //3.���� ���� �ڵ�� ���� ���� ����
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            animSyals.SetBool("IsGround", true);
        }

    }


    public Transform pos;
    public Vector2 boxSize;
    private float curTime;
    public float coolTime = 0.8f;
    public int atkcount = 0;

    public bool isatk;
    private bool iscrit;
    public float critTime;
    void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !animSyals.GetCurrentAnimatorStateInfo(0).IsName("PlayerBaseAttackAnim"))
        {
            
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
            
            animSyals.SetTrigger("BaseAtk");
            //animSyals.SetTrigger("CriticalAtk");         
            
            Debug.Log("�Ϲ� ����");
            
            
            
            foreach (Collider2D collider in collider2Ds)
            {         
               
                if (collider.name == "goblin")
                {
                    isatk = true;
                    atkcount++;
                    Debug.Log(atkcount);
                    collider.GetComponent<GoblinController>().OnDamaged();
                    BaseAtkEff(collider);
                    cameraShake.ShakeCamera();

                    //cameraShake.VibrateForTime(0.5f);//��鸮�� �ð� 0.5�� �ѱ�(UI�Ŵ��� ��ũ��Ʈ ����)
                    //BaseAtkOn(collider);

                    //if(critTime <5.0f)
                    //{
                    //     collider.GetComponent<GoblinController>().CriticalDamaged();
                    //     animSyals.SetTrigger("CriticalAtk");
                    //     CritAtkEff(collider);
                    //     cameraShake.VibrateForTime(0.7f);
                    //     cameraShake.ShakeAmount = 0.3f;
                    //}

                    if (iscrit == true)
                    {
                        collider.GetComponent<GoblinController>().CriticalDamaged();
                        CritAtkEff(collider);

                       
                        //cameraShake.VibrateForTime(0.7f);
                        //cameraShake.ShakeAmount = 0.3f;
                        animSyals.SetTrigger("CriticalAtk");
                    }
                }
                
            }
           
             curTime = coolTime;
            //AudioManager.instance.PlaySfx(AudioManager.Sfx.Player_Attack);
        }
        else
        {
            curTime -= Time.deltaTime;
            isatk = false;
        }
        //critTime = 0.0f;
        
    }

    //void CritAttack()
    //{
    //    if(iscrit == true)
    //    {
    //        Debug.Log("��ų����");
    //        //collider.GetComponent<GoblinController>().CriticalDamaged();
    //        animSyals.SetTrigger("CriticalAtk");
    //       // CritAtkEff(collider);
    //       // cameraShake.VibrateForTime(0.7f);
    //       // cameraShake.ShakeAmount = 0.3f;
    //    }

    //}

    public GameObject FireBallPrefab;
    private float curtimeFireBall;
    public float coolTimeFireBall;
    public Transform firballpos;

    void SkillAttack()
    {
        
       // iscrit = true;
        if (curtimeFireBall <= 0)
        {
            if (Input.GetKey(KeyCode.C) && isRun == false)
            {
                //Instantiate(����������Ʈ,������ġ,ȸ��);         
                animSyals.SetTrigger("SkillAtk");
                curtimeFireBall = coolTimeFireBall;
                isatk = true;
            }
            
        }
        curtimeFireBall -= Time.deltaTime;
        
    }

   public void fireballINS()
    {
        Instantiate(FireBallPrefab, firballpos.position, transform.rotation);
    }

    public GameObject BaseattackEffectPrefab; // ����Ʈ �������� ������ ����
    public GameObject CritattackEffectPrefab; // ����Ʈ �������� ������ ����
    void BaseAtkEff(Collider2D collider)
    {
        // ����Ʈ�� �����ϰ� ��ġ�� �÷��̾� ��ġ�� ����
        GameObject attackEffect = Instantiate(BaseattackEffectPrefab, collider.transform.position, Quaternion.identity);
        Debug.Log("�⺻����Ʈ");
        // ����Ʈ�� ��� �� �ڵ����� �ı��ǵ��� ���� (���� ���, ��ƼŬ �ý����� ��� �ð��� ����)
        Destroy(attackEffect, attackEffect.GetComponent<ParticleSystem>().main.duration);
    }
    
    void CritAtkEff(Collider2D collider)
    {
        // ����Ʈ�� �����ϰ� ��ġ�� �÷��̾� ��ġ�� ����
        GameObject CritattackEffect = Instantiate(CritattackEffectPrefab, collider.transform.position, Quaternion.identity);
        Debug.Log("��ų����Ʈ");
        // ����Ʈ�� ��� �� �ڵ����� �ı��ǵ��� ���� (���� ���, ��ƼŬ �ý����� ��� �ð��� ����)
        Destroy(CritattackEffect, CritattackEffect.GetComponent<ParticleSystem>().main.duration);
    }

    

   

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);//DrawWireCube(pos.position,boxsize)
    }
}
