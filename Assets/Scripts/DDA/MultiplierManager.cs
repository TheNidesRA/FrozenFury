using UnityEngine;

namespace Enemies
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
                mults[0] += 0.01f;
                mults[1] += 0.009f;
                mults[2] -= 0.01f;
                // vars[0] += 1;
                vars[1] += 1;
            }else if (healthLoss < 5)
            {
                mults[0] -= 0.001f;
                mults[1] -= 0.003f;
                mults[2] += 0.007f;
            }else if (healthLoss < 10)
            {
                mults[0] -= 0.007f;
                mults[1] -= 0.01f;
                mults[2] += 0.015f;
                // vars[0] -= 2;
                // vars[1] -= 1;
            }else if (healthLoss < 15)
            {
                mults[0] -= 0.03f;
                mults[1] -= 0.015f;
                mults[2] += 0.02f;
                // vars[0] -= 3;
                vars[1] -= 1;
            }
            else
            {
                mults[0] -= 0.09f;
                mults[1] -= 0.03f;
                mults[2] += 0.04f;
                // vars[0] -= 4;
                vars[1] -= 2;
            }
        }

        public void UpdateWithWinnersHealth(float totalHealth, float winnersHealth, float[] mults, ref float globalDiff)
        {
            float healthPercent = winnersHealth / totalHealth;
            
            if (healthPercent == 0)
            {
                mults[0] += 0.01f;
                mults[1] += 0.005f;
                mults[2] -= 0.01f;
                globalDiff += 4;
            }else if (healthPercent < 0.04)
            {
                mults[0] -= 0.002f;
                mults[1] -= 0.002f;
                mults[2] += 0.005f;
                globalDiff -= 1;
            }else if (healthPercent < 0.1)
            {
                mults[0] -= 0.007f;
                mults[1] -= 0.005f;
                mults[2] += 0.01f;
                globalDiff -= 2;
            }else if (healthPercent < 0.2)
            {
                mults[0] -= 0.01f;
                mults[1] -= 0.008f;
                mults[2] += 0.02f;
                globalDiff -= 4;
            }
            else
            {
                mults[0] -= 0.02f;
                mults[1] -= 0.01f;
                mults[2] += 0.05f;
                globalDiff -= 6;
            }

        }

        public void UpdateWIthPlayerSkill(float skill, float[] mults, AnimationCurve curve)
        {
            float val = curve.Evaluate(skill / 10);
            // Debug.Log("Val: " + );
            mults[0] += val;
            mults[1] += val;
            mults[2] += val;

            // if (skill < 3)
            // {
            //     mults[0] += 0.15f;
            //     mults[1] += 0.15f;
            //     mults[2] += 0.5f;
            // }
            // else if (skill < 5)
            // {
            //     mults[0] += 0.25f;
            //     mults[1] += 0.25f;
            //     mults[2] += 0.4f;
            // }
            // else if (skill < 8)
            // {
            //     mults[0] += 0.35f;
            //     mults[1] += 0.35f;
            //     mults[2] += 0.25f;
            // }
            // else
            // {
            //     mults[0] += 0.5f;
            //     mults[1] += 0.5f;
            //     mults[2] += 0.15f;
            // }
        }

    }
}