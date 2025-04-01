using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 7;
    [SerializeField] private float _turnSpeed = 1440;
    [SerializeField] private Animator roboController;
    private Vector3 _input;
    public Transform model;
    public PlayerStats stats;
    public PlayerHealthController health;
    List<Ability> abilities;
    Ability active1;
    public TMP_Text abilityTimer;
    public Sprite[] allIcons;

    public GameObject grenade;

    void Start()
    {
        abilities = new List<Ability>();
    }

    void Update() 
    {
        if (PauseBehaviour.instance.GetIsPaused()) { return; }
        // Gather input and change rotation once per frame
        GatherInput();
        Look();
        int num = UpgradeManager.instance.playerAbilities.Except(abilities).ToArray().Length;
        if( num > 0 && UpgradeManager.instance.playerAbilities.Count > 0)
        {
            Ability newAbility = UpgradeManager.instance.playerAbilities.Last();
            if(newAbility.isPassive)
            {
                InvokeAbility(newAbility);
            }
            else
            {
                active1 = newAbility;
                abilityTimer.transform.Find("Icon").gameObject.SetActive(true);
                Image icon = abilityTimer.transform.Find("Icon").GetComponent<Image>();
                icon.overrideSprite = allIcons[(int)active1.type];
            }
            abilities = new List<Ability>(UpgradeManager.instance.playerAbilities);
        }
    }
    
    void FixedUpdate() 
    {
        if (PauseBehaviour.instance.GetIsPaused()) { return; }
        // Place Physics related functions in fixed update so they can trigger as many times per frame as necessary
        Move();
    }

    void GatherInput()
    {
        // Get input from input axis
        _input = new Vector3(Input.GetAxisRaw("Horizontal"),0,Input.GetAxisRaw("Vertical"));
        if(Input.GetKeyDown(KeyCode.Q) && active1 != null)
        {
            InvokeAbility(active1);
        }
    }

    void Look()
{
    // Raycast from the mouse position in world space
    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out var hit, 100, LayerMask.GetMask("Ground")))
    {
        // Get the mouse position on the ground (ignoring the Y value)
        var destPoint = hit.point;
        var target = new Vector3(destPoint.x, transform.position.y, destPoint.z);
        
        // Rotate the model to face the mouse
        model.transform.LookAt(target);
    }
}

void Move()
{
    // Move the character based on the input direction
    Vector3 moveDirection = new Vector3(_input.x, 0, _input.z).normalized;

    if (moveDirection != Vector3.zero)
    {
        // Create a matrix to offset input direction for isometric movement
        var matrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        var skewedInput = matrix.MultiplyPoint3x4(moveDirection);

        // Find the relative angle between input and current rotations
        var relative = (transform.position + skewedInput) - transform.position;

        // Rotate the player towards the input direction, independent of looking at the mouse
        var rot = Quaternion.LookRotation(relative, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);

        // Move the player forward in the walking direction
        _rb.MovePosition(transform.position + transform.forward * _speed * Time.deltaTime);
    }

    // Keep the player at the correct Y position (if needed for level design)
    if (_rb.transform.position.y != 1)
    {
        Vector3 fixedPos = new Vector3(_rb.transform.position.x, 1, _rb.transform.position.z);
        _rb.MovePosition(fixedPos);
    }
}

    void InvokeAbility(Ability ability)
    {
        ability.effect(stats, this);
    }

    public void updateCurrentSpeed()
    {
        _speed = 3+stats.getSpeedMod();
    }

    public void Intagible()
    {
        StartCoroutine("Ghost"); 
    }

    public void ThrowGrenade()
    {
        GameObject tmp = Instantiate(grenade, transform.position, transform.rotation);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast (ray, out var hit, 100, LayerMask.GetMask("Ground"))) 
        {
            Vector3 offsetToMouse = hit.point-tmp.transform.position;
            Vector3 fixedVel = new Vector3(Mathf.Clamp(offsetToMouse.x, -5, 5), 0, Mathf.Clamp(offsetToMouse.z, -5, 5));
            tmp.GetComponent<Rigidbody>().velocity = fixedVel;
        }
    }

    IEnumerator Ghost()
    {
        Debug.Log("start");
        float remainingTime = 10;
        gameObject.GetComponent<Collider>().enabled = false;
        Image icon = abilityTimer.transform.Find("Icon").GetComponent<Image>();
        var temp = icon.color;
        temp.a = 0.5f;
        icon.color = temp;
        while(remainingTime > 0)
        {
            abilityTimer.SetText(remainingTime.ToString());
            yield return new WaitForSeconds(1);
            remainingTime-=1;
        }
        abilityTimer.SetText("");
        Debug.Log("finished");
        gameObject.GetComponent<Collider>().enabled = true;
        temp.a = 1f;
        icon.color = temp;
    }
}
