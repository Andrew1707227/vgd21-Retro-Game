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
        RaycastHit2D[] rays = new RaycastHit2D[4];
        rays[0] = Physics2D.Raycast(transform.position,Vector2.up);
        rays[1] = Physics2D.Raycast(transform.position, Vector2.down);
        rays[2] = Physics2D.Raycast(transform.position, Vector2.left);
        rays[3] = Physics2D.Raycast(transform.position, Vector2.right);

        RaycastHit2D bigboi = rays[0];
        for (int i = 0; i < rays.Length; i++) {
            if (rays[i].distance > bigboi.distance) {
                bigboi = rays[i];
            }
        }
        if (bigboi.collider.tag == "Wall") {
            if (bigboi == rays[3] && rot != 90) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));

            }else if (bigboi == rays[2] && rot != 270) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

            } else if (bigboi == rays[0] && rot != 180) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

            } else if (bigboi == rays[1] && rot != 0) {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            }
        }
        rot = transform.rotation.eulerAngles.z;
        if (rot == 0) {
            transform.position += Vector3.up * speed;
        } else if (rot == 90) {
            transform.position += Vector3.left * speed;
        } else if (rot == 180) {
            transform.position += Vector3.down * speed;
        } else if (rot == 270) {
            transform.position += Vector3.right * speed;
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
        speed = 0;
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(.25f);
        Destroy(gameObject);
    }

}
