using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShelfRandomFill : MonoBehaviour
{
    [SerializeField]
    private GameObject scatterObject;

    [SerializeField]
    private int emptySpaces;

    [SerializeField]
    private int shelfLinePositions;

    [SerializeField]
    private int shelfRows;

    public float rowHeight = 0.39601f;
    public float shelfLength = 0.94248f;
    public float offsetGround = 0.02399f;
    public float offsetShelfLeft = 0.02876f;
    public float depth = 0.25f;

    //
    private void OnEnable()
    {
        GameObject parentObj = new GameObject("ParentSpawn");

        parentObj.transform.SetParent(gameObject.transform);

        int itemsToPlace = shelfLinePositions * 5;
        float tempWidth = shelfLength / (shelfLinePositions + 1);

        int currentHeight = 0;
        int currentPlace = 1;

        float currentRowWidth = -tempWidth;
        float tempDepth = depth / (shelfRows + 1);

        Vector3 objPosition = new Vector3(0, 0, 0);

        for (int i = 1; i < shelfRows + 1; i++)
        {
            objPosition.x = tempDepth * i;

            for (int y = 0; y < itemsToPlace; y++)
            {
                GameObject currentScatterObj = Instantiate(scatterObject, new Vector3(0, 0, 0), scatterObject.transform.rotation);

                currentScatterObj.transform.SetParent(parentObj.transform);

                objPosition.y = offsetGround + currentHeight * rowHeight;


                currentRowWidth = offsetShelfLeft + currentPlace * tempWidth;

                objPosition.z = currentRowWidth;
                objPosition.z += Random.Range(-0.025f, 0.025f);

                currentScatterObj.transform.position = objPosition;

                currentPlace++;

                if (currentPlace > shelfLinePositions)
                {
                    currentPlace = 1;

                    currentHeight++;

                    currentRowWidth = -tempWidth;

                    if (currentHeight >= 5)
                    {
                        currentHeight = 0;
                        currentPlace = 1;

                        break;
                    }
                }
            }
        }


        

        for (int i = 0; i < emptySpaces; i++)
        {
            int x = Random.Range(0, parentObj.transform.childCount);

            DestroyImmediate(parentObj.transform.GetChild(x).gameObject);
        }

        parentObj.transform.localPosition = new Vector3(0, 0, 0);
    }
}