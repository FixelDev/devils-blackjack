using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartsDepthController : MonoBehaviour
{
    [SerializeField] private LivesController playerLivesController;
    [SerializeField] private LivesController devilLiveController;

    private List<GameObject> activeHearts;
    private int maxAmountOfHearts;

    private void Start() 
    {
        maxAmountOfHearts = playerLivesController.lives.GetLivesAmount() + devilLiveController.lives.GetLivesAmount();
        activeHearts = new List<GameObject>(); 
    }

    public void SortHeart(GameObject heart)
    {
        for(int i = 0; i <= activeHearts.Count; i++)
        {
            if(activeHearts.Find(h => h.transform.position.z == i) == false)
            {          
                ChangeHeartDepth(heart, i);   
                break;
            }

        }


        activeHearts.Add(heart);

        SortActiveHeartsList();
        
    }

    private void SortActiveHeartsList()
    {
        activeHearts = activeHearts.OrderByDescending(h => h.transform.position.z).ToList();
    }

    private void ChangeHeartDepth(GameObject heart, int depth)
    {
        Vector3 heartPosition = heart.transform.position;
        heartPosition.z = depth;
        heart.transform.position = heartPosition;

        SpriteRenderer heartSpriteRenderer = heart.GetComponentInChildren<SpriteRenderer>();

        heartSpriteRenderer.sortingOrder = maxAmountOfHearts - depth;

        SpriteRenderer heartShadowSpriteRenderer = heart.transform.Find("ShadowGFX").GetComponent<SpriteRenderer>();
        heartShadowSpriteRenderer.sortingOrder = heartSpriteRenderer.sortingOrder - 1;
    }

    public void RemoveHeart(GameObject heart)
    {
        activeHearts.Remove(heart);
    }

    public void RiseHeartToTop(GameObject heart)
    {

        int heartDepth = (int)heart.transform.position.z;
        int heartToMoveDepth = heartDepth;

        ChangeHeartDepth(heart, (int)activeHearts.Last().transform.position.z);

        int currentIndex = activeHearts.IndexOf(heart) + 1;

        while(currentIndex <= activeHearts.IndexOf(activeHearts.Last()))
        {
            GameObject currentHeart = activeHearts[currentIndex];
            int tmp = (int)currentHeart.transform.position.z;
            ChangeHeartDepth(currentHeart, heartToMoveDepth);
            heartToMoveDepth = tmp;
            currentIndex++;
        }

        SortActiveHeartsList();
    }
}
