using TMPro;
using UnityEngine;



public class Card : MonoBehaviour
{
    public GameObject nameTextGO;
    public GameObject hpTextGO;
    public GameObject atkTextGO;
    public GameObject manacostTextGO;
    public bool drag;

    public void SetCardInfo(CardInfo cardInfo){
        nameTextGO.GetComponent<TextMeshPro>().text = cardInfo.name;
        hpTextGO.GetComponent<TextMeshPro>().text = cardInfo.hp.ToString();
        atkTextGO.GetComponent<TextMeshPro>().text = cardInfo.atk.ToString();
        manacostTextGO.GetComponent<TextMeshPro>().text = cardInfo.manacost.ToString();
    }

    void OnMouseDrag()
    {
        if(!drag)
        {
            return;
        }
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion toRotation = Quaternion.Euler(mousePosition.y - transform.position.y, -(mousePosition.x - transform.position.x) * 90, 0);

        mousePosition.z = -1;
        transform.position = Vector3.Lerp(transform.position, mousePosition, Time.deltaTime * 15.0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * 15.0f);
        transform.localScale = new Vector3(2, 3, 0.01f);
    }

    void OnMouseUp()
    {
        if(!drag)
        {
            return;
        }

        Ray ray = new Ray(transform.position, Vector3.forward);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.CompareTag("DropZone"))
            {
                CardMover.MoveToPlayerField(gameObject, hitInfo.collider.gameObject);
            }
            else
            {
                CardMover.MoveToPlayerHand(gameObject);
            }
        }
        else
        {
            CardMover.MoveToPlayerHand(gameObject);
        }
        
    }

}
