using System;

namespace HealthInstitution.Exceptions
{
    class IngredientInUseException : Exception
    {
        public IngredientInUseException() : base()
        {

        }

        public IngredientInUseException(string message) : base(message)
        {
        }
    }
}
