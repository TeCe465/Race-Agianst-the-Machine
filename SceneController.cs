using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    public Rigidbody[] allRigidBodies;
    public GameObject ConditionsObj;
    public int NumOfConditions;
    public bool RandomEnabled = false;
    public int HelpingHand = 0;

    List<MonoBehaviour> conditionsList = new List<MonoBehaviour>();

    void Start()
    {

        foreach(MonoBehaviour item in ConditionsObj.GetComponents<MonoBehaviour>())
        {
            conditionsList.Add(item);
        }

        
        if (RandomEnabled)
        {
            for (int i = 0; i < NumOfConditions; i++)
            {
                int rand = Random.Range(0, conditionsList.Count);
                conditionsList[rand].enabled = true;
            }
        }

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
