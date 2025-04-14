using System;
using System.Collections.Generic;
using UnityEngine;

namespace GNode.MissionSystem
{
    public class MissionChainManager : IMissionSystemComponent<object>
    {
        private readonly MissionManager<object> missionManager;
        private readonly Dictionary<string, MissionChainHandle> handles = new Dictionary<string, MissionChainHandle>();
        
        public MissionChainManager(MissionManager<object> missionManager)
        {
            this.missionManager = missionManager;
        }

        public void StartChain(MissionChain chain)
        {
            if (chain == null || handles.ContainsKey(chain.name)) return;
            var handle = new MissionChainHandle(chain);
            handle.FlushBuffer(t => missionManager.StartMission(t));
            if (!handle.IsCompleted)
            {
                handles.Add(chain.name, handle);
            }
            else
            {
                Debug.Log("[Manager]单任务完成");
                //说明第一个此节点只有一个子节点
                handle.OnMissionComplete(chain.primeNode.UID, true);
            }
               
        }

        public void OnMissionStarted(Mission<object> mission) { }

        public void OnMissionRemoved(Mission<object> mission, bool isFinished)
        {
            // Get the mission chain handle
            var missionChainId = mission.id.Split('.')[0];
            if (!handles.TryGetValue(missionChainId, out var handle)) return;
            
            // Notify the handle that the mission is completed
            handle.OnMissionComplete(mission.id, isFinished);
            handle.FlushBuffer(t => missionManager.StartMission(t));
            
            // Remove the handle if the mission is finished
            if (handle.IsCompleted) handles.Remove(missionChainId);
        }

        public void OnMissionStatusChanged(Mission<object> mission, bool isFinished) { }
    }
}