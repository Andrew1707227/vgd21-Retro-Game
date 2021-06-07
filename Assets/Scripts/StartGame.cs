using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    private GameObject player;
    public GameObject[] enemies;

    public AudioClip start;
    public AudioClip win;

    private AudioSource Asource;
    private bool debounce;

    void Start() {
        Asource = GetComponent<AudioSource>();
        Asource.PlayOneShot(start);
        player = GameObject.Find("Player");
        debounce = false;
    }

    void Update() {
        player.GetComponent<PlayerMove>().enabled = !Asource.isPlaying;
        player.GetComponent<TrailScript>().enabled = !Asource.isPlaying;
        if (Asource.isPlaying) {
            GameObject.Find("AccelerateSFX").GetComponent<AudioSource>().Stop();
        }
        int enemiesLeft = enemies.Length;
        for (int i = 0; i < enemies.Length; i++) {
            if (enemies[i]) {
                enemies[i].GetComponent<EnemyMove>().enabled = !Asource.isPlaying;
                enemies[i].GetComponent<TrailScript>().enabled = !Asource.isPlaying;
            } else {
                enemiesLeft--;
            }
        }
        if ((enemiesLeft == 0 || !player.GetComponent<SpriteRenderer>().enabled) && !debounce) {
            debounce = true;
            StartCoroutine(RestartGame());
        }
    }

    private IEnumerator RestartGame() {
        GameObject.Find("AccelerateSFX").GetComponent<AudioSource>().Stop();
        Asource.PlayOneShot(win);
        yield return new WaitUntil(() => !Asource.isPlaying);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
