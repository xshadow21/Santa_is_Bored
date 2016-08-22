using UnityEngine;
using System.Collections;

public class windScript : MonoBehaviour {

    public GameObject girouette;
    public GameObject char1, char2, char3;
    public float fBlowForce;
    public float wind_multiplier;

    public float MAX_BLOW;

    public GameObject wind_particles;

    private Vector3 girouette_direction;
    private Vector3 wind_force;
    private GameObject head1, head2, head3;

    private GameManager game_man;

	// Use this for initialization
	void Start ()
    {
        fBlowForce = 0.0f;
        wind_particles = GameObject.Find("WindParticles");

        head1 = char1.transform.Find("Sphere").gameObject;
        head2 = char2.transform.Find("Sphere").gameObject;
        head3 = char3.transform.Find("Sphere").gameObject;

        game_man = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        girouette_direction = girouette.transform.right;
        wind_force = girouette_direction.normalized * fBlowForce * (wind_multiplier * (game_man.timer_duration + 3));

        char1.GetComponent<Rigidbody>().AddForceAtPosition(wind_force, head1.transform.position);
        char2.GetComponent<Rigidbody>().AddForceAtPosition(wind_force, head2.transform.position);
        char3.GetComponent<Rigidbody>().AddForceAtPosition(wind_force, head3.transform.position);
    }

    public void ApplyForce(float p_fBlowForce)
    {
        //AkSoundEngine.PostEvent("wind_start", gameObject);
        //GetComponent<AudioSource>().clip = AkSoundEngine.GetIDFromString("wind_start");
        //GetComponent<AudioSource>().volume = p_fBlowForce;
        //AK::SoundEngine::SetRTPCValue(L"RPM", (AkRtpcValue)nRPM);
        //AkSoundEngine.SetRTPCValue("AkVolumeValue", p_fBlowForce * 100);
        //AkSoundEngine.SetRTPCValue("AkPitchValue", p_fBlowForce*100);

        // wind speed on particules
        if (fBlowForce < p_fBlowForce && p_fBlowForce <= MAX_BLOW)
        {
            fBlowForce = p_fBlowForce;
            wind_particles.GetComponent<ParticleSystem>().playbackSpeed += fBlowForce;
        }
        else
        {
            wind_particles.GetComponent<ParticleSystem>().playbackSpeed = wind_particles.GetComponent<ParticleSystem>().playbackSpeed <= 1.0f ? 1.0f : wind_particles.GetComponent<ParticleSystem>().playbackSpeed - 0.2f;
            fBlowForce = p_fBlowForce;
        }
    }
}
