using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    PrimitiveType primitiveToPlace;

    Vector3 nextShapePreviewPos = new Vector3(-7, 1, 1);
    GameObject previewObject;
    private float timer = 15f;
    public Text TimerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        GenerateNextShape();
        TimerText.text = $"Timer: {(int)timer}";
    }

    // Update is called once per frame
    void GenerateNextShape()
    {
        switch (Random.Range(0, 4)) //Da 0 a 3 (4 e' escluso)
        {
            case 0: primitiveToPlace = PrimitiveType.Cube; break;
            case 1: primitiveToPlace = PrimitiveType.Sphere; break;
            case 2: primitiveToPlace = PrimitiveType.Capsule; break;
            case 3: primitiveToPlace = PrimitiveType.Cylinder; break;
            default: primitiveToPlace = PrimitiveType.Cube; break;
        }

        if (previewObject) Destroy(previewObject);

        previewObject = GameObject.CreatePrimitive(primitiveToPlace);
        previewObject.name = "Preview shape";
        previewObject.transform.position = nextShapePreviewPos;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) //right mouse button 
        {
            if(Time.time > 15)
            {
                enabled = false;
            }

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                GameObject go = GameObject.CreatePrimitive(primitiveToPlace);

                go.transform.localScale = Vector3.one * 0.3f;
                go.transform.position = hit.point + new Vector3(0, 1f, 0);
                go.transform.rotation = Random.rotation;

                Block block = go.AddComponent<Block>();

                switch (primitiveToPlace)
                {
                    case PrimitiveType.Cube: block.Points = 5; break;
                    case PrimitiveType.Sphere: block.Points = 10; break;
                    case PrimitiveType.Capsule: block.Points = 15; break;
                    case PrimitiveType.Cylinder: block.Points = 20; break;
                }

                go.AddComponent<Rigidbody>();
                

                // Control color randomness
                Color randomColor = Random.ColorHSV();

                float H, S, V;
                Color.RGBToHSV(randomColor, out H, out S, out V);

                S = 0.8f;
                V = 0.8f;

                randomColor = Color.HSVToRGB(H, S, V);

                go.GetComponent<MeshRenderer>().material.color = randomColor;

                //MUST BE INSIDE ASSETS/RESOURCES
                Texture texture = Resources.Load<Texture>("Wood_Texture");

                Debug.Log(texture);

                //go.GetComponent<MeshRenderer>().material.SetTexture("_BaseMap", texture);
                go.GetComponent<MeshRenderer>().material.mainTexture = texture;

                go.AddComponent<DestroyOnFall>();

                go.AddComponent<DragWithMouse>();

                GenerateNextShape();
            }
        }
    }

    void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.fixedDeltaTime;
        }
        TimerText.text = $"Timer: {(int)timer}";
    }
    
}
