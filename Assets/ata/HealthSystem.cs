using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum sa�l�k
    public int currentHealth; // Mevcut sa�l�k
    public float invincibilityDuration = 1.0f; // �l�ms�zl�k s�resi (saniye)
    private bool isInvincible = false; // �l�ms�zl�k durumu
    private Animator animator; // Animator bile�eni

    // Sa�l�k de�i�ti�inde tetiklenecek olay
    public delegate void OnHealthChanged(int currentHealth, int maxHealth);
    public event OnHealthChanged onHealthChanged;

    void Start()
    {
        currentHealth = maxHealth; // Karakterin sa�l���n� maksimum sa�l�kla ba�lat
        animator = GetComponent<Animator>(); // Animator bile�enini al
    }

    // Hasar alacak fonksiyon
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage; // Hasar� sa�l�k miktar�ndan ��kar

            // Sa�l�k s�f�r veya daha az ise �l�m� tetikle
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                // Sa�l�k de�i�ti�inde olay� tetikle
                onHealthChanged?.Invoke(currentHealth, maxHealth);

                // �l�ms�zl�k s�resini ba�lat
                isInvincible = true;
                Invoke("DisableInvincibility", invincibilityDuration);

                //// Game object'in tag'i "Player" ise, animat�rdeki bir bool de�i�kenini true yap
                //if (gameObject.CompareTag("Player"))
                //{
                //    // Burada "isDamaged" ad�nda bir bool de�i�kenini true yapabiliriz
                //    animator.SetTrigger("isDamaged");
                    
                //}
            }
        }
    }

    // �l�ms�zl��� devre d��� b�rakacak fonksiyon
    private void DisableInvincibility()
    {
        isInvincible = false;
    }

    // �l�me tepki verecek fonksiyon
    private void Die()
    {
        // �l�m durumunu aktifle�tir
        animator.SetBool("isDead", true); // �l�m animasyonunu ba�lat
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length); // �l�m animasyonu tamamland�ktan sonra nesneyi yok et

        Debug.Log(gameObject.name + " died!"); // �rne�in, �l�m animasyonu oynat�labilir veya oyun durdurulabilir
                                               // Burada istedi�iniz �l�m i�lemlerini ger�ekle�tirebilirsiniz

        // �len nesnenin "Player" tag'ine sahip olup olmad���n� kontrol et
        if (gameObject.CompareTag("Player"))
        {
            // E�er �len nesne "Player" tag'ine sahipse, b�l�m� tekrar ba�lat
            Debug.Log("Player died! Restarting level...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Aktif sahneyi tekrar y�kle
        }
    }

    // Sa�l�k yenileme fonksiyonu
    public void Heal(int amount)
    {
        currentHealth += amount; // Sa�l�k miktar�n� art�r

        // Maksimum sa�l�k miktar�n� a�mayacak �ekilde g�ncelle
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Sa�l�k de�i�ti�inde olay� tetikle
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // Trigger ile etkile�imde bulunulan objeler
    void OnTriggerEnter(Collider other)
    {
        // Objede "Damage" etiketi varsa hasar al
        if (other.CompareTag("Damage"))
        {
            DamageObject damageObject = other.GetComponent<DamageObject>();
            if (damageObject != null)
            {
                float damage = damageObject.damageAmount;
                TakeDamage((int)damage);
            }
        }
        // Objede "Heal" etiketi varsa iyile�tir
        else if (other.CompareTag("Heal"))
        {
            HealObject healObject = other.GetComponent<HealObject>();
            if (healObject != null)
            {
                int healAmount = (int)healObject.healAmount;
                Heal(healAmount);
                Destroy(other.gameObject); // �yile�tirme objesini yok et (iyile�tirme bir kere kullan�labilir)
            }
        }
    }
}
