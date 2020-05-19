using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SideScrollerProject
{
    public class WizardBoss : MonoBehaviour
    {
        // Start is called before the first frame update
        //
        public GameObject[] teleportables;
        public Status wizardStatus;
        public Slider bossSlider;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Teleport()
        {
            if (teleportables == null) return;
            this.transform.position = teleportables[Random.Range(0, teleportables.Length)].transform.position;

        }
        public void EnableBattle()
        {
            wizardStatus = this.GetComponent<Status>();
            wizardStatus.slider.slider = null;
            wizardStatus.slider.slider = bossSlider;
            bossSlider.gameObject.SetActive(true);
            wizardStatus.slider.SetMaxValue(wizardStatus.maxHealth);
        }

    }
}