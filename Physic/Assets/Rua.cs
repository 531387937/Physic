using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rua : MonoBehaviour {
    public Camera ca;
    private Physic ph;
    Vector3 screenPosition;//将物体从世界坐标转换为屏幕坐标
    Vector3 mousePositionOnScreen;//获取到点击屏幕的屏幕坐标
    Vector3 mousePos;//将点击屏幕的屏幕坐标转换为世界坐标
    Vector3 dir;
    public bool Canhit=true;
    public float maxPower = 25;
    public float powerSpeed = 1;
    private float a = 0;
    private bool b = false;
    GUIStyle fontStyle = new GUIStyle();
    public GameObject[] balls;
int i=0;

    void Start () {
        balls = GameObject.FindGameObjectsWithTag("Py");
        fontStyle.normal.background = null;    //设置背景填充  
        fontStyle.normal.textColor= new Color(1,0,0);   //设置字体颜色  
    fontStyle.fontSize = 40;       //字体大小  
        ph = GetComponent<Physic>();
	}

    // Update is called once per frame
    void Update()
    {

        //获取鼠标在相机中（世界中）的位置，转换为屏幕坐标；
        screenPosition = ca.WorldToScreenPoint(transform.position);
        //获取鼠标在场景中坐标
        mousePositionOnScreen = Input.mousePosition;
        //让场景中的Z=鼠标坐标的Z
        mousePositionOnScreen.z = screenPosition.z;
        //将相机中的坐标转化为世界坐标
        mousePos = -ca.ScreenToWorldPoint(mousePositionOnScreen);
        //物体跟随鼠标移动


        if (Canhit)
        {
            if (Input.GetMouseButtonDown(0))
            {

                dir = new Vector3(-transform.position.x - mousePos.x, 0, -transform.position.z - mousePos.z).normalized;
            }
            if (Input.GetMouseButton(0))
            {
                print(a);
                if (a < maxPower && !b)
                {
                    a += powerSpeed * Time.deltaTime;
                }
                if (a >= maxPower)
                {
                    b = true;
                }
                if (b)
                {
                    a -= powerSpeed * Time.deltaTime;
                }
                if (a <= 0)
                {
                    b = false;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                ph.velocity = dir * a;
                Canhit = false;
                a = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        if(!Canhit)
        {
            float x = balls.Length;
            
                if(balls[i].GetComponent<Physic>().velocity.magnitude<=0.01f)
                {
                    i++;
                }
            
            if(i==x)
            {
                Canhit = true;
                i = 0;
            }

        }
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2, 60, 850, 800), string.Format("蓄力中："+a.ToString()),fontStyle);
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
    }
}
