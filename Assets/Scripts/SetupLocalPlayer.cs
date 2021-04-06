using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour
{
    private void Start()
    {
        if (isLocalPlayer)
        {
            gameObject.GetComponent<PlayerController>().enabled = true;
            SetUpLocalPlayerSettings();
        }
        else
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }

    void SetUpLocalPlayerSettings() {


        StartCoroutine(SetCamera());
    
    }

    System.Collections.IEnumerator SetCamera() {
  Camera camera = Camera.main;
        camera.transform.position = this.gameObject.transform.position + new Vector3(0, 5f, -8f);

        camera.transform.rotation= Quaternion.LookRotation(((this.transform.position + Vector3.forward * 3f) - camera.transform.position).normalized,Vector3.up); //(this.transform.position + Vector3.forward*3f)- camera.transform.position).normalized
        yield return new WaitForSeconds(0.5f);
      
        
        camera.transform.SetParent(this.transform);
    }
}
