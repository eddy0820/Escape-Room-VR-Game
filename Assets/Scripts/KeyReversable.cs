using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class KeyReversable : MonoBehaviour, IReversable
{
    [Header("Key Objects")]

    [SerializeField] GameObject fixedKey;
    [SerializeField] GameObject keyPiece1;
    [SerializeField] GameObject keyPiece2;
    XRGrabInteractable keyPiece1Interactable;
    XRGrabInteractable keyPiece2Interactable;

    [Header("Overlap Sphere")]
    [SerializeField] float sphereCheckRadius = 5;
    [SerializeField] Color sphereCheckColor;

    [Space(10)]
    [SerializeField] InteractionLayerMask defaultInteractionLayer;
    [SerializeField] InteractionLayerMask targetInteractionLayer;
    [SerializeField] LayerMask key2LayerMask;

    [Space(10)]
    [SerializeField] ShakeSettings shakeSettings;

    [Space(10)]
    [ReadOnly, SerializeField] bool activated;

    private float _timer;
    private Vector3 _randomPos;
    private Vector3 _randomPos2;

    private void Awake()
    {
        keyPiece1Interactable = keyPiece1.GetComponent<XRGrabInteractable>();  
        keyPiece2Interactable = keyPiece2.GetComponent<XRGrabInteractable>();
    }

    private void Update()
    {
        if(Physics.CheckSphere(keyPiece1.transform.GetChild(0).position, sphereCheckRadius, key2LayerMask))
        {
            keyPiece1Interactable.interactionLayers = targetInteractionLayer;
            keyPiece2Interactable.interactionLayers = targetInteractionLayer;
        }
        else
        {
            keyPiece1Interactable.interactionLayers = defaultInteractionLayer;
            keyPiece2Interactable.interactionLayers = defaultInteractionLayer;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = sphereCheckColor;
        Gizmos.DrawWireSphere(keyPiece1.transform.GetChild(0).position, sphereCheckRadius);
    }

    public void DoRewind()
    {
        if(!activated)
        {
            Begin();
            activated = true;
        }
    }

    public void SetAsCurrentTimeReversalObject()
    {
        TimeManager.Instance.SetCurrentTimeReversalObject(gameObject);
    }

    public void UnsetAsCurrentTimeReversalObject()
    {
        TimeManager.Instance.SetCurrentTimeReversalObject(null);
    }
 
   private void OnValidate()
   {
       if (shakeSettings._delayBetweenShakes > shakeSettings._time)
           shakeSettings._delayBetweenShakes = shakeSettings._time;
   }
 
   public void Begin()
   {
       StopAllCoroutines();
       StartCoroutine(ShakeAndRise());
   }
 
   private IEnumerator ShakeAndRise()
   {
        _timer = 0f;
        keyPiece1.GetComponent<Rigidbody>().isKinematic = true;
        keyPiece2.GetComponent<Rigidbody>().isKinematic = true;

        Vector3 _anchorPos = keyPiece1.transform.position;
        Vector3 _anchorPos2 = keyPiece2.transform.position;
 
        while (_timer < shakeSettings._time)
        {
            _timer += Time.deltaTime;

            _randomPos = _anchorPos + (Random.insideUnitSphere * shakeSettings._distance);
            _randomPos2 = _anchorPos2 + (Random.insideUnitSphere * shakeSettings._distance);

            keyPiece1.transform.position = _randomPos;
            keyPiece2.transform.position = _randomPos2;
            
            keyPiece1.transform.position = new Vector3(keyPiece1.transform.position.x, keyPiece1.transform.position.y + shakeSettings._riseStepAmount, keyPiece1.transform.position.z);
            keyPiece2.transform.position = new Vector3(keyPiece2.transform.position.x, keyPiece2.transform.position.y + shakeSettings._riseStepAmount, keyPiece2.transform.position.z);

            _anchorPos = keyPiece1.transform.position;
            _anchorPos2 = keyPiece2.transform.position;

            if (shakeSettings._delayBetweenShakes > 0f)
            {
                yield return new WaitForSeconds(shakeSettings._delayBetweenShakes);
            }
            else
            {
                yield return null;
            }
        }

        keyPiece1.GetComponent<Rigidbody>().isKinematic = false;
        keyPiece2.GetComponent<Rigidbody>().isKinematic = false;

        ReplaceWithNewKey();

        yield break;
    }

    public void ReplaceWithNewKey()
    {
        fixedKey.SetActive(true);
        fixedKey.transform.position = keyPiece1.transform.position;

        keyPiece1.gameObject.SetActive(false);
        keyPiece2.gameObject.SetActive(false);

    }

    [System.Serializable]
    public class ShakeSettings
    {
        [Range(0f, 2f)]
        public float _time = 0.2f;
        [Range(0f, 2f)]
        public float _distance = 0.1f;
        [Range(0f, 0.1f)]
        public float _delayBetweenShakes = 0f;

        public float _riseStepAmount = 0.05f;
    }
}


