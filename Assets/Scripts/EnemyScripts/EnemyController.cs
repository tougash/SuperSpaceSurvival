using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform Player;
    private Rigidbody e_Rigidbody;
    public int MoveSpeed = 4;
    int MinDist = 0;
    [SerializeField]bool attacking = false;
    public GameObject attackBox;
    public float attackTime = 0.75f;
    
    void Start()
    {
        // Find the player by tag (make sure your player has the tag "Player")
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        e_Rigidbody = gameObject.GetComponent<Rigidbody>();
        if (Player == null)
        {
            Debug.LogError("Player not found.");
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (PauseBehaviour.instance.GetIsPaused()) { return; }
        transform.LookAt(Player);
        if (Vector3.Distance(transform.position, Player.position) >= MinDist && !attacking)
        {
            e_Rigidbody.MovePosition(transform.position+ new Vector3(transform.forward.x, 0f, transform.forward.z ) * MoveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, Player.position) <= 1)
            {
                StartCoroutine("AttackPlayer");
            }
        }
    }

    IEnumerator AttackPlayer()
    {
        attacking = true;
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackTime);
        attackBox.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        attacking = false;
    }
}