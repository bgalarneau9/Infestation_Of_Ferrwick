using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpellcasterController : MonoBehaviour
{
    private int health = 100;
    [SerializeField]
    private GameObject spellCasterPrefab;
    [SerializeField]
    private Transform _bloodSplatSpawn;
    [SerializeField]
    private GameObject _bloodSplatPrefab;

    public AudioClip deathsound;

    private AudioSource MyAudioSrc;

    void Start()
    {
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            health -= 25;
            if ( health <= 0)
            {
                GameObject bloodSplatter = Instantiate(_bloodSplatPrefab, _bloodSplatSpawn.position, Quaternion.identity);
                bloodSplatter.GetComponent<ParticleSystem>().Play();
                // 4 - Play Death sound
                //MyAudioSrc.Play();
                Destroy(spellCasterPrefab);
            }
        }
    }
}
