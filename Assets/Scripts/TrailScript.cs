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
            currTrail.transform.localScale += Vector3.up * playerMove.getSpeed();
        } else if (TryGetComponent(out EnemyMove enemyMove)) {
            currTrail.transform.position = trailPos + (transform.position - trailPos) / 2;
            currTrail.transform.localScale += Vector3.up * enemyMove.speed;
        }
        currRot = transform.rotation.eulerAngles.z;
    }

    private void SpawnTrail() {
        if (currTrail) {
            if (TryGetComponent(out PlayerMove playerMove)) {
                currTrail.transform.localScale -= Vector3.up * playerMove.getSpeed();
            } else if (TryGetComponent(out EnemyMove enemyMove)) {
                currTrail.transform.localScale -= Vector3.up * enemyMove.speed;
            }
        }
        currTrail = Instantiate(trailPrefab, transform.position, transform.rotation);
        StartCoroutine(plzwork());
        trailPos = transform.position;
        currTrail.GetComponent<SpriteRenderer>().color = trailColor;
    }
    private IEnumerator plzwork() {
        currTrail.layer = 2;
        yield return new WaitForSeconds(.25f);
        currTrail.layer = 0;
    }
}
