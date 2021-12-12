using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TheKiwiCoder;
#if UNITY_EDITOR
using UnityEditor;


#endif


namespace Enemies
{
    public abstract class Enemy : MonoBehaviour
    {
        public GameObject particle;
        public Vector3 particleScale = new Vector3(1, 1, 1);
        private Vector3 particleOffset;
        public float particleXPosition = -1f;
        public float particleYPosition = -1f;
        public float particleZPosition = -1f;


        /// <summary>
        /// Enemy id
        /// </summary>
        [SerializeField] protected string id;

        /// <summary>
        /// Initial enemy stats
        /// </summary>
        [SerializeField] private InitStats _initStats;

        /// <summary>
        /// Agente navmesh que moverá al enemigo
        /// </summary>
        public NavMeshAgent NavMeshAgent;

        private bool isAttacking = false;

        #region EventosBossy

        public event Action<GameObject> OnEnemyDeath;
        public event Action<GameObject> OnHealthChanged;

        #endregion

        public event EventHandler<float> HealthBarEvent;

        public float initSpeed;
        public float initAcceleration;

        public string Id => id;

        public float maxHealth;
        public float health;
        public float damage;

        [SerializeField] private float _speed;
        [SerializeField] private float _acceleration;

        public float timeWaitAnim = 1.0f;

        private Animator characterAnimator;

        public Transform particlePos;

        public float speed
        {
            get => _speed;
            set
            {
                NavMeshAgent.speed = value;
                _speed = value;
                // Debug.Log("Velociada cambiaadaa: " + _speed);
            }
        }

        public float acceleration
        {
            get => _acceleration;
            set
            {
                NavMeshAgent.acceleration = value;
                _acceleration = value;
                // Debug.Log("Aceleracion cambiaadaa: " + _acceleration);
            }
        }


        public float armor;
        public float attackSpeed;
        public float gold;
        public float attackRange;
        public float baseDamage;
        public bool invencibilidadTrampa = false;
        public float tiempoInvencibilidad = 5f;


        public bool afecctedTrap = false;


        //Cosas añadidads por mi elnidas para lo del bt

        public Vector3 targetPosition;
        public GameObject actionTarget;
        public GameObject auxActionTarget;
        public float angleVision;
        public float radioVision;
        public float innerAngleVision;
        public float innerRadioVision;

        public string NODOACTUAL;
        public Image ActionImage;

        public BehaviourTreeRunner BehaviourTreeRunner = null;

        public Collider collider;

        private void Awake()
        {
            actionTarget = null;
            auxActionTarget = null;
            NavMeshAgent.speed = _speed;
            NavMeshAgent.acceleration = acceleration;
            characterAnimator = gameObject.GetComponent<Animator>();
        }


        [ContextMenu("calcula distancia")]
        public void CalcDist()
        {
            Debug.Log("La distancia es de " + Vector3.Distance(EnemyGoal.instance.getPosition(), transform.position));
        }

        public void UpdateStats(EnemyStats stats)
        {
            health = stats.hp;
            maxHealth = stats.hp;
            damage = stats.dmg;
            speed = stats.speed;
            armor = stats.armor;
            attackSpeed = stats.atackSpd;

            // PrintStats();
            // Debug.Log("init :_ +" + initSpeed);
            initSpeed = speed; //AUX PARA LOS SLOWS Y SPEEDS
        }

        public void InitializeStats()
        {
            health = _initStats.initHp;
            maxHealth = _initStats.initHp;
            damage = _initStats.initDmg;
            speed = _initStats.initSpd;
            armor = _initStats.initArm;
            attackSpeed = _initStats.initAtkSpd;
            baseDamage = _initStats.initBaseDamage;
            gold = _initStats.gold;
            initSpeed = _initStats.initSpd;
            initAcceleration = _initStats.initAcceleration;
            attackRange = _initStats.initattackRange;
            angleVision = _initStats.initangleVision;
            radioVision = _initStats.initradioVision;
            acceleration = _initStats.initAcceleration;
            innerAngleVision = _initStats.initInnerAngleVision;
            innerRadioVision = _initStats.initInnetRadioVision;

        }


        public bool OnHit(float dmg)
        {
            health -= dmg;
            OnHealthChanged?.Invoke(gameObject);
            HealthBarEvent?.Invoke(maxHealth, health);
            AudioManager.Instance?.PlayRandomEnemyHit();
            return health <= 0;
        }

        virtual public void Die()
        {
            NavMeshAgent.isStopped = true;
            NavMeshAgent.ResetPath();
            if (!ReferenceEquals(BehaviourTreeRunner, null))
                BehaviourTreeRunner.enabled = false;
            //OnEnemyDeath?.Invoke(gameObject);
            StartCoroutine(dieAnim());
        }

        public void DieNoAnim()
        {
            Destroy(gameObject);
            //OnEnemyDeath?.Invoke(gameObject);
            if (PlayerPrefs.GetInt("particlesActivated") == 1)
                ParticleManager.Instance?.PlayEnemyDeathParticles(transform.position);
        }

        public IEnumerator dieAnim()
        {
            characterAnimator.SetBool("Die", true);
            collider.enabled = false;

            yield return new WaitForSeconds(timeWaitAnim);
            if (particlePos != null)
            {
                if (PlayerPrefs.GetInt("particlesActivated") == 1)
                    ParticleManager.Instance?.PlayEnemyDeathParticles(particlePos.position);
            }
            else
            {
                if (PlayerPrefs.GetInt("particlesActivated") == 1)
                    ParticleManager.Instance?.PlayEnemyDeathParticles(transform.position);
            }

            Destroy(gameObject);
        }

        public void OnSlow(float slowDown)
        {
            if (!afecctedTrap)
            {
                speed = speed * slowDown;
            }

            afecctedTrap = true;
        }

        public void OnSpedUp(float speedUp)
        {
            speed = speed * speedUp;
            acceleration = acceleration * speedUp;
        }

        public void OnResetSlow()
        {
            speed = initSpeed;
            acceleration = initAcceleration;

            afecctedTrap = false;
        }

        public void OnHitTrap(float dmg)
        {
            if (!invencibilidadTrampa && health > 0)
            {
                health -= dmg;
                StartCoroutine(OnInvencible());
            }


            if (health <= 0)
            {
                Die();
            }
        }


        public IEnumerator OnInvencible()
        {
            invencibilidadTrampa = true;
            yield return new WaitForSeconds(tiempoInvencibilidad);
            invencibilidadTrampa = false;
        }


        private void OnDestroy()
        {
            PlayerStats._instance.gold += (int) gold;
            OnEnemyDeath?.Invoke(gameObject);
            WaveController._instance.EnemyDeath();
        }

        public void PrintStats()
        {
            Debug.Log(Id + "\nHP: " + health + " // Dmg: " + damage + " // Spd: " + speed + " // Arm: " + armor +
                      " // AtkSpd: " + attackSpeed);
        }

        public void Attack()
        {
            if (isAttacking) return;
            isAttacking = true;
            // Debug.Log("Ataca!");
            StartCoroutine(nameof(StartAnimation));
        }

        public bool IsAttackFinished()
        {
            return !isAttacking;
        }

        private IEnumerator StartAnimation()
        {
            yield return new WaitForSeconds(attackSpeed);
            isAttacking = false;
        }


        private void OnDrawGizmos()
        {
            // if (NavMeshAgent.hasPath)
            // {
            //     if (actionTarget != null)
            //     {
            //         GUIStyle v = new GUIStyle();
            //         v.fontSize = 20;
            //         v.fontStyle = FontStyle.Bold;
            //         //Handles.Label(transform.position, actionTarget.name, v);
            //     }
            //
            //
            //     Vector3 pos = transform.position;
            //     pos.y += 15;
            //     pos.x -= 3;
            //
            //     GUIStyle style = new GUIStyle();
            //     style.normal.textColor = Color.blue;
            //     style.fontSize = 20;
            //     style.fontStyle = FontStyle.Bold;
            //
            //     //Handles.Label(pos, NODOACTUAL, style);
            //
            //
            //     for (int i = 0; i < NavMeshAgent.path.corners.Length - 1; i++)
            //     {
            //         //Handles.color = Color.green;
            //         //Handles.DrawLine(NavMeshAgent.path.corners[i], NavMeshAgent.path.corners[i + 1]);
            //     }
            // }
        }

        void OnDrawGizmosSelected()
        {
            float halfFOV = angleVision / 2.0f;
            Quaternion leftRayRotation = Quaternion.AngleAxis(-halfFOV, Vector3.up);
            Quaternion rightRayRotation = Quaternion.AngleAxis(halfFOV, Vector3.up);
            Vector3 leftRayDirection = leftRayRotation * transform.forward;
            Vector3 rightRayDirection = rightRayRotation * transform.forward;
            Gizmos.DrawRay(transform.position, leftRayDirection * radioVision);
            Gizmos.DrawRay(transform.position, rightRayDirection * radioVision);
            
            float halfFOVInner = innerAngleVision / 2.0f;
            Quaternion leftRayRotationInner = Quaternion.AngleAxis(-halfFOVInner, Vector3.up);
            Quaternion rightRayRotationInner = Quaternion.AngleAxis(halfFOVInner, Vector3.up);
            Vector3 leftRayDirectionInner = leftRayRotationInner * transform.forward;
            Vector3 rightRayDirectionInner = rightRayRotationInner * transform.forward;
            
            Gizmos.DrawRay(transform.position, leftRayDirectionInner * innerRadioVision);
            Gizmos.DrawRay(transform.position, rightRayDirectionInner * innerRadioVision);


            // Gizmos.DrawSphere(transform.position,AttackRange);
        }


        public static float GetPathRemainingDistance(NavMeshAgent navMeshAgent)
        {
            if (navMeshAgent.pathPending ||
                navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
                navMeshAgent.path.corners.Length == 0)
                return -1f;

            float distance = 0.0f;
            for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i)
            {
                distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
            }

            return distance;
        }
    }
}

//
// #if UNITY_EDITOR
//     [CustomEditor(typeof(Enemy))]
//     class EnemyGolemEditor : Editor
//     {
//         public override void OnInspectorGUI()
//         {
//             base.OnInspectorGUI();
//             var script = (EnemyGolem)target;
//             if (script == null) return;
//
//             EditorGUILayout.Space();
//
//             if (script.NavMeshAgent.hasPath)
//             {
//                 EditorGUILayout.LabelField("Distacia al objetivo: " +
//                                            Vector3.Distance(script.NavMeshAgent.destination, script.transform.position)
//                                                .ToString());
//
//                 EditorGUILayout.LabelField("Distacia camino: " + GetPathRemainingDistance(script.NavMeshAgent));
//                 EditorGUILayout.LabelField("Distacia ubi: " + script.NavMeshAgent.destination);
//                 EditorGUILayout.LabelField("Path status: " + script.NavMeshAgent.pathStatus);
//                 EditorGUILayout.LabelField("Path Activo? : " + script.NavMeshAgent.isStopped);
//             }
//
//             // if (script.tr!=null)
//             // {
//             //     EditorGUILayout.LabelField("Arbol status: " + script.tr.tree.treeState);
//             //     EditorGUILayout.LabelField("Arbol nombre?: " + script.tr.tree.rootNode.position);
//             //     EditorGUILayout.LabelField("Arbol Description?: " + script.tr.tree.rootNode.description);
//             //     EditorGUILayout.LabelField("Arbol asd?: " + script.tr.tree.name);
//             //     EditorGUILayout.LabelField("Arbol asd?: " + script.tr.tree.rootNode.guid);
//             //     //script.tr.tree.nodes.Find(n => n.guid ==script.tr.tree.)
//             // }
//
//
//             Handles.color = Color.white;
//             Handles.DrawWireArc(script.transform.position, Vector3.up, Vector3.forward, 360, script.radioVision);
//
//             Vector3 viewAngle01 = DirectionFromAngle(script.transform.eulerAngles.y, -script.angleVision / 2);
//             Vector3 viewAngle02 = DirectionFromAngle(script.transform.eulerAngles.y, script.angleVision / 2);
//
//             Handles.color = Color.yellow;
//             Handles.DrawLine(script.transform.position, script.transform.position + viewAngle01 * script.radioVision);
//             Handles.DrawLine(script.transform.position, script.transform.position + viewAngle02 * script.radioVision);
//
//             if (script.actionTarget != null)
//             {
//                 Handles.color = Color.green;
//                 Handles.DrawLine(script.transform.position, script.actionTarget.transform.position);
//             }
//         }
//
//
//         private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
//         {
//             angleInDegrees += eulerY;
//
//             return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
//         }
//
//         public float GetPathRemainingDistance(NavMeshAgent navMeshAgent)
//         {
//             if (navMeshAgent.pathPending ||
//                 navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid ||
//                 navMeshAgent.path.corners.Length == 0)
//                 return -1f;
//
//             float distance = 0.0f;
//             for (int i = 0; i < navMeshAgent.path.corners.Length - 1; ++i)
//             {
//                 distance += Vector3.Distance(navMeshAgent.path.corners[i], navMeshAgent.path.corners[i + 1]);
//             }
//
//             return distance;
//         }
//     }
// #endif
// }