using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

public class Grenade : MonoBehaviour, IEnemyWeapon
{
    [TitleGroup("Grenade Settings")]
    [SerializeField]
    [MinValue(0)]
    [Tooltip("In seconds")]
    private float timeToExplode;
    [SerializeField]
    [MinValue(0)]
    [Tooltip("Blast Radius")]
    private float blastRadius;
    [SerializeField]
    [MinValue(0)]
    [Tooltip("Blast Damage")]
    private float blastDamage;
    [SerializeField]
    [MinMaxSlider(0,1,true)]
    [Tooltip("Opacity of the blast zone in percent")]
    private Vector2 blastIndicatorOpacityRange;

    [TitleGroup("Shrapnel Settings", "Only Change on objects that shoot shrapnel")]
    [SerializeField]
    [Min(0f)]
    private float shrapnelForce;
    [SerializeField]
    private GameObject shrapnel;
    [SerializeField]
    [MinMaxSlider(1,36,true)]
    private Vector2Int shrapnelCountRange;
    [SerializeField]
    [MinMaxSlider(0,360,true)]
    private Vector2Int shrapnelDegreeOffsetRange;
    [SerializeField]
    [MinMaxSlider(0f,10f,true)]
    private Vector2 shrapnelMoveDuration;
    [Button]
    public void ActivateAttack(){
        Attack();
    }

    public Rigidbody2D RB {get; private set;}
    public string Name { get; set;}
    private bool inProgress = false;
    private Transform blastZoneTransform;
    private SpriteRenderer blastZoneSpriteRenderer;
    private DetectPlayerInCollider blastZoneDetectInCollider;
    private int shrapnelCount;
    private List<GameObject> shrapnels;

    private void Awake()
    {
        blastZoneDetectInCollider = GetComponentInChildren<DetectPlayerInCollider>();
        blastZoneSpriteRenderer = blastZoneDetectInCollider.GetComponent<SpriteRenderer>();
        blastZoneTransform = blastZoneDetectInCollider.GetComponent<Transform>();
        RB = GetComponent<Rigidbody2D>();

        blastZoneTransform.localScale = Vector3.one * blastRadius;
        Color initialColor = blastZoneSpriteRenderer.color;
        initialColor.a = 0f;
        blastZoneSpriteRenderer.color = initialColor;
        if(shrapnel != null){
            CreateShrapnels();
        }
    }

    private void CreateShrapnels()
    {
        shrapnels = new();
        shrapnelCount = Random.Range(shrapnelCountRange.x, shrapnelCountRange.y);
        int shrapnelDegreeOffset = Random.Range(shrapnelDegreeOffsetRange.x, shrapnelDegreeOffsetRange.y);
        for (int i = 0; i < shrapnelCount; i++)
        {
            Debug.Log("Adding shrapnel");
            Quaternion initialQuart = Quaternion.AngleAxis(i * 360 / (float)shrapnelCount + shrapnelDegreeOffset, Vector3.forward);
            shrapnels.Add(Instantiate(shrapnel, transform.position, initialQuart, transform));
        }
    }

    public void Attack()
    {
        if (!inProgress) {
            inProgress = true;
            StartCoroutine(Explode());
        }
    }

    private IEnumerator Explode(){
        StartCoroutine(BlastIndicatorFX());
        yield return new WaitForSeconds(timeToExplode);
        Debug.Log("Boom!");
        if(blastZoneDetectInCollider.IsPlayerInCollider){
            PlayerHandler.Instance.TakeDamage(blastDamage);
        }
        blastZoneTransform.gameObject.SetActive(false);
        if(shrapnels != null) {
            Debug.Log("Managing shrapnels");
            foreach (var s in shrapnels)
            {
                StartCoroutine(ProjectShrapnel(s));
            }
        }
    }

    private IEnumerator BlastIndicatorFX(){
        float rate = Time.deltaTime * (blastIndicatorOpacityRange.y - blastIndicatorOpacityRange.x) / timeToExplode;
        Color biColor = blastZoneSpriteRenderer.color;
        while(blastZoneTransform.gameObject.activeSelf) {
            yield return new WaitForSeconds(Time.deltaTime);
            biColor.a += rate;
            Mathf.Clamp(biColor.a, 0f,1f);
            blastZoneSpriteRenderer.color = biColor;
        }
    }

    private IEnumerator ProjectShrapnel(GameObject s){
        Grenade shrapnelGrenade = s.GetComponent<Grenade>();
        shrapnelGrenade.RB.AddForce(s.transform.up  * shrapnelForce, ForceMode2D.Impulse);
        float moveDuration = Random.Range(shrapnelMoveDuration.x, shrapnelMoveDuration.y);
        yield return new WaitForSeconds(moveDuration);
        StartCoroutine(shrapnelGrenade.Explode());
    }
}
