using System.Collections.Generic;
using ParadoxNotion.Design;
using UnityEngine;

namespace GNode.MissionSystem
{
    [ParadoxNotion.Design.Icon("start"), Color("E66E71"), Name("Start")]
    [Description("Start a mission No Require")]
    public class NodeStart : NodeMission
    {
        public override bool allowAsPrime => true;

        [SerializeField]
        private List<MissionRequireTemplate> _requires
        {
            get
            {
                List<MissionRequireTemplate> list = new List<MissionRequireTemplate>();
                MissionRequireTemplate t = new StartRequire();
                list.Add(t);
                return list;
            }
        }

        [SerializeField] private MissionRequireMode _mode;

        /// <summary>create mission prototype</summary>
        /// <returns></returns>
        public override MissionPrototype<object> MissionProto
        {
            get
            {
                var proto = new MissionPrototype<object>(MissionId, _requires.ToArray(), null, _mode);
                return proto;
            }
        }

        public string MissionId => $"{graph.name}.{base.UID}";
    }
}