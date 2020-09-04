using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class DialogueEventHandler : MonoBehaviour
{
    //prefab that handles Events that can be run in a scriptableobject to the scene
    //for example is enabling disabling some GO in the scene
    // this must contain must contain managers 
    //find?
    [SerializeField] List<GameObject> gameObjectsToEnableDisable = new List<GameObject>();
    [SerializeField] Vector3 parentPosition; // spawn place;
    public void InstantiateEachGameObjectAtPosition() => gameObjectsToEnableDisable.ForEach(x => Instantiate(x, parentPosition, Quaternion.identity));
    public void EnableDisableGameObjects(bool enable) => gameObjectsToEnableDisable.ForEach(x => x.gameObject.SetActive(enable));

}
