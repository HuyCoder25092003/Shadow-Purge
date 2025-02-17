using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    private RectTransform parent_rect;
    private Transform anchor;
    public RectTransform rect;
    private Transform char_trans;
    private void Awake()
    {
        rect = gameObject.GetComponent<RectTransform>();
        char_trans = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public void Init(RectTransform parent, Transform anchor)
    {
        this.parent_rect = parent;
        transform.SetParent(parent, true);
        this.anchor = anchor;
        rect.anchoredPosition = Vector2.zero;

    }
    void Update()
    {
        if(anchor!=null)
        {
            Vector3 pos_e = anchor.position;
            pos_e.y = char_trans.position.y;
            Vector3 dir = pos_e - char_trans.position;
            dir.Normalize();
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            rect.localEulerAngles = new Vector3(0, 0, -angle);
        }
       
    }
}
