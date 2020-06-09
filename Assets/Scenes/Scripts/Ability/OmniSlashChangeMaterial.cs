using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniSlashChangeMaterial : MonoBehaviour
{
   SpriteRenderer omniRenderer;
   public Material[] materials;
   public int materialIndex = 0;
    void Start()
    {
        omniRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        omniRenderer.material = materials[materialIndex];
    }
}
