using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface IDamageble
    {
        public int Life { get; set; }
        public float Couldown { get; set; }
        public bool CanBeDamaged { get; set; }

        public void Damage(int lifeToLose);

        public IEnumerator CooldownCounter();
    }
}
