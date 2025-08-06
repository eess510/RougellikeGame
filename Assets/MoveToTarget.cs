using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D m_rigid;

    [SerializeField]
    Transform m_target;

    [SerializeField]
    Vector2 m_velocity = Vector2.zero;

    [SerializeField]
    float m_speed = 2f;
    //

    [SerializeField]
    private Animator m_anim;

    [SerializeField]
    int m_hp = 10;

    [SerializeField]
    int m_maxHP = 10;

    [SerializeField]
    public UnityEvent<MoveToTarget> KilledEvent;

    [SerializeField]
    public UnityEvent<int, int> ChangedHPEvent;

    [SerializeField]
    Player m_player;

    [SerializeField]
    int m_enemyReward = 10;

    //
    [field: SerializeField]
    public int Level { get; private set; } = 0;
    //
    public UnityEvent OnEnemyDied = new UnityEvent();

    //
    public bool IsDead { get; private set; } = false;


    [ContextMenu(nameof(Attack))]
    void Attack()
    {
        m_rigid.velocity = -transform.right * m_speed; //왼쪽방향
    }

    //
    [ContextMenu(nameof(TakeDamage))]

    void TakeDamage()
    {
        TakeDamage(3);
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"TakeDamage called with {damage}");
        print("데미지");
        m_hp -= damage;
        m_anim.SetTrigger("Hit");
        ChangedHPEvent.Invoke(m_hp, m_maxHP);



        if (m_hp <= 0)
        {
            m_hp = 0;
            IsDead = true;
            m_anim.SetTrigger("Death");
            //killed ontriggerenter - destory
            if (m_player != null)
            {
                int exp = m_enemyReward * (Level + 1); // 예: 레벨 0이면 10, 1이면 20
                m_player.AddExp(exp);
            }


            OnEnemyDied.Invoke(); // 죽었ㅇ르 때이벤트 발생
            Destroy(gameObject, 1f);

        }
    }


    public void Attack(Vector3 v)
    {
        m_rigid.velocity = v;
        transform.right = v;
    }

    private void Update()
    {
        if (m_target == null)
            return;

        var dir = m_target.position - transform.position; // 방향벡터 구하기
        m_velocity = dir.normalized * m_speed;

    }
    private void FixedUpdate()
    {
        m_rigid.velocity = m_velocity;
    }
    public Transform Target { get => m_target; set => m_target = value; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"OnCollisionEnter2D:{collision.gameObject.name}");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogError($"OnTriggerEnter2D:{collision.gameObject.name}");

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //플레이어 충돌 : 플레이어 데미지
            m_player.p_TakeDamage(10);
               // Destroy(gameObject);
               //bombfx 효과 넣어주기
            
        }
    }
    

    public void Start()
    {
        if (m_player == null)
        {
            m_player = FindFirstObjectByType<Player>();
        }

        ChangedHPEvent.Invoke(m_hp, m_maxHP);
    }

    public int GetExpReward() => m_enemyReward;


}
