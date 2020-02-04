using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NegativeSpaceInteraction : MonoBehaviour
{

    public float range;
    public LayerMask terrainDetection;
    public float phaseTime;
    RaycastHit surfaceFound;
    public PlayerController player;


    [Header("Cosmetics")]
    public Image indicator;
    public Color canInvert;
    public Color cannotInvert;
    public Color defaultColour;
    public ColourTransitionEffect phaseOverlay;

    bool isPhasing;
    bool isInverted;

    //public Transform arrow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out surfaceFound, range, terrainDetection))
        {
            MeshCollider c = surfaceFound.collider.GetComponent<MeshCollider>();
            if (c != null)
            {
                indicator.color = canInvert;

                if (Input.GetButtonDown("Mouse Left") && isPhasing == false)
                {
                    //InvertSpace(surfaceFound, c);
                    phaseOverlay.Play();
                    StartCoroutine(InvertPhase(surfaceFound, c));
                }
            }
            else
            {
                indicator.color = cannotInvert;
            }

            //print(surfaceFound.normal.)
            //print(Quaternion.FromToRotation(Vector3.up, surfaceFound.normal));

            //arrow.SetPositionAndRotation(surfaceFound.point, Quaternion.FromToRotation(Vector3.back, surfaceFound.normal));
        }
        else
        {
            indicator.color = defaultColour;
        }
        
    }

    /*
    public void InvertSpace(RaycastHit surface, MeshCollider surfaceMesh)
    {
        Vector3 teleportLocation = surfaceFound.point + Quaternion.FromToRotation(Vector3.back, surfaceFound.normal) * Vector3.forward * 2;
        player.transform.position = teleportLocation;

        #region Invert mesh normals
        Mesh mesh = surfaceFound.collider.GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;

        for (int m = 0; m < mesh.subMeshCount; m++)
        {
            int[] triangles = mesh.GetTriangles(m);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i + 0];
                triangles[i + 0] = triangles[i + 1];
                triangles[i + 1] = temp;
            }
            mesh.SetTriangles(triangles, m);
        }

        surfaceMesh.sharedMesh = mesh; // Updates mesh
        #endregion
    }
    */

    public IEnumerator InvertPhase(RaycastHit surface, MeshCollider surfaceMesh)
    {
        isPhasing = true;

        Vector3 originalPosition = player.transform.position;
        Vector3 teleportLocation = surfaceFound.point + Quaternion.FromToRotation(Vector3.back, surfaceFound.normal) * Vector3.forward * 2;

        #region Invert mesh normals
        Mesh mesh = surfaceFound.collider.GetComponent<MeshFilter>().mesh;
        Vector3[] normals = mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;

        for (int m = 0; m < mesh.subMeshCount; m++)
        {
            int[] triangles = mesh.GetTriangles(m);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                int temp = triangles[i + 0];
                triangles[i + 0] = triangles[i + 1];
                triangles[i + 1] = temp;
            }
            mesh.SetTriangles(triangles, m);
        }

        surfaceMesh.sharedMesh = mesh; // Updates mesh
        #endregion

        float elapsedTime = 0;

        while (elapsedTime < phaseTime)
        {
            player.transform.position = Vector3.Lerp(originalPosition, teleportLocation, elapsedTime / phaseTime);
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }

        // Make sure we got there
        player.transform.position = teleportLocation;

        yield return null;

        isPhasing = false;

        print("Completed");



    }
}
