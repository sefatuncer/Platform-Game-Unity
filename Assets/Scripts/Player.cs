using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D fizik; // fizik kuralları
    private Animator myAnimator;

    [SerializeField] // Attribute : Davranış yapıları
    private float hiz;

    [SerializeField]
    private Text toplamSkor;

    [SerializeField]
    private GameObject anahtarVar;

    [SerializeField]
    private GameObject kapiAcik;

    [SerializeField]
    private AudioSource altinSes;

    [SerializeField]
    private AudioSource anahtarSes;


    private bool sagaBak;
    private int skor;

    private bool atak;
    private bool zipla;
    private bool kayma;

    // Start is called before the first frame update
    void Start()
    {
        skor = 0;
        sagaBak = true;
        fizik = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Kontroller();
    }

    private void FixedUpdate()
    {        
        // Nesnenin x eksenindeki değer float
        // olan "yatay" değişkenine atanır
        float yatay = Input.GetAxis("Horizontal");
        
        TemelHareketler(yatay);
        YonCevir(yatay);
        AtakHareketleri();
        DegerleriResetle();
        //Debug.Log(yatay);
    }

    // Çarpışma olduğu zaman kesinlikle çalışan fonksiyon
    // other = Kendisine çarpılan obje
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "altin")
        {
            other.gameObject.SetActive(false); // çarpılan objeyi yok et
            skor = skor + 100;
            SkorAyarla(skor);
            altinSes.Play();
        }
        if(other.gameObject.tag == "anahtar")
        {
            other.gameObject.SetActive(false);
            anahtarVar.SetActive(true);
            kapiAcik.SetActive(true);
            anahtarSes.Play();
        }

    }

    private void TemelHareketler(float yatay)
    {
        if(!myAnimator.GetBool("kayma") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("atak"))
        {
            fizik.velocity = new Vector2(yatay*hiz,fizik.velocity.y);
        }
        if(kayma && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("kayma", true);
        }
        else if(!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
        {
            myAnimator.SetBool("kayma", false);
        }
        myAnimator.SetFloat("karakterHizi",Mathf.Abs(yatay));
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

    private void SkorAyarla(int count)
    {
        toplamSkor.text = "Skor : " + count.ToString();
    }
    // trigger = tetikleyici
    private void AtakHareketleri()
    {
        if(atak && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("atakTrigger"))
        {
            myAnimator.SetTrigger("atakTrigger");
            fizik.velocity = Vector2.zero;
        }
    }

    private void Kontroller()
    {
        if (Input.GetKeyDown (KeyCode.T))
        {
            atak = true;
        }

        if(Input.GetKeyDown (KeyCode.Space))
        {
            zipla = true;
        }
        if(Input.GetKeyDown (KeyCode.Y))
        {
            kayma = true;
        }
    }

    private void DegerleriResetle()
    {
        atak = false;
        zipla = false;
        kayma = false;
    }

}
