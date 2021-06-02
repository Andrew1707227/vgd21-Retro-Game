using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour {
    public GameObject trailPrefab;
    public Color trailColor;

    private GameObject currTrail;
    private float currRot;
    private Vector3 trailPos;

    void Start() {
        currRot = 0;
        SpawnTrail();
    }

    void FixedUpdate() {
        if (currRot != transform.rotation.eulerAngles.z) {
            SpawnTrail();
        }
        if (TryGetComponent(out PlayerMove playerMove)) {
            currTrail.transform.position = trailPos + (transform.position - trailPos) / 2;
            currTrail.transform.localScale += Vector3.up * GetComponent<PlayerMove>().getSpeed();
        } else {
            //currTrail.transform.localScale += Vector3.up * GetComponent<EnemyMove>().getSpeed();
        }
        currRot = transform.rotation.eulerAngles.z;
    }

    private void SpawnTrail() {
        if (currTrail) {
            currTrail.transform.localScale -= Vector3.up * GetComponent<PlayerMove>().getSpeed();
        }
        currTrail = Instantiate(trailPrefab, transform.position, transform.rotation);
        trailPos = transform.position;
        currTrail.GetComponent<SpriteRenderer>().color = trailColor;
    }
}
