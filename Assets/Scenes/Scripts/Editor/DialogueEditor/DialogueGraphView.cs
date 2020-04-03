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
    public class DialogueGraphView : GraphView
    {

        public readonly Vector2 defaultNodeSize = new Vector2(150, 200);
        public Blackboard blackboard;
        public List<ExposedProperty> exposedProperties = new List<ExposedProperty>();
        private NodeSearchWindow _searchWindow;
        public DialogueGraphView(EditorWindow editorWindow)
        {//
            styleSheets.Add(Resources.Load<StyleSheet>("DialougeGraph"));
            //styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scenes/Scripts/Editor/DialogueGraph.uss"));
            SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new ContentDragger());

            var grid = new GridBackground();
            Insert(0, grid);
            grid.StretchToParentSize();


            AddElement(GenerateEntryPointNode());
            AddSearchWindow(editorWindow);

        }
        public void ClearBlackBoardAndExposedProperties()
        {
            exposedProperties.Clear();
            blackboard.Clear();
        }
        public void AddPropertyToBlackBoard(ExposedProperty exposedProperty)
        {
            var localPropertyName = exposedProperty.propertyName;
            var localPropertyValue = exposedProperty.propertyValue;
            while (exposedProperties.Any(x => x.propertyName == localPropertyName))
                localPropertyName = $"{localPropertyName}(1)";

            var property = new ExposedProperty();
            property.propertyName = localPropertyName;
            property.propertyValue = localPropertyValue;
            exposedProperties.Add(property);

            var container = new VisualElement();
            var blackboardField = new BlackboardField{text =property.propertyName,typeText = "string property"};
            container.Add(blackboardField);

            var propertyValueTextField = new TextField("Value")
            {
                value = localPropertyValue
            };
            propertyValueTextField.RegisterValueChangedCallback((EventCallback<ChangeEvent<string>>)(evt=>
            {
            var changingPropertyIndex = exposedProperties.FindIndex(x => x.propertyName==property.propertyName);
            exposedProperties[changingPropertyIndex].propertyValue = evt.newValue;
            }));
            var blackboardValueRow = new BlackboardRow(blackboardField,propertyValueTextField);
            container.Add(blackboardValueRow);
            blackboard.Add(container);


        }

        private void AddSearchWindow(EditorWindow editorWindow)
        {
           _searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
           _searchWindow.Init(this,editorWindow);
           nodeCreationRequest = ContextBoundObject=> SearchWindow.Open(new SearchWindowContext(ContextBoundObject.screenMousePosition), _searchWindow);
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();

            ports.ForEach((port =>
            {
                if (startPort != port && startPort.node != port.node)
                {
                    compatiblePorts.Add(port);
                }

            }));
            return compatiblePorts;
        }

        public void CreateNode(string nodeName, Vector2 position)
        {
            AddElement(CreateDialogueNode(nodeName,position));
        }

        private Port GeneratePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
        {
            return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
        }



        private DialogueNode GenerateEntryPointNode()
        {
            var node = new DialogueNode { title = "START", GUID = Guid.NewGuid().ToString(), DialougeText = "ENTRYPOINT", EntryPoint = true };

            var generatedPort = GeneratePort(node, Direction.Output);
            generatedPort.portName = "Next";
            node.outputContainer.Add(generatedPort);

            node.capabilities&= ~Capabilities.Movable;
            node.capabilities &= Capabilities.Deletable;
            

            node.RefreshExpandedState();
            node.RefreshPorts();

            node.SetPosition(new Rect(100, 200, 100, 150));
            return node;
        }

        internal DialogueNode CreateDialogueNode(string nodeName,Vector2 position)
        {
            var dialogueNode = new DialogueNode { title = nodeName, DialougeText = nodeName, GUID = Guid.NewGuid().ToString() };

            var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
            inputPort.portName = "Input";
            dialogueNode.inputContainer.Add(inputPort);

            dialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

            var button = new Button(() => { AddChoicePort(dialogueNode); });
            button.text = "new Choice";
            dialogueNode.titleContainer.Add(button);

            var textField = new TextField(string.Empty);
            textField.RegisterValueChangedCallback((EventCallback<ChangeEvent<string>>)(evt => 
            
            {
                dialogueNode.DialougeText = evt.newValue;
                dialogueNode.title = evt.newValue;
            }
            ));
            textField.SetValueWithoutNotify(dialogueNode.title);
            dialogueNode.mainContainer.Add(textField);

            dialogueNode.RefreshExpandedState();
            dialogueNode.RefreshPorts();
            dialogueNode.SetPosition(new Rect(position, defaultNodeSize));
            return dialogueNode;
        }

        public void AddChoicePort(DialogueNode dialogueNode, string overridenPortName = "")
        {
            var generatedPort = GeneratePort(dialogueNode, Direction.Output);
            var oldLabel = generatedPort.contentContainer.Q<Label>("type");
            generatedPort.contentContainer.Remove(oldLabel);
            var outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;

            var choicePortName = string.IsNullOrEmpty(overridenPortName) ? $"Choice{outputPortCount + 1}" : overridenPortName;


            var textField = new TextField { name = string.Empty, value = choicePortName };
            textField.RegisterValueChangedCallback((EventCallback<ChangeEvent<string>>)(evt => generatedPort.portName = evt.newValue));
            generatedPort.contentContainer.Add(new Label(" "));
            generatedPort.contentContainer.Add(textField);
            var deleteButton = new Button(() => RemovePort(dialogueNode, generatedPort))
            {
                text = "X"
            };
            generatedPort.contentContainer.Add(deleteButton);


            generatedPort.portName = choicePortName;
            dialogueNode.outputContainer.Add(generatedPort);
            dialogueNode.RefreshPorts();
            dialogueNode.RefreshExpandedState();
        }

        private void RemovePort(DialogueNode dialogueNode, Port generatedPort)
        {
            var targetEdge = edges.ToList().Where(x => x.output.portName == generatedPort.portName && x.output.node == generatedPort.node);
            if (targetEdge.Any())
            {
                var edge = targetEdge.First();
                edge.input.Disconnect(edge);
                RemoveElement(targetEdge.First());
            }
            dialogueNode.outputContainer.Remove(generatedPort);
            dialogueNode.RefreshPorts();
            dialogueNode.RefreshExpandedState();
        }
    }
}