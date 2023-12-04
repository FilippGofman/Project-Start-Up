using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SuicideBomberScript : EnemyAI{

    [Header("Explosion Data")]
    [SerializeField] HpComponent hp;
    [SerializeField] float explosionScale;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask mask;
    [SerializeField] float bombFuse;

    [Header("Animator")]
    [SerializeField] Animator animator;

    protected override void Start(){
        base.Start();

        //hp = GetComponent<HpComponent>();
        hp.OnDeath += SpawnExplosion;
        canLeaveAttackState = false;
    }

    public void SpawnExplosion(){

        Vector3 position;
        RaycastHit hit;

        Physics.Raycast(transform.position, Vector3.down, out hit, 10, mask);
        position = hit.point + new Vector3(0, 1.15f, 0) * explosionScale;

        GameObject explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.transform.localScale = new Vector3(explosionScale, explosionScale, explosionScale);

        VisualEffect effect = explosion.GetComponent<VisualEffect>();

        effect.playRate = 1.75f;

        Destroy(transform.root.gameObject);

    }

    protected override void Attack(){
        //StartCoroutine(StartBombTimer());
        //lastAttackTime = Time.time;
    }

    IEnumerator StartBombTimer() {

        int frames = 120;
        float frameDuration = bombFuse / frames;

        for (int i = 0; i < frames; i++) {
            yield return new WaitForSeconds(frameDuration);
        }

        //yield return new WaitForSeconds(bombFuse);
        SpawnExplosion();
        //Destroy(transform.root.gameObject);

    }

    protected override void PlayAttackAnimation(){
        animator.SetBool("Rolling", false);
    }

    protected override void PlayChaseAnimation(){
        animator.SetBool("Rolling", true);
    }

}
