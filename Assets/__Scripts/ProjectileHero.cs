using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class ProjectileHero : MonoBehaviour{
    private BoundsCheck bndCheck;

    void Awake(){
        bndCheck = GetComponent<BoundCheck>();
    }

    void Update (){
        if(bndCheck.LocIs(BoundsCheck.eScreenLocs.offUp)){
            Destroy(gameObject);
        }
    }
}