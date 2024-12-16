using Application.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public abstract class EntityBase
    {
        [Key]
        public int ID { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime? Updated { get; set; }
        public bool Removed { get; private set; } = false;


        public virtual void Validate()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this);

            bool isValid = Validator.TryValidateObject(this, validationContext, validationResults, validateAllProperties: true);
            if (!isValid)
            {
                throw new ValidacaoException($"Validation failed for {GetType().Name} object: " + string.Join(", ", validationResults));
            }
        }

        public void Remove()
        {
            Removed = true;
        }
    }
}
