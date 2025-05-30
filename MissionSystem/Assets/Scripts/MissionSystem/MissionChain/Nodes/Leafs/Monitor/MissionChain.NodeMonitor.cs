using System;
using System.Text;
using System.Collections.Generic;
using ParadoxNotion.Design;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GNode.MissionSystem
{
    [ParadoxNotion.Design.Icon("Eye"), Name("Monitor"), Color("D2B31A")]
    [Description("监视器适合于全局检测")]
    public class NodeMonitor : NodeRequire
    {
        public override bool allowAsPrime => true;

        [SerializeField] private readonly List<MissionRequireTemplate> _requires =
            new List<MissionRequireTemplate>();

        [SerializeField] private MissionRequireMode _mode;

        /// <summary>create mission prototype</summary>
        /// <returns></returns>
        public MissionPrototype<object> MissionProto
        {
            get
            {
                var proto = new MissionPrototype<object>(MissionId, _requires.ToArray(), null, _mode);
                return proto;
            }
        }

        public string MissionId => $"{graph.name}.{base.UID}";
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

        protected override void OnNodeGUI()
        {
            GUILayout.BeginVertical(Styles.roundedBox);
            if (_requires.Count == 0)
            {
                GUILayout.Label("No Requires");
            }
            else
            {
                var builder = new StringBuilder();
                foreach (var require in _requires)
                    builder.AppendLine(require.Summary);
                GUILayout.Label(builder.ToString().Trim('\n'));
            }

            GUILayout.EndVertical();
        }

        protected override void OnNodeInspectorGUI()
        {
            /* draw requires */
            GUILayout.Label("<color=#fffde3><size=12><b>Requires List</b></size></color>");
            GUILayout.BeginVertical("box");
            EditorUtils.ReorderableList(_requires, (index, picked) =>
            {
                var require = _requires[index];
                require.DrawInspectorGUI();
            });
            if (_requires.Count > 1)
            {
                _mode = (MissionRequireMode)EditorGUILayout.EnumPopup("Require Mode", _mode);
            }

            GUILayout.EndVertical();


            /* add new require */
            GUI.backgroundColor = Colors.lightBlue;
            if (GUILayout.Button("Add Require"))
            {
                Action<Type> OnTypeSelected = type =>
                {
                    var require = (MissionRequireTemplate)Activator.CreateInstance(type);
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