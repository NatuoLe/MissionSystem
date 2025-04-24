using System;
using System.Collections.Generic;
using UnityEngine;

namespace GNode.MissionSystem
{
       public class MissionChainManager : IMissionSystemComponent<object>
    {
        private readonly MissionManager<object> missionManager;
        private readonly Dictionary<string, MissionChainHandle> handles = new Dictionary<string, MissionChainHandle>();
        private readonly Dictionary<string, Mission<object>> _registeredMonitors = new Dictionary<string, Mission<object>>();
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
                Debug.Log("StartMission-" + chain.name);
            }

            //注册监视器
            handle.FlushMonitorBuffer(t => missionManager.RegisterMonitor(t));
        }

        public void OnMissionStarted(Mission<object> mission)
        {
        }

        public void OnMissionRemoved(Mission<object> mission, bool isFinished)
        {
            // Get the mission chain handle
            var missionChainId = mission.id.Split('.')[0];
            if (!handles.TryGetValue(missionChainId, out var handle)) return;

            // Notify the handle that the mission is completed
            handle.OnMissionComplete(mission.id, isFinished);
            handle.FlushBuffer(t => missionManager.StartMission(t));
            //注册监视器
            handle.FlushMonitorBuffer(t => missionManager.RegisterMonitor(t));
            // Remove the handle if the mission is finished
            if (handle.IsCompleted)
            {
                // 任务链完成时移除关联的监控任务
                handle.RemoveMonitors(monitorId => missionManager.RemoveMonitor(monitorId));
                handles.Remove(missionChainId);
            }
        }

        public void OnMissionStatusChanged(Mission<object> mission, bool isFinished)
        {
        }

        public void OnMonitorRegistered(Mission<object> Proto)
        {
            if (Proto == null) return;
    
            // 1. 简单记录日志
            Debug.Log($"监控任务注册成功: {Proto.id}");
    
            // 2. 存储已注册的监控任务（用于后续管理）
            if (!_registeredMonitors.ContainsKey(Proto.id))
            {
                _registeredMonitors.Add(Proto.id, Proto);
            }
    
            // 3. 可以在这里关联监控任务与任务链的关系
            // 例如：monitor.id 可能包含任务链名称作为前缀
            var chainId = Proto.id.Split('.')[0];
            if (handles.TryGetValue(chainId, out var handle))
            {
                // 将监控任务与任务链handle关联
                handle.AddMonitorMission(Proto.id);
            }
        }

        public void OnMonitorRemoved(Mission<object> monitor)
        {
        }

        public void OnMonitorReset(Mission<object> monitor)
        {
        }

        public void OnMonitorInvoke(Mission<object> monitor)
        {
            Debug.Log($"[任务链系统] 监控任务触发: {monitor.id}");
    
            // 可以在这里处理监控任务触发后的逻辑
            // 例如：解锁新的任务链、记录游戏进度等
    
            // 如果是与任务链关联的监控任务
            var idParts = monitor.id.Split('.');
            if (idParts.Length < 2) return;
    
            var chainId = idParts[0];
            if (handles.TryGetValue(chainId, out var handle))
            {
                // 通知任务链有监控任务完成
                handle.OnMonitorComplete(monitor.id);
        
                // 可能触发任务链的下一步
                //handle.FlushBuffer(t => missionManager.StartMission(t));
            }
        }
    }
}