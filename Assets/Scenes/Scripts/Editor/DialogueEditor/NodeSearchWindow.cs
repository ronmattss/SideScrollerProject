using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace SideScrollerProject
{
    
    public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private DialogueGraphView _graphView;
        private EditorWindow _window;
        private Texture2D _indentation;
        public void Init(DialogueGraphView graphView,EditorWindow window)
        {
            _graphView = graphView;
            _window = window;

            _indentation = new Texture2D (1,1);
            _indentation.SetPixel(0,0,new Color(0,0,0,0));
            _indentation.Apply();
            
        }
        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry> {
            new SearchTreeGroupEntry(new GUIContent("Create Elements"), 0),
            new SearchTreeGroupEntry(new GUIContent("Dialogue Node"),1),
            new SearchTreeEntry(new GUIContent("Dialogue Node",_indentation))
            {
                userData = new DialogueNode(),level =2
            },
            
            };
            return tree;
        }

        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            var worldMousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent,context.screenMousePosition - _window.position.position);
           var localMousePosition = _graphView.contentViewContainer.WorldToLocal(worldMousePosition);
           switch(SearchTreeEntry.userData)
           {
               case DialogueNode dialogueNode:
               _graphView.CreateNode("DialogueNode",localMousePosition);
               return true;
               default:
               return false;
           }
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}