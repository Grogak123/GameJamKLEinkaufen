using UnityEditor;

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ShelfRandomFill : MonoBehaviour
{
    [SerializeField]
    private GameObject scatterObject;

    public float scatterObjectWidth;

    [SerializeField]
    private int randomEmptySpaces;

    [SerializeField]
    private int objectsInOneLine;

    [SerializeField]
    private int shelfRows;

    [SerializeField]
    private int shelfRowsStartHeight;

    [SerializeField]
    private int shelfRowsEndHeight;

    [Range(0.0f, 0.25f)]
    public float randomOffset;

    public float depthOffset;

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

        int itemsToPlace = (objectsInOneLine * (shelfRowsEndHeight - shelfRowsStartHeight)) * shelfRows;

        float spaceBetweenObjects = shelfLength - (scatterObjectWidth * objectsInOneLine);
        spaceBetweenObjects = spaceBetweenObjects / (objectsInOneLine + 1);


        int currentHeight = shelfRowsStartHeight;
        int currentPlace = 1;

        float currentRowWidth = offsetShelfLeft + spaceBetweenObjects + (scatterObjectWidth / 2);
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

                objPosition.z = currentRowWidth;

                currentRowWidth += (spaceBetweenObjects + scatterObjectWidth);

                objPosition.z += Random.Range(-randomOffset, randomOffset);

                currentScatterObj.transform.position = objPosition;

                currentPlace++;

                if (currentPlace > objectsInOneLine)
                {
                    currentPlace = 1;

                    currentHeight++;

                    currentRowWidth = offsetShelfLeft + spaceBetweenObjects + (scatterObjectWidth / 2);

                    if (currentHeight >= shelfRowsEndHeight)
                    {
                        currentHeight = shelfRowsStartHeight;
                        currentPlace = 1;

                        break;
                    }
                }
            }
        }

        //Zufällig Items löschen
        for (int i = 0; i < randomEmptySpaces; i++)
        {
            int x = Random.Range(0, parentObj.transform.childCount);

            DestroyImmediate(parentObj.transform.GetChild(x).gameObject);
        }

        //Meshes zu einem neuen Mesh zusammenfügen


        //MeshFilter[] meshFilters = parentObj.GetComponentsInChildren<MeshFilter>();

        //CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        //int z = 0;
        //while (z < meshFilters.Length)
        //{
        //    combine[z].mesh = meshFilters[z].sharedMesh;

        //    combine[z].transform = meshFilters[z].transform.localToWorldMatrix;

        //    meshFilters[z].gameObject.SetActive(false);

        //    z++;
        //}

        //GameObject newMesh = new GameObject(scatterObject.name + "_CombinedMesh");
        //newMesh.AddComponent<MeshFilter>();
        //newMesh.AddComponent<MeshRenderer>();

        //newMesh.GetComponent<MeshFilter>().mesh = new Mesh();
        //newMesh.GetComponent<MeshFilter>().sharedMesh.CombineMeshes(combine);

        //newMesh.GetComponent<MeshRenderer>().material = scatterObject.GetComponent<MeshRenderer>().sharedMaterial;

        //SaveMesh(newMesh.GetComponent<MeshFilter>().sharedMesh, scatterObject.name + "_CombinedMesh", false, false);

        //parentObj.transform.localPosition = new Vector3(0, 0, 0);
    }
}