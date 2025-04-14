using System;
using System.Linq;
using ParadoxNotion.Design;
using UnityEngine;

namespace GNode.MissionSystem
{
    [ParadoxNotion.Design.Icon("start"), Color("E66E71"), Name("Start")]
    [Description("Start a mission No Require")]
    public class NodeStarter : GNodeComposite
    {
        public override bool allowAsPrime => true;
        public string MissionId => $"{graph.name}.NodeStarter.{base.UID}";

        public void Execute(Action<NodeBase> HandleExcuteNode)
        {
            Debug.Log("执行[connections]");
            foreach (var outConnection in outConnections.Where(c => ((ConnectionBase)c).IsAvailable))
                HandleExcuteNode(outConnection.targetNode as NodeBase);
        }
    }
}