namespace Enemies
{
    public class StatCalculator
    {
        public void UpdateVariables(float[] vars, float[] mults)
        {
            for (int i = 0; i < vars.Length; i++)
            {
                vars[i] *= mults[i];
            }
        }
        
        
    }
}