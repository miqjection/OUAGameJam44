using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1f; // Sald�r� menzili
    public int damage = 100; // Hasar miktar�
    public LayerMask enemyLayer; // D��manlar�n katman�
    public float cooldownTime = 1f; // Sald�r�lar aras� cooldown s�resi

    private Animator animator; // Animator bile�eni
    private bool canAttack = true; // Sald�r� yap�labilir durum

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator bile�enini al
    }

    void Update()
    {
        if (canAttack && Input.GetKeyDown(KeyCode.E))
        {
            Attack(); // Sald�r�y� tetikle
        }
    }

    void Attack()
    {
        canAttack = false; // Sald�r� yapma iznini kald�r

        animator.SetBool("isAttack", true); // Animator'deki "isAttacking" bool de�erini true yap

        // Belirlenen menzildeki d��manlar� tespit et
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        // Her tespit edilen d��mana hasar ver
        foreach (Collider2D enemy in hitEnemies)
        {
            // D��man�n sa�l�k sistemini al
            HealthSystem enemyHealth = enemy.GetComponent<HealthSystem>();

            // Hasar verebilecek bir d��man ise hasar ver
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Cooldown s�resi sonunda sald�r� yap�labilir duruma getir
        Invoke("ResetAttack", cooldownTime);
    }

    // Sald�r� bitince animasyonu durdur ve tekrar sald�r�ya izin ver
    void ResetAttack()
    {
        animator.SetBool("isAttack", false); // Animator'deki "isAttacking" bool de�erini false yap
        canAttack = true; // Sald�r� yapma iznini geri ver
    }

    // Sald�r� menzilini g�rselle�tirmek i�in �izim i�levi
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
