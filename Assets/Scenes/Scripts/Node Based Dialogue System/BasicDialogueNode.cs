using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class BasicDialogueNode : Node {

	[Input] public string a;
	[Output] public string b;
	protected override void Init() {
		base.Init();
		
	}

	// Return the correct value of an output port when requested
	public override object GetValue(NodePort port) {
		Debug.Log(port.fieldName);
		return null;
	}
}