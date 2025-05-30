﻿using System;
using System.Text;
using System.Collections.Generic;
using ParadoxNotion.Design;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GNode.MissionSystem
{
    [ParadoxNotion.Design.Icon("community-fill"), Color("b1d480"), Name("Mission")]
    [Description("setup a new mission")]
    public class NodeMission : NodeRequire
    {
        [SerializeField] private bool explicitMission;
        [SerializeField] private MissionRequireTemplate _submitRequire;
        public override bool allowAsPrime => true;

        [SerializeField] private readonly List<MissionRequireTemplate> _requires =
            new List<MissionRequireTemplate>();

        [SerializeField] private MissionRequireMode _mode;

        /// <summary>create mission prototype</summary>
        /// <returns></returns>
        public virtual MissionPrototype<object> MissionProto
        {
            get
            {
                var proto = new MissionPrototype<object>(MissionId, _requires.ToArray(), null, _mode);
                return proto;
            }
        }

        public string MissionId => $"{graph.name}.{name}.{base.UID}";

#if UNITY_EDITOR

        /// <summary>remove given require from current list</summary>
        public override void DeleteRequire(MissionRequireTemplate require)
        {
            /* do safe check before record undo action */
            if (_requires.Contains(require))
            {
                UndoUtility.RecordObject(graph, "Require Deleted");
                _requires.Remove(require);
            }
        }

        /// <summary>add new require to current list</summary>
        public override void AddRequire(MissionRequireTemplate require)
        {
            if (require is null || _requires.Contains(require)) return;
            UndoUtility.RecordObject(graph, "Require Added");
            require._node = this;
            _requires.Add(require);
        }

        protected string RequireModeLabel
        {
            get
            {
                return _mode switch
                {
                    MissionRequireMode.Any => "Complete Any Require",
                    MissionRequireMode.All => "Complete All Requires",
                    _ => string.Empty
                };
            }
        }

        protected override void OnNodeGUI()
        {
            GUILayout.BeginVertical(Styles.roundedBox);
            if (explicitMission)
            {
                GUILayout.Label($"<i>任务类型:<color=#46d6e0>显式任务</color></i>");
            }
            else
            {
                GUILayout.Label($"<i>任务类型:<color=#969696>隐式任务</color></i>");
            }

            if (_requires.Count == 0)
            {
                GUILayout.Label("No Requires");
            }
            else
            {
                if (_requires.Count > 1)
                {
                    GUILayout.Label($"<i><color=#969696>{RequireModeLabel}</color></i>");
                }

                var builder = new StringBuilder();
                foreach (var require in _requires)
                    builder.AppendLine(require.Summary);
                GUILayout.Label(builder.ToString().Trim('\n'));
            }

            GUILayout.EndVertical();
        }

        protected override void OnNodeInspectorGUI()
        {
            GUILayout.Label("<color=#fffde3><size=12><b>任务显隐</b></size></color>");
            // 添加一个复选框来控制 bool 值
            explicitMission = GUILayout.Toggle(explicitMission, "显式任务");
            /* draw requires */
            GUILayout.Label("<color=#fffde3><size=12><b>需求列表</b></size></color>");
            GUILayout.BeginVertical("box");
            EditorUtils.ReorderableList(_requires, (index, picked) =>
            {
                var require = _requires[index];
                require.DrawInspectorGUI();
            });
            if (_requires.Count > 1)
            {
                _mode = (MissionRequireMode) EditorGUILayout.EnumPopup("Require Mode", _mode);
            }

            GUILayout.EndVertical();


            /* add new require */
            GUI.backgroundColor = Colors.lightBlue;
            if (GUILayout.Button("Add Require"))
            {
                Action<Type> OnTypeSelected = type =>
                {
                    var require = (MissionRequireTemplate) Activator.CreateInstance(type);
                    AddRequire(require);
                };

                var menu = EditorUtils.GetTypeSelectionMenu(typeof(MissionRequireTemplate), OnTypeSelected);
                if (CopyBuffer.TryGetCache<MissionRequireTemplate>(out var cache))
                {
                    menu.AddSeparator("/");
                    menu.AddItem(new GUIContent($"Paste {cache.Title}"), false,
                        () => { AddRequire(Utils.CopyObject(cache)); });
                }

                menu.ShowAsBrowser("Select Require", typeof(MissionRequireTemplate));
            }

            GUI.backgroundColor = Color.white;
        }
#endif
    }
}