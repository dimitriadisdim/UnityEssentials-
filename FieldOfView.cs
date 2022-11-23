using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]private float viewDistance;
    private float _startingAngle;
    private float _angleIncrease;
    private float _angle = 0f;
    private float _fov = -90f;
    private int _raycount = 50;
    private int _layermask;
    private Vector3 _origin;
    private Mesh _mesh;
    

    private void Start(){
        // Assign mesh
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        // Assign values
        _layermask = LayerMask.GetMask("Enemy");
        _angleIncrease = _fov / _raycount;
        _origin = Vector3.zero;
        // Move it to (0,0,0)
        transform.position = new Vector3(0,0,0);
    }
    
    // Update is called once per frame
    void LateUpdate() => DrawMesh(); //So the update of the player can pass the values first
    
    public void SetCordinates(Vector3 origin, Vector3 aimDirection){
        _origin = origin;
        _startingAngle = Utils.GetAngleFromVectorFloatXZ(aimDirection) - _fov / 2f; 
    }

    private void DrawMesh(){
        // Variables
        _angle = _startingAngle;
        // Make mesh variables
        Vector3[] vertices = new Vector3[_raycount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[_raycount * 3];

        // Assign value to vertices
        vertices[0] = _origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for(int i=0; i<= _raycount; i++){
            Vector3 vertex;
            // Raycasting
            RaycastHit hit;
            if(!Physics.Raycast(_origin, Utils.GetVectorFromAngle(_angle), out hit, viewDistance, ~_layermask))
                vertex = _origin + Utils.GetVectorFromAngle(_angle) * viewDistance; // No hit
            else
                vertex = hit.point; // hit

            // Assign values
            vertices[vertexIndex] = vertex;
            if (i > 0){
                triangles[triangleIndex + 0] = 0;
                triangles[triangleIndex + 1] = vertexIndex;
                triangles[triangleIndex + 2] = vertexIndex -1;

                triangleIndex += 3;
            }
            vertexIndex++;
            _angle -= _angleIncrease;
        }

        _mesh.vertices = vertices;
        _mesh.uv = uv;
        _mesh.triangles = triangles;
        _mesh.bounds = new Bounds(_origin, Vector3.one * 1000f);
    }
}

