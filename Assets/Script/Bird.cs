using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float xSpeed;
    public float yMinSpeed;
    public float yMaxSpeed;

    Rigidbody2D rigidbody;

    bool moveLeft;
 
    public GameObject DeathVFX;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
       MoveDiretion();
        Flip();
    }
    private void Update()
    {
        Fly();   
    }
    void MoveDiretion()
    {
        moveLeft = transform.position.x > 0 ? true : false;
    }
    void Flip()
    {
        Vector3 Scale = transform.localScale;
        if (moveLeft)
        {
            
            if (Scale.x < 0) return;
            transform.localScale = new Vector3(Scale.x * -1,Scale.y,Scale.z);

        }
        else
        {
            if(Scale.x > 0) return;
            transform.localScale = new Vector3(Scale.x * -1, Scale.y, Scale.z); 
        }
    }
    void Fly()
    {
        float RandomY = Random.Range(yMinSpeed, yMaxSpeed); // random cả về tốc độ lẫn hướng di chuyển lên xuống
        rigidbody.velocity = moveLeft ? new Vector2(-xSpeed, RandomY) : new Vector2(xSpeed, RandomY);
    }
    public void Die()
    {
        GameController.Instance.CountBirdKilled++;
        GameGUIManager.Instance.UpdateKilled(GameController.Instance.CountBirdKilled);
        AudioController.Instance.PlaySound(AudioController.Instance.BirdDie);
        Destroy(gameObject);
        GameObject DeathVfxClone = Instantiate(DeathVFX,transform.position,Quaternion.identity);
        Destroy(DeathVfxClone,1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("DeathZone")) Destroy(gameObject);
    }
}
