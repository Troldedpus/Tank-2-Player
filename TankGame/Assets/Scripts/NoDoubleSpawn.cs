using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDoubleSpawn : MonoBehaviour {

    public bool istrigger = false;

	// Use this for initialization
	void Start () {
        Collider2D collider = GetComponent<Collider2D>();
        Collider2D[] colliders = new Collider2D[1];
        ContactFilter2D contactFilter = new ContactFilter2D();

        int collidercount = collider.OverlapCollider(contactFilter, colliders);

        if (collidercount > 0)
            Destroy(gameObject);
		else
        {
            collider.isTrigger = istrigger;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
