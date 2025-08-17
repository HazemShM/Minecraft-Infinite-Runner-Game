
using UnityEngine;

public class BlocksSpecialities : MonoBehaviour
{
    private PlayerFuel playerFuel;
    private PlayerMovement playerMovement;
    private void Start()
    {
        playerFuel = GetComponent<PlayerFuel>();
        playerMovement = GetComponent<PlayerMovement>();
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Burning")){
            AudioManager.manager.PlaySFX("fireB");
            playerFuel.StartBurning();
        }
        if(collision.gameObject.CompareTag("Supplies")){
            AudioManager.manager.PlaySFX("Supplies");
            playerFuel.RefillFuel();
        }
        if(collision.gameObject.CompareTag("Boost")){
            AudioManager.manager.PlaySFX("Boost");
            playerMovement.StartRunning();
        }
        if(collision.gameObject.CompareTag("Sticky")){
            AudioManager.manager.PlaySFX("Sticky");
            playerMovement.StopRunning();
        }
        if(collision.gameObject.CompareTag("Obstacle")){
            AudioManager.manager.PlaySFX("Obstacle");
            playerMovement.Die();
        }

    }
    private void OnCollisionExit(Collision collision){
        if(collision.gameObject.CompareTag("Burning")){
            playerFuel.StopBurning();
            
        }
        // if(collision.gameObject.CompareTag("Boost")){
        // }
        // if(collision.gameObject.CompareTag("Supplies")){
            
        // }
        // if(collision.gameObject.CompareTag("Sticky")){

        // }
    }
    private void OnTriggerEnter(Collider collider){
        if(collider.gameObject.CompareTag("Sticky")){
            AudioManager.manager.PlaySFX("Portal");
            playerMovement.StopRunning();
        }
        if(collider.gameObject.CompareTag("Empty")){
            AudioManager.manager.PlaySFX("Falling");
            playerMovement.Fall();
        }
    }

}
