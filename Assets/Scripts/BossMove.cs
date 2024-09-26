using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    private int speed;                //オブジェクトのスピード
    private int radius;               //円を描く半径
    private Vector2 defPosition;      //defPositionをVector2で定義する。
    float x;
    float y;
    float elapsedTime;                //経過時間

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        radius = 2;

        defPosition = transform.position;    //defPositionを自分のいる位置に設定する。
        elapsedTime = 0.0f;                  //経過時間を初期化
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Circle()
    {
        while (true)
        {
            elapsedTime += Time.deltaTime * speed;

            x = radius * Mathf.Sin(Time.time * speed);      //X軸の設定
            y = radius * Mathf.Cos(Time.time * speed);      //Y軸の設定

            transform.position = new Vector2(x + defPosition.x, y + defPosition.y);  //自分のいる位置から座標を動かす。
        }
        
    }

    public void Horizontal()
    {

    }
}
