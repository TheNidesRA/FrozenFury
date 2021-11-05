namespace Enemies
{
    public class MultiplierManager
    {
        public void UpdateWithGlobalHealth(ref float[]mults, int healthLoss)
        {
            if (healthLoss == 0)
            {
                
            }else if (healthLoss < 5)
            {
                
            }else if (healthLoss < 10)
            {
                
            }else if (healthLoss < 15)
            {
                
            }
            else
            {
                
            }
        }

        public void UpdateWithWinnersHealth(float totalHealth, float winnersHealth, ref float[] mults)
        {
            float healthPrcnt = winnersHealth / totalHealth;
            
            if (healthPrcnt == 0)
            {
                
            }else if (healthPrcnt < 0.04)
            {
                
            }else if (healthPrcnt < 0.1)
            {
                
            }else if (healthPrcnt < 0.2)
            {
                
            }
            else
            {
                
            }

        }
    }
}