using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cat_Enum;
public class Cat_Zeus : Cat_Controller
{
    public Animator thunderAnimator;
    public Cat_ZeusRangeGetAttack catZeusRangeGetAttack;

    public override IEnumerator WaitForNextCatAttack()
    {
        while (true)
        {            
            if (catZeusRangeGetAttack.listCatControllers.Count > 0)
            {
                catAnimator.SetBool("Attack", true);            
                thunderAnimator.SetBool("Thunder", true);               
                homeTarget = null;                
                foreach(Cat_Controller _catTartget in catZeusRangeGetAttack.listCatControllers)
                {
                    _catTartget.CatGetDamage(amountDamage);
                }
            }
            if (homeTarget != null)
            {
                catAnimator.SetBool("Attack", true);            
                thunderAnimator.SetBool("Thunder", true);             
                homeTarget.HomeGetDamage(amountDamage);
            }
            
            catAnimator.speed = (catAttackSpeed < 1) ? 1 : catAttackSpeed;
            float _lengthAnim = (catAttackSpeed <= 1) ? catAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length / catAnimator.GetCurrentAnimatorStateInfo(0).speed
                       : (catAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.length / catAttackSpeed) / catAnimator.GetCurrentAnimatorStateInfo(0).speed;
            //Debug.LogError("_lengthAnim: " + _lengthAnim);
            yield return new WaitForSeconds(_lengthAnim);
            catAnimator.SetBool("Attack", false);      
            thunderAnimator.SetBool("Thunder", false);         
            float waitTime = (catDurationAttack / catAttackSpeed) - _lengthAnim;
            yield return new WaitForSeconds(waitTime);

            //Debug.LogError("waitTime: " + waitTime);
            yield return null;
        }
    }
    public override void CatAttack()
    {
        StopCatWalk();
        if (catZeusRangeGetAttack.listCatControllers.Count <= 0) return;
        if (catZeusRangeGetAttack.listCatControllers.Count > 0) homeTarget = null;
        StartCoroutine(catController.WaitForNextCatAttack());
    }
}
