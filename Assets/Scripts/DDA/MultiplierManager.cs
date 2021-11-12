﻿namespace Enemies
{
    public class MultiplierManager
    {
        public MultiplierManager()
        {
        }

        public void UpdateWithGlobalHealth(float[]vars, float[]mults, int healthLoss)
        {
            if (healthLoss == 0)
            {
                mults[0] += 0.07f;
                mults[1] += 0.05f;
                mults[2] -= 0.05f;
                vars[0] += 1;
                vars[2] += 1;
            }else if (healthLoss < 5)
            {
                mults[0] -= 0.1f;
                mults[1] -= 0.08f;
                mults[2] += 0.07f;
            }else if (healthLoss < 10)
            {
                mults[0] -= 0.12f;
                mults[1] -= 0.1f;
                mults[2] += 0.09f;
                vars[0] -= 2;
                vars[2] -= 2;
            }else if (healthLoss < 15)
            {
                mults[0] -= 0.17f;
                mults[1] -= 0.14f;
                mults[2] += 0.12f;
                vars[0] -= 3;
                vars[2] -= 3;
            }
            else
            {
                mults[0] -= 0.12f;
                mults[1] -= 0.1f;
                mults[2] += 0.15f;
                vars[0] -= 2;
                vars[2] -= 2;
            }
        }

        public void UpdateWithWinnersHealth(float totalHealth, float winnersHealth, float[] mults, ref float globalDiff)
        {
            float healthPercent = winnersHealth / totalHealth;
            
            if (healthPercent == 0)
            {
                mults[0] += 0.06f;
                mults[1] += 0.05f;
                mults[2] -= 0.04f;
                globalDiff += 4;
            }else if (healthPercent < 0.04)
            {
                mults[0] -= 0.04f;
                mults[1] -= 0.03f;
                mults[2] += 0.02f;
                globalDiff -= 2;
            }else if (healthPercent < 0.1)
            {
                mults[0] -= 0.05f;
                mults[1] -= 0.04f;
                mults[2] += 0.03f;
                globalDiff -= 4;
            }else if (healthPercent < 0.2)
            {
                mults[0] -= 0.07f;
                mults[1] -= 0.05f;
                mults[2] += 0.05f;
                globalDiff -= 6;
            }
            else
            {
                mults[0] -= 0.1f;
                mults[1] -= 0.075f;
                mults[2] += 0.075f;
                globalDiff -= 8;
            }

        }
        
        
    }
}