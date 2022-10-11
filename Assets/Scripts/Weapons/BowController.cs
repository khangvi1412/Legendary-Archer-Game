using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class BowController : MonoBehaviour
{
    [System.Serializable]
    public class BowSettings
    {
        [Header("Arrow Settings")]
        public Rigidbody arrowPrefab;
        public Transform arrowPos;
        public Transform arrowEquipParent;
        public float arrowForce = 10;

        [Header("Bow String Settings")]
        public Transform bowString;
        public Transform stringInitialPos;
        public Transform stringHandPullPos;
        public Transform stringInitialParent;

        [Header("Bow Audio Settings")]
        //public AudioClip pullStringAudio;
        public AudioClip releaseStringAudio;
        public AudioClip drawArrowAudio;
    }
    [SerializeField]
    public BowSettings bowSettings;
    Rigidbody currentArrow;

    AudioSource bowAudio;
    public GameObject arrowScript;
    ArrowController arrowController;
    Cinemachine.CinemachineImpulseSource source;
    public GameObject informationPlayer;
    private PlayerInformation player;
    
    // Start is called before the first frame update
    void Start()
    {
        bowAudio = GetComponent<AudioSource>();
        player = informationPlayer.GetComponent<PlayerInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickArrow()
    {
        bowAudio.PlayOneShot(bowSettings.drawArrowAudio);
        bowSettings.arrowPos.gameObject.SetActive(true);
    }

    public void DisableArrow()
    {
        bowSettings.arrowPos.gameObject.SetActive(false);
    }

    public void PullString()
    {
        bowSettings.bowString.transform.position = bowSettings.stringHandPullPos.position;
        bowSettings.bowString.transform.parent = bowSettings.stringHandPullPos;
    }

    public void ReleaseString()
    {
        bowSettings.bowString.transform.position = bowSettings.stringInitialPos.position;
        bowSettings.bowString.transform.parent = bowSettings.stringInitialParent;
    }
    public void PullAudio()
    {
        //bowAudio.PlayOneShot(bowSettings.pullStringAudio);
    }

    public void Fire(Vector3 target, bool didHit)
    {
        
        bowAudio.PlayOneShot(bowSettings.releaseStringAudio);
        Vector3 dir = target - bowSettings.arrowPos.position;
        currentArrow = Instantiate(bowSettings.arrowPrefab, bowSettings.arrowPos.position, bowSettings.arrowPos.rotation);
        arrowController = currentArrow.GetComponent<ArrowController>();
        arrowController.target = target;
        arrowController.hit = didHit;
        arrowController.player = player;
        currentArrow.AddForce(dir * bowSettings.arrowForce, ForceMode.Force);
        source = GetComponent<Cinemachine.CinemachineImpulseSource>();
        source.GenerateImpulse(Camera.main.transform.forward);

    }
    
}
