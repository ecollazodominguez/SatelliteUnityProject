using UnityEngine;
using System.Collections.Generic;

namespace SAT
{
    public class Gun : MonoBehaviour
    {
        [Header("Balística")]
        [Tooltip("Tiempo en segundos entre disparos")]
        [SerializeField] private float fireDelay = 0.15f;
        [Tooltip("El rango en el que el disparo asistido se aplicará si está activado")]
        [SerializeField] [Range(0f, 180f)] private float assistRange = 10f;
        [Tooltip("Velocidad de salida en m/s de una bala disparada.")]
        [SerializeField] private float velocidadSalida = 1500f;

        [SerializeField] public Vector3 inheritedVelocity;

        [Header("Patrón de disparo")]
        [Tooltip("Desviación máximo aleatorio en grados.")]
        [SerializeField] private float deviation = 0.1f;
        [Tooltip("Si usas varios cañones, si deberían disparar por secuencia o a la vez.")]
        [SerializeField] private bool sequentialBarrels = false;
        [Tooltip("Referencia Transform por donde las balas serán invocadas. Múltiple cañones pueden ser asignados. Si no hay ningún cañón asignado las balas saldrán por el centro del gameObject.")]
        [SerializeField] private Transform[] barrels = null;

        [Header("Prefabs")]
        [Tooltip("Prefab de la bala a disparar por el arma.")]
        [SerializeField] private Bullet bulletPrefab = null;
        [Tooltip("Salida del efecto de partícula de luz que aparecerá al disparar desde el arma.")]
        [SerializeField] private ParticleSystem muzzleFlashPrefab = null;

       
        public bool UseGimballedAiming = true;

        private Dictionary<Transform, ParticleSystem> barrelToMuzzleFlash = new Dictionary<Transform, ParticleSystem>();
        private Queue<Transform> barrelQueue = null;
        private float cooldown = 0f;

        public Ship ship;

        // =========================
        // Propiedades del disparo asistido (gimballing)
        // =========================

        /// <summary>
        /// Poner en true para activar. El arma intentará automáticamente apuntar a la localización definida por la propiedad TargetPosition
        /// </summary>

        /// <summary>
        /// Con la propiedad "UseGimballedAiming" ,
        /// el arma intentará apuntar a la posición entre los limites definidor por el campo assistRange en el Inspector de la interfaz.
        /// </summary>
        public Vector3 TargetPosition { get; set; }

        public bool ReadyToFire { get { return (cooldown <= 0f); } }



        private void Awake()
        {

            barrelQueue = new Queue<Transform>(barrels);

            // Añade la particula de luz a cada cañón si no lo tienen.
            if (muzzleFlashPrefab != null)
            {
                foreach (var barrel in barrels)
                {
                    var muzzleFlash = Instantiate(muzzleFlashPrefab, barrel, false).GetComponent<ParticleSystem>();
                    barrelToMuzzleFlash.Add(barrel, muzzleFlash);
                }
            }
        }

        private void Update()
        {
                // Retardo entre un disparo y otro.
                cooldown = Mathf.MoveTowards(cooldown, 0f, Time.deltaTime);
        }




        public void Fire(Vector3 inheritedVelocity)
        {
            if (ReadyToFire == false)
                return;

            if (barrelQueue.Count == 0)
            {
                // Si no se han indicado los cañones usará el transform como backup.
                SpawnAndFireBulletFromBarrel(transform, inheritedVelocity);
            }
            else if (sequentialBarrels)
            {
                // Si es secuencial irá disparando en orden de cola
                var barrel = barrelQueue.Dequeue();
                SpawnAndFireBulletFromBarrel(barrel, inheritedVelocity);
                barrelQueue.Enqueue(barrel);
            }
            else
            {
                // Disparan todos a la vez
                foreach (var barrel in barrelQueue)
                {
                    SpawnAndFireBulletFromBarrel(barrel, inheritedVelocity);
                }
            }

                // Retardo entre un disparo y otro.
            cooldown = fireDelay;
        }

        private void SpawnAndFireBulletFromBarrel(Transform barrel, Vector3 inheritedVelocity)
        {
            Vector3 spawnPos = barrel.position;
            Quaternion aimRotation = barrel.rotation;

            // Dirige la bala al objetivo si se quiere
            if (UseGimballedAiming == true)
            {
                Vector3 gimballedVec = transform.forward;
                gimballedVec = Vector3.RotateTowards(gimballedVec,
                                                     TargetPosition - barrel.position,
                                                     Mathf.Deg2Rad * assistRange,
                                                     1f);
                gimballedVec.Normalize();
                aimRotation = Quaternion.LookRotation(gimballedVec);
            }

            // Aplica la partícula de luz si existe.
            if (barrelToMuzzleFlash.ContainsKey(barrel))
                barrelToMuzzleFlash[barrel].Play();

            // Se instancia y se dispara la bala.
            var bullet = Instantiate(bulletPrefab, spawnPos, aimRotation);
            bullet.Fire(spawnPos, aimRotation, inheritedVelocity, velocidadSalida, deviation);
        }
    }
}
