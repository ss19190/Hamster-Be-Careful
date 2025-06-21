using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HamsterMover : MonoBehaviour
{
    public GameObject hamster;
    private bool isAtTop;
    private bool isAtBottom;

    // Start is called before the first frame update
    void Start()
    {
        isAtTop = false;
        isAtBottom = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkIfBottomEdge();
        MoveHamster();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("TopScreen")){
            isAtTop = true;
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Objects")){
            FindObjectOfType<GameController>().objectMet(collision);
        }
            
    }
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.layer == LayerMask.NameToLayer("TopScreen"))
        {
            isAtTop = false;
        }
    }
    private void MoveHamster()
    {
        float moveY = Input.GetAxisRaw("Vertical");
        if(moveY > 0 && !isAtTop){
            transform.Translate(Vector2.up * moveY * 0.1f);
        }
        if(moveY < 0 && !isAtBottom){
            transform.Translate(Vector2.up * moveY * 0.1f);
        }
    }

    private void checkIfBottomEdge()
    {
        float bottomEdge = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        if (transform.position.y - 0.6 <= bottomEdge)
        {
            isAtBottom = true;
        }
        else
        {
            isAtBottom = false;
        }
        
    }
}
