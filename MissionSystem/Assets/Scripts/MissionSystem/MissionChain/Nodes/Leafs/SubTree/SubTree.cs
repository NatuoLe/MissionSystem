using NodeCanvas.BehaviourTrees;
using NodeCanvas.DialogueTrees;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace GNode.MissionSystem
{
    [Name("Sub Chain")]
    [Description("Executes a sub Behaviour Tree. The status of the root node in the SubTree will be returned.")]
    [ParadoxNotion.Design.Icon("BT")]
    [DropReferenceType(typeof(MissionChain))]
    public class SubTree : GNodeNested<MissionChain>
    {
        [SerializeField, ExposeField] private BBParameter<MissionChain> _subTree = null;

        public override MissionChain subGraph { get { return _subTree.value; } set { _subTree.value = value; } }
        public override BBParameter subGraphParameter => _subTree;
        public override bool allowAsPrime => true;
        

        protected override void OnReset() {
            if ( currentInstance != null ) {
                currentInstance.Stop();
            }
        }

        public void Execute()
        {
            MissionModule.Instance.StartMission(_subTree.value);
        }
    }
}