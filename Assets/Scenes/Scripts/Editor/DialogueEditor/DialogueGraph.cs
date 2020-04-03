using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using UnityEditor.Experimental.GraphView;
using System.Linq;

namespace SideScrollerProject
{
    public class DialogueGraph : EditorWindow
    {


        private DialogueGraphView _graphView;
        private string _fileName = "New";
        // Start is called before the first frame update
        [MenuItem("Graph/Dialogue Graph")]
        public static void OpenDialogueGraphWindow()
        {
            DialogueGraph window = GetWindow<DialogueGraph>();
            window.titleContent = new GUIContent("Dialouge Graph");
        }


        private void OnEnable()
        {
            ConstructGraphView();
            GenerateToolBar();
            GenerateMiniMap();
            GenerateBlackBoard();
        }

        private void GenerateBlackBoard()
        {
            var blackboard = new Blackboard();
            blackboard.Add(new BlackboardSection { title = "Exposed Properties" });
            blackboard.addItemRequested = _blackboard => 
            {
                _graphView.AddPropertyToBlackBoard(new ExposedProperty());
            };
            blackboard.editTextRequested = (blackboard1,element,newValue) => 
            {
                var oldPropertyName = ((BlackboardField)element).text;
                if(_graphView.exposedProperties.Any(x => x.propertyName == newValue))
                {
                    EditorUtility.DisplayDialog("Error","This property name already exists","OK");
                    return;
                }

                var propertyIndex = _graphView.exposedProperties.FindIndex(x => x.propertyName == oldPropertyName);
                _graphView.exposedProperties[propertyIndex].propertyName = newValue;
                ((BlackboardField)element).text = newValue;
            };
            blackboard.SetPosition(new Rect(10,30,200,300));
            _graphView.Add(blackboard);
            _graphView.blackboard = blackboard;
        }

        private void GenerateMiniMap()
        {
            var minimap = new MiniMap { anchored = true };
            var cords = _graphView.contentViewContainer.WorldToLocal(new Vector2(this.maxSize.x-10,30));
            minimap.SetPosition(new Rect(cords.x, cords.y, 200, 140));
            _graphView.Add(minimap);
        }

        private void OnDisable()
        {
            rootVisualElement.Remove(_graphView);
        }

        private void GenerateToolBar()
        {
            var toolbar = new Toolbar();

            var filenameTextField = new TextField("File Name: ");
            filenameTextField.SetValueWithoutNotify(_fileName);
            filenameTextField.MarkDirtyRepaint();
            filenameTextField.RegisterValueChangedCallback((EventCallback<ChangeEvent<string>>)(evt => _fileName = evt.newValue));
            toolbar.Add(filenameTextField);

            toolbar.Add(new Button(() => RequestDataOperation(true)) { text = "Save Data" });
            toolbar.Add(new Button(() => RequestDataOperation(false)) { text = "Load Data" });


            rootVisualElement.Add(toolbar);
        }

        private void RequestDataOperation(bool save)
        {
            if (!string.IsNullOrEmpty(_fileName))
            {
                var saveUtility = GraphSaveUtility.GetInstance(_graphView);
                if (save)
                {
                    saveUtility.SaveGraph(_fileName);
                }
                else
                {
                    saveUtility.LoadGraph(_fileName);
                }

            }
            else
            {
                EditorUtility.DisplayDialog("Invalid file name!", "Enter a valid file name", "ok");
                return;
            }


        }

        private void ConstructGraphView()
        {
            _graphView = new DialogueGraphView(this) { name = "Dialogue Graph" };

            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);

        }


    }
}
