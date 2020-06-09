﻿using UnityEngine;
using TMPro;
using FMODUnity;
using System.Collections;

public class Gun : MonoBehaviour {
    public float range = 100f;
    public GameObject gun;

    public float currentTime;

    public float totalShifts = 2;
    public float currentShifts;
    int interval = 1;
    float nextTime = 0;


    public TextMeshPro tmp;

    public GameObject firePoint;
    public GameObject fireParticle;


    public StudioEventEmitter soundEvent;

    // Update is called once per frame
    void Start() {
        currentShifts = totalShifts;

    }
    void Update() {

    }

    private void FixedUpdate() {

        clockTicks();


        updateParticle();


    }

    public void clockTicks() {
        if (Time.time >= nextTime) {


            tmp.text = currentTime.ToString();
            nextTime += interval;

        }

    }

    public void lightUpWires() {

    }

    public void updateParticle() {

        if (fireParticle.activeSelf) {
            fireParticle.transform.position += firePoint.transform.forward / 2;
            Invoke("ResetParticle", 1.0f);

        }
    }

    void ResetParticle() {
       fireParticle.transform.position = firePoint.transform.position;
       fireParticle.SetActive(false);
    }

    public void Shoot() {
        soundEvent.Play();
        fireParticle.SetActive(true);
        FreezeMonster();
        ResetTime();
    }

    void FreezeMonster() {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 13;

        Vector3 src = firePoint.transform.position;
        Vector3 dest = firePoint.transform.forward;

        RaycastHit Hit;
        if (Physics.Raycast(src, dest, out Hit, range, layerMask)) {
            Debug.DrawRay(src, dest * Hit.distance, Color.white);
            Debug.Log(Hit.collider.tag);
            Hit.collider.gameObject.GetComponent<MonsterBehaviour>()?.Freeze();
        } else {
            Debug.DrawRay(src, dest * range, Color.white);
        }
    }

    void ResetTime() {

    }
}
