using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bulletdata
{
    public string name_pool;
    public int damage = 0;
    public Rigidbody rig_body;
    public Vector3 force;
    public Vector3 point_impact;
    public BodyType bodyType;
    public ConfigWeaponRecord cf_wp;

}
public class BulletControl : MonoBehaviour
{
    private Transform trans;
    private Transform impact_pf;
    private Bulletdata data;
    public LayerMask mask;
    // Start is called before the first frame update
    void Awake()
    {
        trans = transform;
    }
    IEnumerator OnEndLife()
    {
        yield return new WaitForSeconds(3);
        BYPoolManager.instance.dic_pool[data.name_pool].DeSpawned(transform);
    }
    public void Spawned()
    {
        StopCoroutine("OnEndLife");
        StartCoroutine("OnEndLife");
    }
    public void DeSpawned()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        trans.Translate(Vector3.forward * Time.deltaTime * 20);
        if(Physics.Raycast(trans.position,trans.forward,out RaycastHit hitInfo,1, mask))
        {
            Transform impact = BYPoolManager.instance.dic_pool["Impact"].Spawned();
            impact.position = hitInfo.point;
            impact.forward = hitInfo.normal;
            
            BYPoolManager.instance.dic_pool[data.name_pool].DeSpawned(trans);

            data.point_impact = hitInfo.point;
            EnemyOnDamage enemyOnDamage = hitInfo.collider.GetComponent<EnemyOnDamage>();

            if (enemyOnDamage!=null)
            {
                enemyOnDamage.OnDamage(data);
            }

        }
    }
    public void Setup(Bulletdata data)
    {
        this.data = data;
        
    }

    private void OnBecameInvisible()
    {
        
        BYPoolManager.instance.dic_pool[data.name_pool].DeSpawned(transform);
    }

}
