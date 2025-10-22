namespace DND.Domain.SharedKernel
{
    /// <summary>
    /// Speed value object representing movement speed in DND
    /// It can be divided into three properties: walking, swimming and flying
    /// Almost all creatures have walking speed>0, but flying and swimming are often 0
    /// Each speed type is represented in feet per round with an integer value
    /// </summary>
    public class Speed
    {
        private int _walking = 30; // Default walking speed is 30 feet per round
        private int _swimming = 0; // Default swimming speed is 0 feet per round
        private int _flying = 0;   // Default flying speed is 0 feet per round

        public int Walking { get => _walking; set => _walking = value; }
        public int Swimming { get => _swimming; set => _swimming = value; }
        public int Flying { get => _flying; set => _flying = value; }

        // Default Constructor
        public Speed()
        {
            _walking = Walking;
            _swimming = Swimming;
            _flying = Flying;
        }

        // Constructor to initialize specific speeds
        public Speed(int walking, int swimming = 0, int flying = 0)
        {
            if (walking < 0 || swimming < 0 || flying < 0)
                throw new ArgumentException("Speed values cannot be negative.");
            Walking = walking;
            Swimming = swimming;
            Flying = flying;
        }

        // Method to update walking speed, returns true if the update was successful
        public bool UpdateWalking(int newWalking)
        {
            if (newWalking < 0) return false;
            Walking = newWalking;
            return true;
        }

        // Method to update swimming speed, returns true if the update was successful
        public bool UpdateSwimming(int newSwimming)
        {
            if (newSwimming < 0) return false;
            Swimming = newSwimming;
            return true;
        }

        // Method to update flying speed, returns true if the update was successful
        public bool UpdateFlying(int newFlying)
        {
            if (newFlying < 0) return false;
            Flying = newFlying;
            return true;
        }

    }
}
