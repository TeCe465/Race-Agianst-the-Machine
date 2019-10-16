using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Rigidbody[] allRigidBodies;
    public GameObject ConditionsObj;

    List<MonoBehaviour> conditionsList = new List<MonoBehaviour>();

    void Start()
    {
        foreach(MonoBehaviour item in ConditionsObj.GetComponents<MonoBehaviour>())
        {
            conditionsList.Add(item);
        }

        //conditionsList[1].enabled = true;

        allRigidBodies = Object.FindObjectsOfType<Rigidbody>();
        foreach(Rigidbody item in allRigidBodies)
        {
            GameObject tempObj = item.transform.gameObject;
            Vector3 objectSize = Vector3.Scale(tempObj.transform.localScale, tempObj.GetComponent<MeshRenderer>().bounds.size);
            item.mass = Mathf.Abs(objectSize.x * objectSize.y * objectSize.z);

            //Debug.Log(item.name + " mass: " + item.mass);
        }
    }

    void Update()
    {
    }
}
