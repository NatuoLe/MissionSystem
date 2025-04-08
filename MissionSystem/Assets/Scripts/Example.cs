using System;
using UnityEngine;
using GNode.MissionSystem;

public class Example : MonoBehaviour
{
    [SerializeField] private MissionChain chain;
    private MissionModule _module;

    private void Start()
    {
        _module = new MissionModule();
        _module.Init();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Start Chain");
            //GameAPI.MissionChainManager.StartChain(chain);
            _module.StartMission(chain);
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("MSG: StepClick");
            //GameAPI.Broadcast(new NodeMessage(GNodeEventType.StartStep));
            _module.SendMessage(new NodeMessage(GNodeEventType.StartStep));
        }


        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("MSG: LevelArrived");
            GameAPI.Broadcast(new NodeMessage(GNodeEventType.LevelArrived));
        }
    }
}