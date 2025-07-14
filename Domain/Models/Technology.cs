using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Interfaces;


namespace Domain.Models
{
    public class Technology : ITechnology
    {
        private const int MaxDescriptionLength = 255;
        public Guid Id { get; set; }
        public string Description { get; set; }

        public Technology()
        {
        }
        public Technology(string description)
        {
            ValidateDescription(description);
            Id = Guid.NewGuid();
            Description = description;
        }
        public Technology(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
        public string ValidateDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Descrição não pode estar vazia.", nameof(description));

            description = description.Trim();

            if (description.Length > MaxDescriptionLength)
                throw new ArgumentException($"Descrição não pode exceder {MaxDescriptionLength} caracteres.");

            return description;
        }
        public void UpdateDescription(string description)
        {
            this.Description = description;
        }
    }
}