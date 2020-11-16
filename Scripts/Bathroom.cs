using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bathroom : MonoBehaviour
{
    //gameobjects and audio
    #region Declaration
    public GameObject m;
    public GameObject wall;
    public AudioClip bell;
    public AudioClip bells;
    public AudioClip crying;
    public GameObject flickerL;
    public GameObject flicker2;
    public GameObject Light3;
    public GameObject door;
    public GameObject door2;
    public AudioClip doorshut;
    public AudioClip lightoff;
    public AudioClip lighton;
    public AudioSource source;

    public List <TextMeshProUGUI> Voices;
    #endregion
    // Start is called before the first frame update
    //text fading
    void Start()
    {
        
        foreach (var text in Voices)
        {
            text.alpha = 0;
        }
        m.SetActive(false);
        flickerL.GetComponentInChildren<FlickeringLight>().enabled = false;
        flicker2.GetComponentInChildren<FlickeringLight>().enabled = false;
    }


    #region StartingCoroutines
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(EnterEvent());
          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {

            this.gameObject.GetComponent<Collider>().enabled = false;
            StartCoroutine(ExitEvent());
           
        }
    }
    #endregion
    #region Enumerations
    //Door closes behind player and plays event
    IEnumerator EnterEvent()
    {

        door.GetComponent<Door>().enabled = false;
        yield return new WaitForSeconds(3f);
        door.transform.eulerAngles = new Vector3(door.transform.eulerAngles.x, door.transform.eulerAngles.y + 89);
        
        door.GetComponent<Door>().doorStatus = false;


        source.PlayOneShot(doorshut, 0.3f);
        yield return new WaitForSeconds(9f);
        source.PlayOneShot(lightoff);
        Light3.GetComponentInChildren<Light>().enabled = false;
        StartCoroutine(Text());
        yield return new WaitForSeconds(.2f);
        flickerL.GetComponentInChildren<FlickeringLight>().enabled = true;


        yield return new WaitForSeconds(.6f);
        
        flickerL.GetComponentInChildren<Light>().intensity = 40;
        flickerL.GetComponentInChildren<Light>().color = new Color32(255,8,30,255);
        source.PlayOneShot(bells, .4f);
        source.PlayOneShot(crying, .2f);
        door.GetComponent<Door>().enabled = true;

        //lock inside




    }

    //Triggers are used for event detection
    //This event triggers after the player leaves the bathroom
    IEnumerator ExitEvent()
    {

        source.Stop();
        door.GetComponent<Door>().enabled = false;
        door2.GetComponent<Door>().enabled = false;
        flickerL.GetComponentInChildren<FlickeringLight>().enabled = false;
        source.PlayOneShot(doorshut, 0.3f);
        door2.transform.eulerAngles = new Vector3(door2.transform.eulerAngles.x, door2.transform.eulerAngles.y + 89);
        
        door2.GetComponent<Door>().enabled = true;
        door2.GetComponent<Door>().doorStatus = false;
        source.PlayOneShot(bell, .2f);
        foreach (var text in Voices)
        {
            text.alpha = 0;
        }
        Voices.Clear();
        yield return new WaitForSeconds(1f);
        source.PlayOneShot(lightoff, 0.1f);
        flicker2.GetComponentInChildren<FlickeringLight>().enabled = true;
        yield return new WaitForSeconds(.5f);
        m.SetActive(true);
      

        yield return new WaitForSeconds(.5f);
        source.PlayOneShot(lighton, 0.1f);
        //close door here
        
        door.transform.eulerAngles = new Vector3(door.transform.eulerAngles.x, door.transform.eulerAngles.y + 89);
        door.GetComponent<Door>().enabled = true;
        door.GetComponent<Door>().doorStatus = false;
       
        source.PlayOneShot(doorshut, 0.3f);
        m.SetActive(false);
        flickerL.GetComponentInChildren<Light>().intensity = 20;
        flickerL.GetComponentInChildren<Light>().color = new Color32(248, 249, 231, 255);
        flicker2.GetComponentInChildren<FlickeringLight>().enabled = false;

        Destroy(wall);
        Light3.GetComponentInChildren<Light>().enabled = true;
        yield return null;
    }
    //text to be displayed during event
    IEnumerator Text()
    {
        foreach (var text in Voices)
        {
          yield  return new WaitForSeconds(0.2f);
            text.alpha = .2f;
        }
    }
    #endregion

}
