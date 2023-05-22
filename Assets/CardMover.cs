using UnityEngine;


public class CardMover
{
    static void MoveTo(GameObject obj, GameObject to){
        if (to == null){
            Debug.LogError(to.name + " is not assigned.");
            return;
        }

        Vector3 toPosition = to.transform.position;
        toPosition.z = -0.1f;
        obj.transform.position = toPosition;
    }

    static void ScaleTo(GameObject obj, GameObject to, string axes = "xyz" ){
        Renderer toRenderer = to.GetComponent<Renderer>();
        Renderer objRenderer = obj.GetComponent<Renderer>();

        if (toRenderer == null || objRenderer == null){
            Debug.LogError(to.name + " is not assigned.");
            return;
        };

        Bounds toBounds = toRenderer.bounds;
        Bounds objBounds = objRenderer.bounds;

        Vector3 objScale = obj.transform.localScale;

        if (axes.Contains("x"))
        {
            objScale.x *= toBounds.size.x / objBounds.size.x;
        }

        if (axes.Contains("y"))
        {
            objScale.y *= toBounds.size.y / objBounds.size.y;
        }

        if (axes.Contains("z"))
        {
            objScale.z *= toBounds.size.z / objBounds.size.z;
        }

        obj.transform.localScale = objScale;

    }

    static public void  MoveToPlayerDeck(GameObject obj){
        MoveTo(obj, GOStore.Instance.playerDeck);
        ScaleTo(obj, GOStore.Instance.playerDeck);
    }

    static public void  MoveToEnemyDeck(GameObject obj){
        MoveTo(obj, GOStore.Instance.enemyDeck);
        ScaleTo(obj, GOStore.Instance.enemyDeck);
    }

    static public void  MoveToPlayerHand(GameObject obj){
        // Vector3 objPosition = obj.transform.position;
        // float gap = 1.10f;

        // objPosition.x = - playerHand.transform.localScale.x / 2f + obj.transform.localScale.x / 2f + obj.transform.localScale.x * (cardsInHand * gap);
        // objPosition.y = playerHand.transform.position.y;
        // objPosition.z = -1;

        // obj.transform.position = objPosition;

        // cardsInHand++;
        MoveTo(obj, GOStore.Instance.playerHand);
        ScaleTo(obj, GOStore.Instance.playerHand, "yz");
        obj.transform.rotation = Quaternion.identity;
    }

    static public void MoveToPlyerField(GameObject obj, GameObject field)
    {
        MoveTo(obj, field);
        ScaleTo(obj, field);
        obj.transform.rotation = Quaternion.identity;
    }

    static public void  FlipFrontDown(GameObject obj){
        Quaternion objRotation = obj.transform.rotation;
        objRotation.y = 180;
        obj.transform.rotation  = objRotation;
    }
    
    static public void  FlipFrontUp(GameObject obj){  
        Quaternion objRotation = obj.transform.rotation;
        objRotation.y = 0;
        obj.transform.rotation  = objRotation;
    }
    
    static public void MoveOutFromBoard(GameObject obj){
        Vector3 objPosition = obj.transform.position;
        objPosition.z = -10;
        obj.transform.position = objPosition;
    }
}
