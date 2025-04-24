

namespace GNode.MissionSystem
{
    public abstract class NodeRequire:NodeBase
    {
        #if UNITY_EDITOR
        public abstract void DeleteRequire(MissionRequireTemplate require);

        public abstract void AddRequire(MissionRequireTemplate require);
        #endif
        public string MissionId => $"{graph.name}.{base.UID}";
    }
}