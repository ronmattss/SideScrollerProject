using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements.Experimental;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using UnityEditor;
using System;

namespace SideScrollerProject
{
    public class GraphSaveUtility
    {

        private DialogueGraphView _targetGraphView;
        private DialogueContainer _containerCache;
        private List<Edge> edges => _targetGraphView.edges.ToList();
        private List<DialogueNode> nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();
        public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
        {
            return new GraphSaveUtility
            {
                _targetGraphView = targetGraphView
            };
        }

        private bool SaveNodes(DialogueContainer dialogueContainer)
        {
            if (!edges.Any()) return false; // if there are no edges( connections) return;

           // var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

            var connectedPorts = edges.Where(x => x.input.node != null).ToArray();

            for (int i = 0; i < connectedPorts.Length; i++)
            {
                var outputNode = connectedPorts[i].output.node as DialogueNode;
                var inputNode = connectedPorts[i].input.node as DialogueNode;

                dialogueContainer.nodeLinks.Add(new NodeLinkData { baseNodeGuid = outputNode.GUID, portName = connectedPorts[i].output.portName, targetNodeGuid = inputNode.GUID });
            }

            foreach (var dialogueNode in nodes.Where(nodes => !nodes.EntryPoint))
            {
                dialogueContainer.dialogueNodeData.Add(new DialogueNodeData
                {
                    guid = dialogueNode.GUID,
                    dialogueText = dialogueNode.DialougeText,
                    postion = dialogueNode.GetPosition().position
                });
            }
            return true;
            //create folder if does not exist

        }



        public void SaveGraph(string fileName)
        {
            var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();
            if(!SaveNodes(dialogueContainer)) return;
            SaveExposedProperties(dialogueContainer);

            if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");
            AssetDatabase.CreateAsset(dialogueContainer, $"Assets//Resources//{fileName}.asset");
            AssetDatabase.SaveAssets();
        }

        private void SaveExposedProperties(DialogueContainer dialogueContainer)
        {
            dialogueContainer.exposedProperties.AddRange(_targetGraphView.exposedProperties);
        }

        public void LoadGraph(string fileName)
        {
            _containerCache = Resources.Load<DialogueContainer>(fileName);
            if (_containerCache == null)
            {
                EditorUtility.DisplayDialog("File Not Found", "Target dialogue file does not exist", "OK");
                return;
            }

            ClearGraph();
            CreateNodes();
            ConnectNodes();
            CreateExposedProperties();

        }

        private void CreateExposedProperties()
        {
          // Clear existing Properties
          _targetGraphView.ClearBlackBoardAndExposedProperties();
          foreach(var exposedProperty in _containerCache.exposedProperties)
          {
              _targetGraphView.AddPropertyToBlackBoard(exposedProperty);
          }
        }

        private void ConnectNodes()
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                var connections = _containerCache.nodeLinks.Where(x => x.baseNodeGuid == nodes[i].GUID).ToList();

                for (int j = 0; j < connections.Count; j++)
                {
                    var targetNodeGuid = connections[j].targetNodeGuid;
                    var targetNode = nodes.First(x => x.GUID == targetNodeGuid);
                    LinkNodes(nodes[i].outputContainer[j].Q<Port>(), (Port)targetNode.inputContainer[0]);
                    targetNode.SetPosition(new Rect(_containerCache.dialogueNodeData.First(x => x.guid == targetNodeGuid).postion, _targetGraphView.defaultNodeSize));
                }
            }
        }

        private void LinkNodes(Port output, Port input)
        {
            var tempEdge = new Edge { output = output, input = input };
            tempEdge?.input.Connect(tempEdge);
            tempEdge?.output.Connect(tempEdge);
            _targetGraphView.Add(tempEdge);
        }

        private void CreateNodes()
        {
            foreach (var nodeData in _containerCache.dialogueNodeData)
            {
                var tempNode = _targetGraphView.CreateDialogueNode(nodeData.dialogueText, Vector2.zero);
                tempNode.GUID = nodeData.guid;
                _targetGraphView.AddElement(tempNode);

                var nodePorts = _containerCache.nodeLinks.Where(x => x.baseNodeGuid == nodeData.guid).ToList();
                nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.portName));
            }
        }

        private void ClearGraph()
        {
            //Set Entry points GUID back from the save. Discard existing GUID
            nodes.Find(x => x.EntryPoint).GUID = _containerCache.nodeLinks[0].baseNodeGuid;
            foreach (var node in nodes)
            {
                if (node.EntryPoint) continue;
                //Remove edges that connected to this node
                edges.Where(x => x.input.node == node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));
                // then Remove the noed
                _targetGraphView.RemoveElement(node);
            }
        }
    }
}
