using System.Collections.Generic;

namespace GNode.MissionSystem
{
    public class MissionChainBridge : IMissionSystemComponent<object>
    {
        /// <summary>
        /// 隐式任务
        /// </summary>
        private readonly Dictionary<string, Mission<object>> implicitHandles =
            new Dictionary<string, Mission<object>>();

        /// <summary>
        /// 显式任务
        /// </summary>
        public readonly Dictionary<string, Mission<object>> ExplicitHandles =
            new Dictionary<string, Mission<object>>();

        public void OnMissionStarted(Mission<object> mission)
        {
            if (mission.explicitMission)
            {
                ExplicitHandles.Add(mission.id, mission);
            }
            else
            {
                implicitHandles.Add(mission.id, mission);
            }
        }

        public void OnMissionRemoved(Mission<object> mission, bool isFinished)
        {
            throw new System.NotImplementedException();
        }

        public void OnMissionStatusChanged(Mission<object> mission, bool isFinished)
        {
            /*if (explicitHandles.ContainsKey(mission.id))
            {
                explicitHandles[mission.id] = mission;
            }*/
        }

        public void OnMonitorRegistered(Mission<object> Proto)
        {
            throw new System.NotImplementedException();
        }

        public void OnMonitorRemoved(Mission<object> monitor)
        {
            throw new System.NotImplementedException();
        }

        public void OnMonitorReset(Mission<object> monitor)
        {
            throw new System.NotImplementedException();
        }

        public void OnMonitorInvoke(Mission<object> monitor)
        {
            throw new System.NotImplementedException();
        }
    }
}