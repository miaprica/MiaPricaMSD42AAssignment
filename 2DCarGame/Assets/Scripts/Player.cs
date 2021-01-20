using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 0.7f;
    [SerializeField] int health = 50;

    [SerializeField] AudioClip playerHealthReduceSound;
    [SerializeField] [Range(0, 1)] float playerHealthReduceVolume = 0.75f;

    [SerializeField] GameObject deathVFX;
    [SerializeField] float explosionDuration = 1f;

    [SerializeField] AudioClip playerDeathSound;
    [SerializeField] [Range(0, 1)] float playerDeathSoundVolume = 0.75f;

    float xMin, xMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Win();
    }

    public int GetHealth()
    {
        return health;
    }

    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        DamageDealer dmgDealer = otherObject.gameObject.GetComponent<DamageDealer>();
        
        if (!dmgDealer) //if its null
        {
            return;
        }

        ProcessHit(dmgDealer);
    }

    private void ProcessHit(DamageDealer dmgDealer)
    {
        health -= dmgDealer.GetDamage();
        int score = FindObjectOfType<GameSession>().GetScore();
        AudioSource.PlayClipAtPoint(playerHealthReduceSound, Camera.main.transform.position, playerHealthReduceVolume);

        if (health <= 0 && score < 100)  //task 2, question f)       
        {
            health = 0;
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(playerDeathSound, Camera.main.transform.position, playerDeathSoundVolume);
        Destroy(explosion, explosionDuration);
        FindObjectOfType<Level>().LoadGameOver();
    }


    private void Win()
    {
        int score = FindObjectOfType<GameSession>().GetScore();

        if (score >= 100)
        {
            score = 100;
            FindObjectOfType<Level>().LoadWinnerScreen();
        }
    }

    //moving the player car on x axis
    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var newXPos = transform.position.x + deltaX;
        newXPos = Mathf.Clamp(newXPos, xMin, xMax);
        this.transform.position = new Vector2(newXPos, transform.position.y);
    }

    //private void OnTriggerEnter2D(Collider2D otherObject)
    //{
      //  Destroy(otherObject.gameObject);
    //}
}
