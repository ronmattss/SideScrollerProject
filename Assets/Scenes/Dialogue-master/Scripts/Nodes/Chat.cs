using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Dialogue
{
    [NodeTint("#CCFFCC")]
    public class Chat : DialogueBaseNode
    {

        public delegate void invokeMethod();
        public CharacterInfo character;
        [TextArea] public string text;
        public List<GameObject> ListOfGameObjects;
        public List<Vector3> ListOfPositions;
        public List<Spawnable> ListofSpawnables;

        [Output(dynamicPortList = true)] public List<Answer> answers = new List<Answer>();

        [System.Serializable]
        public class Answer
        {
            public string text;
            public AudioClip voiceClip;

        }

        public void AnswerQuestion(int index)
        {
            NodePort port = null;
            if (answers.Count == 0)
            {
                port = GetOutputPort("output");
            }
            else
            {
                if (answers.Count <= index) return;
                port = GetOutputPort("answers " + index);
                Debug.Log(port.Connection.node.name);
            }

            if (port == null) return;
            for (int i = 0; i < port.ConnectionCount; i++)
            {
                NodePort connection = port.GetConnection(i);
                (connection.node as DialogueBaseNode).Trigger();
            }
        }

        public override void Trigger()
        {
            (graph as DialogueGraph).current = this;
        }
    }
}