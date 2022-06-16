using System;

namespace HealthInstitution.Core.Exceptions
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
