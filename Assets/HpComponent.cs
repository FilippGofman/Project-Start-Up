using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HpComponent : MonoBehaviourWithPause{

    [SerializeField] float maxHp;
    public float currentHp { get; private set; }

    public event Action<float, float> OnDamageTaken;
    public event Action OnDeath;

    private void Start(){
        currentHp = maxHp;
        OnDamageTaken?.Invoke(currentHp, maxHp);
    }

    public void TakeDamage(float pDamage) {

        currentHp = Mathf.Max(0,currentHp-pDamage);
        OnDamageTaken?.Invoke(currentHp, maxHp);
        if (currentHp == 0) {
            OnDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    public void RestoreHp(float pHp) {
        currentHp = Mathf.Min(currentHp + pHp, maxHp);
    }

}
