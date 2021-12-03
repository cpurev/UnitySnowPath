using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSnowCollider : MonoBehaviour
{
    public Shader drawShader;
    private RenderTexture dispmap;
    private Material myMaterial;
    private Material drawMaterial;
    public GameObject terrain;

    public Transform[] _colliders;
    RaycastHit hit;
    int layer;

    // Start is called before the first frame update
    void Start()
    {
        layer = LayerMask.GetMask("Ground");
        drawMaterial = new Material(drawShader);
        drawMaterial.SetVector("_Color", Color.red);

        myMaterial = terrain.GetComponent<MeshRenderer>().material;
        myMaterial.SetTexture("_DispTex", dispmap = new RenderTexture(512, 512, 0, RenderTextureFormat.ARGBFloat));
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < _colliders.Length; ++i){
            if(Physics.Raycast(_colliders[i].position, -Vector3.up, out hit, 1f, layer)){
                drawMaterial.SetVector("_Coordinate", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
                drawMaterial.SetFloat("_Size", 0.8f);
                
                RenderTexture temp = RenderTexture.GetTemporary(dispmap.width, dispmap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(dispmap, temp);
                Graphics.Blit(temp, dispmap, drawMaterial);
                RenderTexture.ReleaseTemporary(temp);
            }
        }
        
    }
}
