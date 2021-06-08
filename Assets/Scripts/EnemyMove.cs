using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour {

    public float speed;
    private bool debounce;

    void Start() {
        debounce = false;
    }

    void FixedUpdate() {
        float rot = transform.rotation.eulerAngles.z;
        RaycastHit2D ray = Physics2D.Raycast(transform.position,Vector2.zero);
        if (rot == 0) {
            transform.position += Vector3.up * speed;
            ray = Physics2D.Raycast(transform.position, Vector3.up);
            float rad = Random.value;
            if (ray.distance < 1 && rad < .5f) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            } else if (ray.distance < 1) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
            }
        } else if (rot == 90) {
            transform.position += Vector3.left * speed;
            float rad = Random.value;
            if (ray.distance < 1 && rad < .5f) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            } else if (ray.distance < 1) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        } else if (rot == 180) {
            transform.position += Vector3.down * speed;
            ray = Physics2D.Raycast(transform.position, Vector3.down);
            float rad = Random.value;
            if (ray.distance < 1 && rad < .5f) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
            } else if (ray.distance < 1) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            }
        } else if (rot == 270) {
            transform.position += Vector3.right * speed;
            ray = Physics2D.Raycast(transform.position, Vector3.right);
            float rad = Random.value;
            if (ray.distance < 1 && rad < .5f) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            } else if (ray.distance < 1) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Wall" && !debounce) {
            debounce = true;
            StartCoroutine(killPlayer());
        }
    }

    private IEnumerator killPlayer() {
        GetComponent<Animator>().enabled = true;
        GetComponent<AudioSource>().Play();
        speed = 0;
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }

}
