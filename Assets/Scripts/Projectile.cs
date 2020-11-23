using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    MeshRenderer mesh;
    float displacementAmount;
    Animator anim;

    private void Start() {
        mesh = GetComponent<MeshRenderer>();
        anim = GetComponent<Animator>();    
    }
    private void Awake() {
        Spawn();
    }


    private void Spawn() {
        displacementAmount = -1;
    }

    // Update is called once per frame
    void Update() {
        displacementAmount = Mathf.Lerp(displacementAmount, 0.1f, Time.deltaTime);
        mesh.material.SetFloat("_Amount", displacementAmount);

        
    }
}
