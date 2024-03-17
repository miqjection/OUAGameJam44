using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float attackRange = 1f;
    public int damage = 10;
    public float attackCooldown = 4f;
    public LayerMask playerLayer;
    public int maxHealth = 100;
    public float patrolDistance = 5f; // Patrol alan�n�n geni�li�i

    private Animator animator;
    private bool isWalking = false;
    private bool isAttacking = false;
    private bool isDead = false;
    private int currentHealth;
    private bool movingRight = true; // Ba�lang��ta sa�a do�ru hareket edilsin

    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        InvokeRepeating("PlayerAttack", 0f, attackCooldown);
        StartCoroutine("Patrol"); // Patrul Coroutine'unu ba�lat
    }

    void Update()
    {
        // D��man �lmediyse ve y�r�meye ba�lamad�ysa ve patrolling modunda ise
        if (!isDead && !isAttacking && isWalking)
        {
            animator.SetBool("iswalk", true);
            // Hareket y�n�ne g�re hareket ettirme
            if (movingRight)
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            else
                transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
        }
    }

    IEnumerator Patrol()
    {
        // D��man ya�am�yor oldu�u s�rece patrul et
        while (!isDead)
        {
            isWalking = true; // Patrolling moduna gir
            // Rastgele bir s�re bekle
            yield return new WaitForSeconds(Random.Range(1f, 4f));

            // Rastgele bir y�ne do�ru hareket et
            // E�er d��man sa�a do�ru hareket ediyorsa, sol tarafa gitmesi gerekiyorsa
            if (movingRight)
                movingRight = false;
            else // E�er d��man sol tarafa hareket ediyorsa, sa�a gitmesi gerekiyorsa
                movingRight = true;
            Flip(); // Y�n� �evir
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void PlayerAttack()
    {
        Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        foreach (Collider2D player in hitPlayers)
        {
            HealthSystem playerHealth = player.GetComponent<HealthSystem>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isWalking = false; // Hareket etmeyi durdur
            isAttacking = true; // Sald�r� moduna ge�
            animator.SetBool("iswalk", false);
            animator.SetBool("isattack", true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttacking = false; // Sald�r� modunu sonland�r
            animator.SetBool("isattack", false);
            animator.SetBool("iswalk", true);
            isWalking = true; // Patrolling moduna geri d�n
        }
    }
}
