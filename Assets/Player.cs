using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D fizik; // fizik kuralları

    [SerializeField] // Attribute : Davranış yapıları
    private float hiz;

    private bool sagaBak;

    // Start is called before the first frame update
    void Start()
    {
        sagaBak = true;
        fizik = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Nesnenin x eksenindeki değer float
        // olan "yatay" değişkenine atanır
        float yatay = Input.GetAxis("Horizontal");
        TemelHareketler(yatay);
        YonCevir(yatay);
        //Debug.Log(yatay);
    }

    private void TemelHareketler(float y)
    {
        // velocity = hız
        fizik.velocity = new Vector2(y*hiz, fizik.velocity.y);
    }

    private void YonCevir(float yatay)
    {
        // Objenin yönünün değişme şartı
        if(yatay > 0 && !sagaBak || yatay < 0 && sagaBak)
        {
            sagaBak = !sagaBak;
            Vector3 yon = transform.localScale;
            yon.x *= -1;
            transform.localScale = yon;
            //Debug.Log(sagaBak);
        }
    }


}
