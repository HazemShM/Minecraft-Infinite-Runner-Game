
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerFuel : MonoBehaviour
{
    private float maxFuel = 50;
    private float currentFuel;
    private float burnRate = 10f;
    private float normalDrainRate = 1f;
    [SerializeField] Text fuelText;
    private PlayerMovement playerMovement;
    private 
    void Start()
    {   
        playerMovement = GetComponent<PlayerMovement>();
        currentFuel = maxFuel;
    }

    void Update()
    {
        fuelText.text = ""+((int)currentFuel);
        if(currentFuel <=15){
            fuelText.color = Color.red;
        }else{
            fuelText.color = Color.green;
        }
        if(!playerMovement.GameOver()){
            currentFuel -= normalDrainRate * Time.deltaTime;
        }
        if (currentFuel <= 0){
            playerMovement.Die();
        }
    }

    public void StartBurning()
    {
        normalDrainRate = burnRate;
    }

    public void StopBurning()
    {   
        normalDrainRate = 1f;
    }

    public void RefillFuel()
    {
        currentFuel = maxFuel;
    }
    public int getFuel()
    {
        return (int) currentFuel;
    }

}

