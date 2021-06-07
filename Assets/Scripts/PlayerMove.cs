using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour {

    public float minSpeed;
    private float currSpeed;
    public float maxSpeed;
    public float accler;

    private bool debounce;

    public AudioSource Asource;

    void Start() {
        currSpeed = minSpeed;
        debounce = false;
        Asource.Play();
    }

    void FixedUpdate() {
        float rot = transform.rotation.eulerAngles.z;
        if (Input.GetAxis("Horizontal") == 1 && rot != 90) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
            if (currSpeed < maxSpeed) {
                currSpeed += accler;
            }
        } else if (Input.GetAxis("Horizontal") == -1 && rot != 270) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
            if (currSpeed < maxSpeed) {
                currSpeed += accler;
            } 
        } else if (Input.GetAxis("Vertical") == 1 && rot != 180) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            if (currSpeed < maxSpeed) {
                currSpeed += accler;
            } 
        }else if (Input.GetAxis("Vertical") == -1 && rot != 0) {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            if (currSpeed < maxSpeed) {
                currSpeed += accler;
            } 
        } else if (currSpeed > minSpeed) {
            currSpeed -= accler;
        }
        if (rot != transform.rotation.eulerAngles.z) {
            Asource.Play();
        }
        rot = transform.rotation.eulerAngles.z;
        if (rot == 0) {
            transform.position += Vector3.up * currSpeed;
        } else if (rot == 90) {
            transform.position += Vector3.left * currSpeed;
        } else if (rot == 180) {
            transform.position += Vector3.down * currSpeed;
        } else if (rot == 270) {
            transform.position += Vector3.right * currSpeed;
        }
    }
    public float getSpeed() {
        return currSpeed;
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
        maxSpeed = 0;
        minSpeed = 0;
        currSpeed = 0;
        yield return new WaitForSeconds(.25f);
        GetComponent<SpriteRenderer>().enabled = false;
        Asource.enabled = false;
    }

}
