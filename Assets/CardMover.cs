using UnityEngine;


public class CardMover
{
    private static void MoveTo(GameObject obj, GameObject to){
        if (to == null){
            Debug.LogError(to.name + " is not assigned.");
            return;
        }

        var toPosition = to.transform.position;
        toPosition.z = -0.1f;
        obj.transform.position = toPosition;
    }

    private static void ScaleTo(GameObject obj, GameObject to, string axes = "xyz" ){
        var toRenderer = to.GetComponent<Renderer>();
        var objRenderer = obj.GetComponent<Renderer>();
        
        if (toRenderer == null || objRenderer == null){
            Debug.LogError(to.name + " is not assigned.");
            return;
        };

        var toBounds = toRenderer.bounds;
        var objBounds = objRenderer.bounds;

        var objScale = obj.transform.localScale;

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

    public static  void  MoveToPlayerDeck(GameObject obj){
        MoveTo(obj, GOStore.Instance.playerDeck);
        ScaleTo(obj, GOStore.Instance.playerDeck);
    }

    public static void  MoveToEnemyDeck(GameObject obj){
        MoveTo(obj, GOStore.Instance.enemyDeck);
        ScaleTo(obj, GOStore.Instance.enemyDeck);
    }

    public static void  MoveToPlayerHand(GameObject obj){
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

    public static void MoveToPlayerField(GameObject obj, GameObject field)
    {
        MoveTo(obj, field);
        ScaleTo(obj, field);
        obj.transform.rotation = Quaternion.identity;
    }

    public static void  FlipFrontDown(GameObject obj){
        var objRotation = obj.transform.rotation;
        objRotation.y = 180;
        obj.transform.rotation  = objRotation;
    }
    
    public static void  FlipFrontUp(GameObject obj){  
        var objRotation = obj.transform.rotation;
        objRotation.y = 0;
        obj.transform.rotation  = objRotation;
    }
    
    public static void MoveOutFromBoard(GameObject obj){
        var objPosition = obj.transform.position;
        objPosition.z = -10;
        obj.transform.position = objPosition;
    }
}
