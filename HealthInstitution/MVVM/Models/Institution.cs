namespace HealthInstitution.MVVM.Models
{
    public class Institution
    {
        private static Institution s_instance;

        protected Institution() { }

        public static Institution Instance()
        {
            if (s_instance == null)
            {
                s_instance = new Institution();
            }
            return s_instance;
        }
    }
}
