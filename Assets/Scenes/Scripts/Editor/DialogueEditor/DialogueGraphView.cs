using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;

public class DialogueGraphView : GraphView
{

    private readonly Vector2 defaultNodeSize = new Vector2(150, 200);
    public DialogueGraphView()
    {//
        styleSheets.Add(Resources.Load<StyleSheet>("DialougeGraph"));
      //styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scenes/Scripts/Editor/DialogueGraph.uss"));
SetupZoom(ContentZoomer.DefaultMinScale,ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new ContentDragger());

        var grid = new GridBackground();
        Insert(0,grid);
        grid.StretchToParentSize();
        

        AddElement(GenerateEntryPointNode());

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

    public void CreateNode(string nodeName)
    {
        AddElement(CreateDialogueNode(nodeName));
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
        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    internal DialogueNode CreateDialogueNode(string nodeName)
    {
        var dialogueNode = new DialogueNode { title = nodeName, DialougeText = nodeName, GUID = Guid.NewGuid().ToString() };

        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);

var button = new Button(() =>{AddChoicePort(dialogueNode);});
button.text = "new Choice";
dialogueNode.titleContainer.Add(button);

        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
        dialogueNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));
        return dialogueNode;
    }

    private void AddChoicePort(DialogueNode dialogueNode)
    {
        var generatedPort = GeneratePort(dialogueNode,Direction.Output);
        var outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;
        generatedPort.portName = $"Choice {outputPortCount}";

        dialogueNode.outputContainer.Add(generatedPort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }
}
