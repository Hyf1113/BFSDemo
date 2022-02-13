using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    public bool isExplored;

    //表示当前的点是由之前的哪一个点搜索得到的
    public WayPoint exploredFrom;

    private void Start()
    {
       // Debug.Log(gameObject.name + ":" + GetPosition());
    }

    //自定义坐标
    public Vector2Int GetPosition()
    {
        return new Vector2Int(Mathf.RoundToInt(transform.position.x / 2f) , Mathf.RoundToInt(transform.position.z / 2f));
    }
}
