using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField]
    private GameObject startPoint, endPoint;

    [SerializeField]
    private bool isRunning = true;

    private WayPoint searchCenter;

    public List<WayPoint> path = new List<WayPoint>();

    //通过字典保存游戏内的坐标
    public Dictionary<Vector2Int, WayPoint> wayPointDic = new Dictionary<Vector2Int, WayPoint>();

    private Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };


    private Queue<WayPoint> queue = new Queue<WayPoint>();

    private void Start()
    {
       
    }

    public List<WayPoint> GetPath()
    {
        startPoint.GetComponent<MeshRenderer>().material.color = Color.blue;
        endPoint.GetComponent<MeshRenderer>().material.color = Color.red;
        LoadAllWayPoint();
        //  ExploreAround();
        BFS();
        CreatePath();
        return path;
    }


    //把场景中的点都存放进字典中
    private void LoadAllWayPoint()
    {
        var wayPoints = FindObjectsOfType<WayPoint>();
        foreach (WayPoint wayPoint in wayPoints)
        {
            var tempWayPoint = wayPoint.GetPosition();
            if (wayPointDic.ContainsKey(tempWayPoint))
            {
                Debug.Log("字典中已经有这个键对应的值的坐标");
            }
            else
            {
                wayPointDic.Add(tempWayPoint, wayPoint);
            }
        }
    }

    //Version1:获取初始点的子节点（上右下左），通过字典的键来访问它们
    //Version2:遍历当前节点的子节点，传入参数_from
    //由于把searchCenter声明成全局变量，所以去除参数
    private void ExploreAround()
    {
        if (!isRunning)
            return;

        foreach (Vector2Int direction in directions)
        {
            // Debug.Log("Exploring: "+ exploreArounds);
            // var exploreArounds = startPoint.GetComponent<WayPoint>().GetPosition() + direction; //初始点的周围子节点
            var exploreArounds = searchCenter.GetPosition() + direction;

            if (wayPointDic.ContainsKey(exploreArounds))
            {
                //子节点已经被搜索 或者 当前队列中有这个子节点 （防止重复搜索一个节点）
                if (!(wayPointDic[exploreArounds].isExplored || queue.Contains(wayPointDic[exploreArounds])))
                {
                  //  wayPointDic[exploreArounds].GetComponent<MeshRenderer>().material.color = Color.green;
                    queue.Enqueue(wayPointDic[exploreArounds]);
                    wayPointDic[exploreArounds].exploredFrom = searchCenter; //将四周的子节点的来源存储到四周子节点的exploredFrom变量中
                }
            }
        }
    }



    //搜索顺序为上右下左
    public void BFS()
    {
        //初始点加入队列中
        queue.Enqueue(startPoint.GetComponent<WayPoint>());
        //判断队列是否为空
        while (queue != null && isRunning)
        {
            //队首出队
            searchCenter = queue.Dequeue();
            Debug.Log("Search From: " + searchCenter.GetPosition());
            //判断是否是目标点
            if (searchCenter == endPoint.GetComponent<WayPoint>())
            {
                //是目标点则停止继续搜索
                isRunning = false;
            }
            else
            {
                //继续遍历searchCenter四周子节点
                ExploreAround();
                searchCenter.isExplored = true;
            }
        }
        Debug.Log("搜索完成");
    }


    private void CreatePath()
    {    
        path.Add(endPoint.GetComponent<WayPoint>()); //首先获取目标点的信息

        WayPoint prePoint = endPoint.GetComponent<WayPoint>().exploredFrom; //表示目标点是通过prePoint搜索而来的，即目标点的父节点为prePoint

        //一直往前找，直到找到初始点
        while (prePoint != startPoint.GetComponent<WayPoint>())
        {
            prePoint.GetComponent<MeshRenderer>().material.color = Color.yellow;
            path.Add(prePoint);
            prePoint = prePoint.exploredFrom;
        }

        path.Add(startPoint.GetComponent<WayPoint>());
        path.Reverse();
    }








}
