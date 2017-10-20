using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int level = 1;
    public int exp = 0;
    public int needExp = 1;
    public float immortalTime = 3.0f;
    public float curTime = 0.0f;
    bool immortality = false;

	// Use this for initialization
	void Start () {

        needExp = level * 3;

	}
	
	// Update is called once per frame
	void Update () {
		
        if(exp >= needExp)
        {
            LevelUp();
        }

        if(immortality)
        {
            Immortal();
        }
	}

    void LevelUp()
    {
        exp = 0;
        level++;
        Resize();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(immortality)
        {
            return;
        }

        Slime temp = hit.gameObject.GetComponent<Slime>();
        if(temp != null)
        {
            
            if(level > temp.level)
            {
                temp.slimeState = Slime.SLIMESTATE.DEAD;
                exp += temp.level;

            }
            else if( level == temp.level)
            {
                if(exp >= temp.exp)
                {
                    temp.slimeState = Slime.SLIMESTATE.DEAD;
                    exp += temp.level;
                }
            }
            else
            {
                temp.exp += level;
                Dead();
            }
            

        }
    }
    void Dead()
    {
        level = 1;
        exp = 0;
        Resize();
        Immortal();
    }

    void Resize()
    {
        needExp = level * 3;
        transform.localScale = new Vector3(level, level, level);
    }

    void Immortal()
    {
        immortality = true;

        curTime += Time.deltaTime;

        if (curTime> immortalTime)
        {
            curTime = 0.0f;
            immortality = false;
        }
        

    }
}
