using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : Stats
{
    public Transform currentCheckpoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }
    private void OnTriggerEnter(Collider ThingWeStepInto)
    {
        if (ThingWeStepInto.gameObject.CompareTag("CheckPoint"))
        {
            currentCheckpoint = ThingWeStepInto.transform;
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue += 7;
            }
        }
    }
    private void OnTriggerExit(Collider ThingWeStepOutOf)
    {
        if (ThingWeStepOutOf.gameObject.CompareTag("CheckPoint"))
        {
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue -= 7;
            }
        }
    }
}
