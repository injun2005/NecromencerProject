using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObjectInfo
{
    public string objectName;
    public GameObject perfab;
    public int count;
}

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    [SerializeField]
    ObjectInfo[] objectInfos = null;

    [Header("오브젝트 풀의 위치")]
    [SerializeField]
    Transform tfPoolParent;

    public Dictionary<string,Queue<GameObject>> objectPoolList = new Dictionary<string, Queue<GameObject>>(); 

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        ObjectPoolState();
    }

    void ObjectPoolState()
    {
        if (objectInfos != null)
        {
            for (int i = 0; i < objectInfos.Length; i++)
            {
                objectPoolList.Add(objectInfos[i].objectName, InsertQueue(objectInfos[i]));
            }
        }
    }
    public void Push(string ObjName, GameObject obj)
    {
        if(objectPoolList.ContainsKey(ObjName))
        {
            obj.SetActive(false);
            objectPoolList[ObjName].Enqueue(obj);
        }
    }

    public GameObject Pop(string objName)
    { 
        GameObject obj = null;
        if (objectPoolList.ContainsKey(objName))
        {
            obj  = objectPoolList[objName].Dequeue();
            obj.SetActive(true);
        }
        return obj;
    }
    Queue<GameObject> InsertQueue(ObjectInfo perfab_objectInfo)
    {
        Queue<GameObject> queue = new Queue<GameObject>();

        for (int i = 0; i < perfab_objectInfo.count; i++)
        {
            GameObject objectClone = Instantiate(perfab_objectInfo.perfab) as GameObject;
            objectClone.SetActive(false);
            objectClone.transform.SetParent(tfPoolParent);
            queue.Enqueue(objectClone);
        }

        return queue;
    }
}
