using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField]
    PlayerExpUI m_expUI;

    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();

    Animator animator;

    [SerializeField]
    Rigidbody2D m_rigid;


    [SerializeField]
    Bullet2D m_prefabBullet;

    [SerializeField]
    Spawner spawner;


    [SerializeField]
    public int m_hp = 100;

    [SerializeField]
    public int m_maxHP = 100;

    [SerializeField]
    GameOverController gameOverController;


    [SerializeField]
    int m_exp = 0;

    [SerializeField]
    int m_level = 1;

    [SerializeField]
    int m_expToNextLevel = 100;



    //
    public enum WeaponType
    {
        Sword,
        Gun
    }
    [SerializeField]
    WeaponType currWeapon = WeaponType.Sword;
    /// <summary>
    /// 
    /// </summary>
    /// 
    [SerializeField]
    float fireRate = 0.2f; // 초당 발사 간격

    float fireTimer = 0f;


    [SerializeField]
    public UnityEvent<int, int> PlayerChangedHpEvent = new UnityEvent<int, int>();

    [SerializeField]
    Player m_player;

    private void Start()
    {
        animator = GetComponent<Animator>();
        m_rigid = GetComponent<Rigidbody2D>();

      

        if (PlayerChangedHpEvent != null)
        {
            PlayerChangedHpEvent.Invoke(m_hp, m_maxHP);
        }
    }






private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        UpdateState();


        fireTimer += Time.deltaTime;




        if( Input.GetMouseButton(0)) // 누르고 있는 동안
    {
            if (fireTimer >= fireRate)
            {
                fireTimer = 0f;

                Vector3 pos = transform.position;
                Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos.z = 0;

                Vector3 dir = targetPos - pos;

                Bullet2D obj = Instantiate(m_prefabBullet);
                obj.transform.position = pos;
                obj.Fire(dir.normalized * 5f);
            }
        }

        /*
        if (Input.GetMouseButtonDown(0)) //var ==  vector3 총
        {
            Vector3 pos = transform.position;
            Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPos.z = 0;

            Vector3 dir = targetPos - pos;

            Bullet2D obj = Instantiate(m_prefabBullet);
            obj.transform.position = pos;
            obj.Fire(dir.normalized * 5f);

        }

            */

    }
    private void FixedUpdate()
    {
        MoveCharacter();

    }

    private void MoveCharacter()
    {

        m_rigid.velocity = movement * movementSpeed;
    }
    private void UpdateState()
    {
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isMove", false);

        }
        else
        {
            animator.SetBool("isMove", true);
        }
        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);


    }

    private void OnDrawGizmos()
    {
        Vector3 pos = transform.position;
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPos.z = 0;

        Vector3 dir = targetPos - pos;

        Debug.DrawLine(pos, pos + dir, Color.blue);
        Debug.DrawLine(pos, pos + dir.normalized, Color.green);

    }

  

    [SerializeField]
    Animator m_anim;

    [SerializeField]
    UnityEvent m_clickEvent;

    [SerializeField]
    MoveToTarget m_enemy;

    public void p_TakeDamage(int damage)
    {
        m_hp -= damage;
        m_anim.SetTrigger("Hurt");
  //      PlayerChangedHpEvent.Invoke(m_hp, m_maxHP);


        if (m_hp <= 0)
        {

            m_anim.SetTrigger("Death");
            //killed ontriggerenter - destory
            gameOverController.GameOver(); // 죽었ㅇ르 때이벤트 발생
            Destroy(gameObject, 1f);

        }
        PlayerChangedHpEvent.Invoke(m_hp, m_maxHP);
    }

    public void ChangeWeapon()
    {
        if (currWeapon == WeaponType.Sword)
            currWeapon = WeaponType.Gun;
        else
            currWeapon = WeaponType.Sword;
    }

    public void AddExp(int exp)
    {
        m_exp += exp;
        Debug.Log($"경험치 +{exp}, 현재: {m_exp} / {m_expToNextLevel}");

        while (m_exp >= m_expToNextLevel)
        {
            m_exp -= m_expToNextLevel;
            m_level++;


            m_expToNextLevel = Mathf.FloorToInt(100 * Mathf.Pow(1.1f, m_level - 1)); Debug.Log($"레벨업! 현재 레벨: {m_level}");


        }
        if (m_expUI != null)
            m_expUI.SetExp(m_exp, m_expToNextLevel, m_level);

    }
}
