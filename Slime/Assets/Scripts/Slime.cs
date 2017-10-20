using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour {

    public int level = 1;
    public int exp = 0;
    public int needExp = 2;
    public float rotateSpeed = 10.0f;
    public Transform target;

    public enum SLIMESTATE
    {
        NONE = -1,

        IDLE = 0,
        MOVE,
        LEVELUP,
        DEAD,
    }

    public SLIMESTATE slimeState = SLIMESTATE.IDLE;
    CharacterController characterController = null;

    Dictionary<SLIMESTATE, System.Action> dicState = new Dictionary<SLIMESTATE, System.Action>();
	// Use this for initialization
	void Start () {


        dicState[SLIMESTATE.NONE] = None;
        dicState[SLIMESTATE.IDLE] = Idle;
        dicState[SLIMESTATE.MOVE] = Move;
        dicState[SLIMESTATE.LEVELUP] = LevelUp;
        dicState[SLIMESTATE.DEAD] = Dead;


        characterController = GetComponent<CharacterController>();
        level = selectLevel();
        needExp = level * 2;

        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * level;
    }
	
	// Update is called once per frame
	void Update () {

        if( exp >= needExp)
        {
            slimeState = SLIMESTATE.LEVELUP;
        }

        dicState[slimeState]();

	}

    int selectLevel()
    {
        int level;
        int temp = Random.Range(0, 100);

        if(temp < 50)
        {
            level = 1;
        }
        else if( temp < 75)
        {
            level = 2;
        }
        else if( temp  < 87)
        {
            level = 3;
        }
        else if( temp < 93)
        {
            level = 4;
        }
        else
        {
            level = 5;
        }



        return level;
    }

    void None()
    {

    }
    void Idle()
    {
        if(Search())
        {
            slimeState = SLIMESTATE.MOVE;
        }
    }
    void Move()
    {
        if(target == null)
        {
            slimeState = SLIMESTATE.IDLE;
        }
        Vector3 moveDirection = target.transform.position - transform.position;
        moveDirection.Normalize();
        characterController.Move(moveDirection * level * Time.deltaTime);
    }
    void LevelUp()
    {
        exp = 0;
        level++;
        needExp = level * 2;
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * level;

        slimeState = SLIMESTATE.IDLE;
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.layer == 9)
        {

            Slime temp = hit.gameObject.GetComponent<Slime>();
            if(temp != null)
            {
                if(level >= temp.level)
                {
                    exp += temp.level;
                    temp.slimeState = SLIMESTATE.DEAD;
                }
                else
                {
                    temp.exp += level;
                    slimeState = SLIMESTATE.DEAD;
                }
            }
        }
    }

    bool Search()
    {
        Vector3 y = new Vector3(0.0f, 1.0f, 0.0f);
        transform.Rotate(y, rotateSpeed * Time.deltaTime);

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;
        if(Physics.Raycast(transform.position,fwd,out hit,20))
        {
            //Debug.LogWarning(hit.transform.name, hit.transform.gameObject);
            if (hit.transform.CompareTag("Slime"))
            {
                target = hit.transform;
                return true;
            }
        }

        return false;
    }
}

