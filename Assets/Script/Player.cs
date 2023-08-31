using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float FireRate;
    float currentFireRate;
    bool isShooted;

    public GameObject ViewFinder;
    GameObject viewFinderClone;
    private void Start()
    {
        isShooted = false;
        if(ViewFinder != null) viewFinderClone = Instantiate(ViewFinder,Vector3.zero,Quaternion.identity);
        
    }
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        viewFinderClone.transform.position = new Vector3(mousePos.x, mousePos.y, -1);
        onClick(() => Shoot(mousePos));
        Reload();
    }
    void onClick(Action action)
    {
        if (!GameController.Instance.IsGameStar || GameController.Instance.IsGameOver) return;
        if(Input.GetMouseButtonDown(0) && !isShooted) action();
    }
    void Shoot(Vector3 mousePos)
    {
        AudioController.Instance.PlaySound(AudioController.Instance.Shooting);
        CineController.Instance.ShakeTrigger();
        // lấy ra vector hướng từ camera tới con trỏ chuột
        Vector3 shootDirection = Camera.main.transform.position - mousePos;

        // chuẩn hóa vector, tức là chia các thành phần của vector cho magnitude của nó để được 1 vector có độ dài là 1
        // ví dụ vector(3,4) => magtitude = sqrt(3^2 + 4^2) = 5 => vector chuẩn hóa là vector(3/5,4/5) = vector(0.6,0.8) có magnitude = 1
        shootDirection.Normalize();  // chuẩn hóa vector

        // Raycast: thực hiện 1 tia kiểm tra xem tia đó có va chạm với collider nào không
        // RaycastHit: cung cấp thông tin của va chạm gồm đối tượng va chạm, vị trí va chạm và hướng của tia 
        // như bên dưới gọi RaycastAll sẽ trả về tất cả các RaycastHit lưu nó vào 1 array
        // 2 tham số truyền vào là:
        //  +origin là vị trí bắt đầu của tia, đại diện cho vị trí xuất phát của tia chéo
        //  +direction là hướng của tia 
        RaycastHit2D[] hit2Ds = Physics2D.RaycastAll(mousePos, shootDirection);// lấy tất cả các vật mà raycast(dễ hiểu thì là súng ngắm vào cái nào nó trả về cái đấy)
        if(hit2Ds.Length > 0 && hit2Ds != null)
        {
            for(int i = 0; i < hit2Ds.Length; i++)
            {
                RaycastHit2D hit = hit2Ds[i];
                float distance = Vector3.Distance((Vector2)hit.collider.transform.position,(Vector2)mousePos); // khoảng cách giữa collider của vật và con trỏ chuột
                if(hit.collider != null && distance <= 0.4f)
                {
                    var bird = hit.collider.GetComponent<Bird>();
                    bird.Die();
                }
            }
        }
        isShooted = true;
        currentFireRate = FireRate;
    }
    void Reload()
    {
        if (isShooted)
        {
            currentFireRate -= Time.deltaTime;
            GameGUIManager.Instance.UpdateFireRate(currentFireRate);
            if (currentFireRate <= 0)
            {
                isShooted = false;
            }
        }
    }
}
