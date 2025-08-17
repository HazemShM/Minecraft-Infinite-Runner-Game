using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{   
    private ScrollRect scrollRect;     
    private float scrollSpeed = 0.07f; 
    private float scrollPosition = 0f; 
    private bool isScrolling = true;
    private void Start(){

        scrollRect = GetComponent<ScrollRect>();
        isScrolling = true;
        scrollPosition = 0f;
        scrollRect.verticalNormalizedPosition = 1f; 
    }
    private void Update()
    {
        if (isScrolling && scrollPosition < 1f){
            scrollPosition += scrollSpeed * Time.deltaTime;
            scrollRect.verticalNormalizedPosition = 1f - scrollPosition;
        }
        else if (scrollPosition >= 1f){
            isScrolling = false;
        }
    }
    public void ReScroll(){

        scrollRect = GetComponent<ScrollRect>();
        isScrolling = true;
        scrollPosition = 0f;
        scrollRect.verticalNormalizedPosition = 1f; 
    }
}
