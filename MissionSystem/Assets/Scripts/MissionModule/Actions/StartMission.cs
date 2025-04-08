using ParadoxNotion.Design;
using GNode.MissionSystem;
using UnityEditor;
using UnityEngine;

[Name("StartMission"), Category("Guide"), Description("启动任务")]
public class StartMission : ActionBase
{
    [SerializeField] private string message;
    public override void Execute()
    {
        MissionModule.Instance.StartMission(message);
    }
#if UNITY_EDITOR

    public override string Summary
    {
        get { return $"开始任务: \"{message}\""; }
    }

    protected override void OnInspectorGUI()
    {
        message = UnityEditor.EditorGUILayout.TextField("MissionKey", message);
    }
#endif
}