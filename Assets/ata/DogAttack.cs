using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRange = 1f; // Saldýrý menzili
    public int damage = 100; // Hasar miktarý
    public LayerMask enemyLayer; // Düþmanlarýn katmaný
    public float cooldownTime = 1f; // Saldýrýlar arasý cooldown süresi

    private Animator animator; // Animator bileþeni
    private bool canAttack = true; // Saldýrý yapýlabilir durum

    void Start()
    {
        animator = GetComponent<Animator>(); // Animator bileþenini al
    }

    void Update()
    {
        if (canAttack && Input.GetKeyDown(KeyCode.E))
        {
            Attack(); // Saldýrýyý tetikle
        }
    }

    void Attack()
    {
        canAttack = false; // Saldýrý yapma iznini kaldýr

        animator.SetBool("isAttack", true); // Animator'deki "isAttacking" bool deðerini true yap

        // Belirlenen menzildeki düþmanlarý tespit et
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);

        // Her tespit edilen düþmana hasar ver
        foreach (Collider2D enemy in hitEnemies)
        {
            // Düþmanýn saðlýk sistemini al
            HealthSystem enemyHealth = enemy.GetComponent<HealthSystem>();

            // Hasar verebilecek bir düþman ise hasar ver
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }

        // Cooldown süresi sonunda saldýrý yapýlabilir duruma getir
        Invoke("ResetAttack", cooldownTime);
    }

    // Saldýrý bitince animasyonu durdur ve tekrar saldýrýya izin ver
    void ResetAttack()
    {
        animator.SetBool("isAttack", false); // Animator'deki "isAttacking" bool deðerini false yap
        canAttack = true; // Saldýrý yapma iznini geri ver
    }

    // Saldýrý menzilini görselleþtirmek için çizim iþlevi
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
