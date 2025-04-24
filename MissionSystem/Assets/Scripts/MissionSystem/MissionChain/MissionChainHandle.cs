using System;
using System.Collections.Generic;
using System.Linq;
using NodeCanvas.Framework;
using UnityEngine;

namespace GNode.MissionSystem
{
    public class MissionChainHandle
    {
        private readonly MissionChain chain;
        private readonly Dictionary<string, NodeMission> activeNodes = new Dictionary<string, NodeMission>();
        private readonly Queue<NodeMission> buffer = new Queue<NodeMission>();

        private readonly Queue<NodeMonitor> _monitorBuffer = new Queue<NodeMonitor>();

        //private List<string> _registeredMonitorIds = new List<string>();
        /// <summary>
        /// 这个是通过监视器注册的任务
        /// </summary>
        private readonly HashSet<string> _associatedMonitors = new HashSet<string>();

        public bool IsCompleted => activeNodes.Count == 0;

        public MissionChainHandle(MissionChain chain)
        {
            this.chain = chain;

            /* execute prime node */
            if (chain.primeNode != null)
                ExecuteNode(chain.primeNode as NodeBase);
        }

        public void FlushBuffer(System.Action<MissionPrototype<object>> deployer)
        {
            if (buffer.Count == 0) return;
            while (buffer.Count > 0)
            {
                var node = buffer.Dequeue();
                var missionProto = node.MissionProto;
                activeNodes.Add(missionProto.id, node);
                deployer(missionProto);
            }
        }

        public void OnMissionComplete(string missionId, bool continues)
        {
            if (!activeNodes.Remove(missionId, out var node)) return;

            /* execute all available output connections */
            if (continues)
            {
                foreach (var outConnection in node.outConnections.Where(c => ((ConnectionBase)c).IsAvailable))
                    ExecuteNode(outConnection.targetNode as NodeBase);
            }
        }

        #region Monitor

        public void FlushMonitorBuffer(System.Action<MissionPrototype<object>> deployer)
        {
            if (_monitorBuffer.Count == 0) return;
            while (_monitorBuffer.Count > 0)
            {
                var node = _monitorBuffer.Dequeue();
                var missionProto = node.MissionProto;
                deployer(missionProto);
            }
        }

        public void AddMonitorMission(string MissionId)
        {
            _associatedMonitors.Add(MissionId);
        }

        public void RemoveMonitors(Action<string> removeAction)
        {
            foreach (var MissionId in _associatedMonitors)
            {
                removeAction?.Invoke(MissionId);
            }

            _associatedMonitors.Clear();
        }

        public void OnMonitorComplete(string protoID)
        {
            // 1. 从已关联监控任务集合中移除
            _associatedMonitors.Remove(protoID);

            // 2. 查找与此监控任务关联的节点
            var monitorNode = chain.GetNode(protoID) as NodeMonitor;
            if (monitorNode == null) return;

            // 3. 执行监控节点的后续连接
            foreach (var outConnection in monitorNode.outConnections.Where(c => ((ConnectionBase)c).IsAvailable))
            {
                ExecuteNode(outConnection.targetNode as NodeBase);
            }

            // 4. 可选：记录监控任务完成状态（如果需要持久化）
            Debug.Log($"监控任务完成: {protoID}");

            // 5. 如果这是最后一个未完成的监控任务且没有其他活动节点，可以触发完成事件
            if (_associatedMonitors.Count == 0 && activeNodes.Count == 0)
            {
                // 可以在这里触发任务链的"软完成"事件
                // 因为监控任务不影响主要任务链的完成状态，但可能有额外逻辑
            }
        }

        #endregion

        /// <summary>execute given node</summary>
        public void ExecuteNode(NodeBase node)
        {
            if (node is null) return;
            switch (node)
            {
                /* execute action node */
                case NodeAction actionNode:
                    actionNode.Execute();
                    break;
                /* execute mission node, add output prototype to buffer queue */
                case NodeMission missionNode:
                    if (activeNodes.ContainsKey(missionNode.MissionId)) return;
                    Debug.Log($"{this.GetHashCode()}[Buffer_Enqueue].{missionNode.MissionId}");
                    buffer.Enqueue(missionNode);
                    break;
                case SubTree tree:
                    tree.Execute();
                    break;
                case NodeStarter starter:
                    Debug.Log("执行Start");
                    starter.Execute(ExecuteNode);
                    break;
                case NodeMonitor monitor:
                    _monitorBuffer.Enqueue(monitor);
                    break;
            }
        }
    }
}