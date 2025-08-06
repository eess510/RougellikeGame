using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{

    [SerializeField]
    MoveToTarget[] m_enemyPrefabs;

    [SerializeField]
    Transform m_target; //���� �߰��� Ÿ�� �̸� ����

    //[SerializeField]
    //  private  EnemyHPUI m_enemyHPUI; ���� �ִ� ���� HPUI
   

    


    [SerializeField]
    float m_spawnTime =3f;

    int currLevel = 0;

    //
    int minE = 2;
    int maxE = 7;

    int aliveEnemies = 0;



     void Start()
    {
       
        SpawnSingleEnemy(currLevel);
    }
    //

    void SpawnSingleEnemy(int level) // �� level �� 1����
    {

        if (level >= m_enemyPrefabs.Length)
        {
            Debug.LogWarning("SpawnSingleEnemy: �߸��� ���� ���� �õ�");
            return;
        }
       
        if (currLevel >= m_enemyPrefabs.Length || m_enemyPrefabs[currLevel] == null)
        {
            Debug.LogWarning("�� ���� ��û: �������� ���ų� ���� ���� �ʰ�");
            return;
        }

        var enemy = Instantiate(m_enemyPrefabs[level]);
        //
       

        enemy.transform.position = new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), 0);
        enemy.Target = m_target;


        EnemyHPUI m_enemyHPUI = enemy.GetComponentInChildren<EnemyHPUI>();
        if (m_enemyHPUI != null)
        {
            m_enemyHPUI.OnSpawnEnemy(enemy);
        }


        m_enemyHPUI.OnSpawnEnemy(enemy);
        bool spawnedExtras = false; //

        enemy.ChangedHPEvent.AddListener((hp, maxHp) =>
        {
            if (hp <= 0 && !spawnedExtras)
            {


                     spawnedExtras = true;

                    int extraCount = Random.Range(minE, maxE);
                    aliveEnemies = extraCount;

                    StartCoroutine(SpawnExtraEnemies(level, extraCount));
                
            }

        }); }



    IEnumerator SpawnExtraEnemies(int level, int count)
    {
        for (int i = 0; i < count; i++)
        {
            //
            yield return new WaitForSeconds(m_spawnTime);

            var enemy = Instantiate(m_enemyPrefabs[level]);
            enemy.transform.position = new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 4f), 0);
            enemy.Target = m_target;
            EnemyHPUI m_enemyHPUI = enemy.GetComponentInChildren<EnemyHPUI>();
            if (m_enemyHPUI != null)
            {
                m_enemyHPUI.OnSpawnEnemy(enemy);
            }


            m_enemyHPUI.OnSpawnEnemy(enemy);

            bool countedDead = false;

            enemy.ChangedHPEvent.AddListener((hp, maxHp) =>
            {
                if (hp <= 0 && !countedDead)
                {
                    countedDead = true;
                    aliveEnemies--;

                    if (aliveEnemies <= 0  )
                    {
                        currLevel++;


                        if (currLevel >= m_enemyPrefabs.Length)
                        {
                            Debug.Log("���� �Ϸ�. ���� ������ �̵�");
                            SceneManager.LoadScene("sc03");
                        }
                        else
                        {
                            SpawnSingleEnemy(currLevel);
                        }
                    }
                }
            });
        } 

    }
}
   