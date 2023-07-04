using System;
using UnityEngine;
using UnityEngine.UI;

//all things you can kill
public class Attributes : MonoBehaviour
{
    #region Structs
    [Serializable]
    public struct Attribute
    {
        //the name of the attribute
        public string name;
        //the current value of the attribute eg 10
        public float currentValue;
        //the maximum value of the attribute eg 100
        public float maxValue;
        //the regen value eg heal over time or regen from spell or potion
        public float regenValue;
        //the bar that displays this fill amount eg health bar
        public Image displayImage;
    }
    #endregion
    #region Variables
    // start with 3 attributes eg health, stamina and mana
    public Attribute[] attributes = new Attribute[3];
    public bool isDamaged;
    public bool canHeal;
    public bool isUnAlived;
    public float healDelayTimer;
    #endregion
    public virtual void RegenOverTime(int attributeIndex)
    {
        //regen chosen attribute by its regen amount over time
        attributes[attributeIndex].currentValue += Time.deltaTime * attributes[attributeIndex].regenValue;
    }
    public virtual void Damage(float damage)
    {
        //will use this to trigger players screen to flash red later
        isDamaged = true;
        //reduce the health by the amount we are damaged
        attributes[0].currentValue -= damage;
        //delay any healing regen
        canHeal = false;
        healDelayTimer = 0;
        //check if we should be unalived and set unalived if needed
        if (attributes[0].currentValue <= 0 && !isUnAlived)
        {
            UnAlived();
        }
    }
    public virtual void UnAlived()
    {
        //we are unalived
        isUnAlived = true;
    }
    public virtual void SetHealth()
    {
        attributes[0].displayImage.fillAmount = Mathf.Clamp01(attributes[0].currentValue / attributes[0].maxValue);
    }
    public virtual void SetMana()
    {
        attributes[1].displayImage.fillAmount = Mathf.Clamp01(attributes[1].currentValue / attributes[1].maxValue);
    }
    public virtual void SetStamina()
    {
        attributes[2].displayImage.fillAmount = Mathf.Clamp01(attributes[2].currentValue / attributes[2].maxValue);
    }
    public virtual void Update()
    {
        #region Attributes Display
        SetHealth();
        SetMana();
        SetStamina();
        #endregion
        #region Can Heal
        //if we cant heal
        if (!canHeal)
        {
            //our heal delay timer goes up
            healDelayTimer += Time.deltaTime;
            //when our heal delay timer hits the required amount of time
            if (healDelayTimer >= 5)
            {
                //we can heal again
                canHeal = true;
            }
        }
        //if we can heal, and are injured but not unalived at 0
        if (canHeal && attributes[0].currentValue < attributes[0].maxValue && attributes[0].currentValue > 0)
        {
            //trigger our regen over time on our health attribute
            RegenOverTime(0);
        }
        #endregion
    }
}
