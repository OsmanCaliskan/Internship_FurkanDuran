using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

// Bir değişkenin public ya da private olmasını neye göre karar verdin
// set get fonksiyonları tam olarak hangi problemi çözüyor ?

public class CubeBehaviour : MonoBehaviour
{
    public int index;

    public Vector3 offSet;
    public Vector3 startPos;
    public Vector3 center;
    public float hover = -0.1f;
    
    int state = 0;
    public int myRow;
    public int myColumn;
    
    public Material Material1;
    public Material Material2;
    public Material Material3;
    
    public GameObject gridCubes;

    private Vector3 scaleChange;
    private float startScaleChange = 0.03f;
    public int scaleChangeStart;
    
    private float lerper = 0.05f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (index == 0)
        {
            GetComponent<Renderer>().material = Material1;
        }
        else if (index == 1)
        {
            GetComponent<Renderer>().material = Material2;
        }
        else
        {
            GetComponent<Renderer>().material = Material3;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        
        Vector3 target = new Vector3(0, 0, 0);
        mousePos.z = -Camera.main.transform.position.z - target.z;
        
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        
        //state 0 1 2 neyi simgeliyo ?
        if (state == 0)
        {
            // yanlış lerp kullanımı fps dependent
            transform.position = Vector3.Lerp(transform.position, startPos, lerper);
            myColumn = -1;
            myRow = -1;
        }
        else if (state == 1)
        {
            // yanlış lerp kullanımı fps dependent
            transform.position = Vector3.Lerp(transform.position + new Vector3(0, 0, hover), worldPos + offSet + center, lerper);
        }
        else if (state == 2)
        {
            // yanlış lerp kullanımı fps dependent
            transform.position = Vector3.Lerp(transform.position, startPos, lerper);
            index = -2;
        }
        else if (state == 3)
        {
            // yanlış lerp kullanımı fps dependent
            transform.position = Vector3.Lerp(transform.position, startPos, lerper);
            if (transform.localScale.x <= 0.1f)
            {
                Destroy(gameObject);
            }
            else if (scaleChangeStart <= 0)
            {
                startScaleChange = Mathf.Lerp(startScaleChange, -0.01f, 0.1f);
                scaleChange = new Vector3(startScaleChange, startScaleChange, startScaleChange);
                transform.localScale += scaleChange;
            }
            else
            {
                scaleChangeStart -= 1;
            }
        }
    }

    public void ReturnMyLocation(out int cIndex, out int rIndex)
    {
        float separate = 0.5f;
        int row = 10, column = 10;
        
        float baseY = ((column - 1) * separate) / 2;
        float baseX = ((row - 1) * separate) / 2;

        rIndex = Mathf.FloorToInt((transform.position.x + baseX + separate / 2) / separate);
        cIndex = Mathf.FloorToInt((baseY - transform.position.y + separate / 2) / separate);
    }

    public void PlaceMyself()
    {
        myColumn = -1;
        myRow = -1;
        ReturnMyLocation(out myColumn, out myRow);
        
        float separate = 0.5f;
        int row = 10, column = 10;
        
        float baseY = ((column - 1) * separate) / 2;
        float baseX = ((row - 1) * separate) / 2;

        float myX = ((myRow - 0.5f) * separate + separate / 2) - baseX;
        float myY = (myColumn + 0.5f) * separate - baseY - separate / 2;
        
        startPos = new Vector3(myX, -myY, 0);
    }
    
    public void setIndex(int index)
    {
        this.index = index;
    }
    public int getIndex() {return this.index;}
    
    public int GetState() {return this.state;}
    
    public void setOffset(Vector3 offSet)
    {
        this.offSet = offSet;
    }
    
    public void setStart(Vector3 startPos)
    {
        this.startPos = startPos;
    }

    public void setCenter(Vector3 center)
    {
        this.center = center;
    }
    
    public void setState(int state)
    {
        this.state = state;
    }
}
