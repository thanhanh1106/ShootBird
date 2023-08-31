using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : Singleton<BackGroundController>
{
    public Sprite[] sprites;
    public SpriteRenderer BackGroundImage;
    protected override void Awake()
    {
        MakeSingleton(false); // hủy đối tượng khi load Scene mới
        BackGroundImage = GetComponent<SpriteRenderer>();
    }
     void Start()
    {
        ChangedBackGround();
    }
    void ChangedBackGround()
    {
        if(BackGroundImage != null && sprites != null && sprites.Length > 0)
        {
            int RandImg = Random.Range(0, sprites.Length);
            if (sprites[RandImg] != null )
            {
                BackGroundImage.sprite = sprites[RandImg];
            }
        }
    }
     
}
