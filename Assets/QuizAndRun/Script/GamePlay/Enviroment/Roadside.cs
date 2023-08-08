using UnityEngine;

public class Roadside : MonoBehaviour
{
    [SerializeField] Color colorOnTrigger;
    [SerializeField] Color colorOffTrigger;

    private void Awake()
    {
        colorOffTrigger = this.gameObject.GetComponent<SpriteRenderer>().color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = colorOnTrigger;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = colorOffTrigger;
        
    }

}
