using SWS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : FSM_System
{
    public int hp = 20;
    public int max_hp;
    public int damage = 5;

    public float range_Detect;
    public float range_Attack;
    public float attack_speed;
    public float time_count_attack = 0;
    protected int critical;
    public Transform trans;
    public CharacterControl characterControl;
    public NavMeshAgent agent_;
    public bool isMoveAgent;
    public float time_delay_agent = 0;
    public splineMove splineMove_;
    protected Bulletdata bulletdata;
    public Transform anchor_Hub;
    private EnemyHub enemyhub;
    private EnemyIndicator enemyIndicator;
    public bool isAlive = true;
    public GameObject enemy_detect;
    public Bulletdata cur_bullet_data;
    private void Awake()
    {
        trans = transform;
        GameObject char_object = GameObject.FindGameObjectWithTag("Player");
        characterControl = char_object.GetComponent<CharacterControl>();

    }
    public virtual void Setup(ConfigEnemyRecord configEnemy)
    {
        hp = configEnemy.HP;
        max_hp = hp;
        damage = configEnemy.Damage;
        int index = 0;
        trans.position = SceneConfig.instance.GetPointSpawn(out index).position;
        int min = index * 3 + 1;
        int max = min + 3;
        string path = "Path_" + UnityEngine.Random.Range(min, max);

        splineMove_.pathContainer = WaypointManager.Paths[path];
        // create Hub
        enemyhub = BYPoolManager.instance.dic_pool["Enemyhub"].Spawned().GetComponent<EnemyHub>();
        enemyhub.Init(IngameUI.instance.parent_Hub, anchor_Hub);

        enemyIndicator = BYPoolManager.instance.dic_pool["EnemyIndicator"].Spawned().GetComponent<EnemyIndicator>();
        enemyIndicator.Init(IngameUI.instance.parent_indicator, anchor_Hub);
    }
    public void OnDead()
    {
        isAlive = false;
        Destroy(enemy_detect);
        BYPoolManager.instance.dic_pool["Enemyhub"].DeSpawned(enemyhub.transform);
        if (enemyIndicator != null)
        {
            BYPoolManager.instance.dic_pool["EnemyIndicator"].DeSpawned(enemyIndicator.transform);
            enemyIndicator = null;
        }
        MissionManager.instance.EnemyOnDead(this, cur_bullet_data);
        Invoke("DelayDestroy", 3);
    }
    private void DelayDestroy()
    {
        Destroy(gameObject);
    }
    public override void Update()
    {
        base.Update();
        time_count_attack += Time.deltaTime;

        Vector3 pos_ch = characterControl.trans.position;
        pos_ch.y = trans.position.y;
        float dis = Vector3.Distance(pos_ch, trans.position);
        if (enemy_detect != null)
            enemy_detect.SetActive(dis <= 10);
        if (enemyIndicator != null)
            enemyIndicator.gameObject.SetActive(dis > 10);

    }

    public virtual void OnDamage(Bulletdata bulletdata)
    {
        cur_bullet_data = bulletdata;
        enemyhub.UpdateDamage(hp, max_hp);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range_Detect);
    }

}