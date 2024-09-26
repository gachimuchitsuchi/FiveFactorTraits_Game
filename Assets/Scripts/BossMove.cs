using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    private int speed;                //�I�u�W�F�N�g�̃X�s�[�h
    private int radius;               //�~��`�����a
    private Vector2 defPosition;      //defPosition��Vector2�Œ�`����B
    float x;
    float y;
    float elapsedTime;                //�o�ߎ���

    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        radius = 2;

        defPosition = transform.position;    //defPosition�������̂���ʒu�ɐݒ肷��B
        elapsedTime = 0.0f;                  //�o�ߎ��Ԃ�������
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

            x = radius * Mathf.Sin(Time.time * speed);      //X���̐ݒ�
            y = radius * Mathf.Cos(Time.time * speed);      //Y���̐ݒ�

            transform.position = new Vector2(x + defPosition.x, y + defPosition.y);  //�����̂���ʒu������W�𓮂����B
        }
        
    }

    public void Horizontal()
    {

    }
}
