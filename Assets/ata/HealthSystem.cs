using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 100; // Maksimum saðlýk
    public int currentHealth; // Mevcut saðlýk
    public float invincibilityDuration = 1.0f; // Ölümsüzlük süresi (saniye)
    private bool isInvincible = false; // Ölümsüzlük durumu
    private Animator animator; // Animator bileþeni

    // Saðlýk deðiþtiðinde tetiklenecek olay
    public delegate void OnHealthChanged(int currentHealth, int maxHealth);
    public event OnHealthChanged onHealthChanged;

    void Start()
    {
        currentHealth = maxHealth; // Karakterin saðlýðýný maksimum saðlýkla baþlat
        animator = GetComponent<Animator>(); // Animator bileþenini al
    }

    // Hasar alacak fonksiyon
    public void TakeDamage(int damage)
    {
        if (!isInvincible)
        {
            currentHealth -= damage; // Hasarý saðlýk miktarýndan çýkar

            // Saðlýk sýfýr veya daha az ise ölümü tetikle
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                // Saðlýk deðiþtiðinde olayý tetikle
                onHealthChanged?.Invoke(currentHealth, maxHealth);

                // Ölümsüzlük süresini baþlat
                isInvincible = true;
                Invoke("DisableInvincibility", invincibilityDuration);

                //// Game object'in tag'i "Player" ise, animatördeki bir bool deðiþkenini true yap
                //if (gameObject.CompareTag("Player"))
                //{
                //    // Burada "isDamaged" adýnda bir bool deðiþkenini true yapabiliriz
                //    animator.SetTrigger("isDamaged");
                    
                //}
            }
        }
    }

    // Ölümsüzlüðü devre dýþý býrakacak fonksiyon
    private void DisableInvincibility()
    {
        isInvincible = false;
    }

    // Ölüme tepki verecek fonksiyon
    private void Die()
    {
        // Ölüm durumunu aktifleþtir
        animator.SetBool("isDead", true); // Ölüm animasyonunu baþlat
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length); // Ölüm animasyonu tamamlandýktan sonra nesneyi yok et

        Debug.Log(gameObject.name + " died!"); // Örneðin, ölüm animasyonu oynatýlabilir veya oyun durdurulabilir
                                               // Burada istediðiniz ölüm iþlemlerini gerçekleþtirebilirsiniz

        // Ölen nesnenin "Player" tag'ine sahip olup olmadýðýný kontrol et
        if (gameObject.CompareTag("Player"))
        {
            // Eðer ölen nesne "Player" tag'ine sahipse, bölümü tekrar baþlat
            Debug.Log("Player died! Restarting level...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Aktif sahneyi tekrar yükle
        }
    }

    // Saðlýk yenileme fonksiyonu
    public void Heal(int amount)
    {
        currentHealth += amount; // Saðlýk miktarýný artýr

        // Maksimum saðlýk miktarýný aþmayacak þekilde güncelle
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Saðlýk deðiþtiðinde olayý tetikle
        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    // Trigger ile etkileþimde bulunulan objeler
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
        // Objede "Heal" etiketi varsa iyileþtir
        else if (other.CompareTag("Heal"))
        {
            HealObject healObject = other.GetComponent<HealObject>();
            if (healObject != null)
            {
                int healAmount = (int)healObject.healAmount;
                Heal(healAmount);
                Destroy(other.gameObject); // Ýyileþtirme objesini yok et (iyileþtirme bir kere kullanýlabilir)
            }
        }
    }
}
