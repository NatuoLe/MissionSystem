using ParadoxNotion.Design;
using RedSaw.MissionSystem;
using UnityEditor;
using UnityEngine;

[Name("CompleteMission"), Category("Guide"), Description("完成任务-本地记录")]
public class CompleteMission : ActionBase
{
    [SerializeField] private string message;

    public override void Execute()
    {
        MissionModule.MissionInnerPipeline.CompleteMission(message);
    }
#if UNITY_EDITOR

    public override string Summary
    {
        get { return $"完成任务: \"{message}\""; }
    }

    protected override void OnInspectorGUI()
    {
        message = UnityEditor.EditorGUILayout.TextField("MissionKey", message);
    }
#endif
}