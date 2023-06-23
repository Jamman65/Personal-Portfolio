using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JO
{



    public abstract class State : MonoBehaviour
    {
        public abstract State Tick(EnemyController enemyController, EnemyStats enemyStats, EnemyAnimator enemyAnimator, EnemyManager enemy);
    }
}

