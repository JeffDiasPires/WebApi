using Domain.Validations;
using FluentValidation;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;


namespace Domain.Costumers
{
    public class Client
    {
        [JsonIgnore]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

        [Required]
        [BsonElement("FirstName")]
        public string FirstName { get; set; }

        [Required]
        [BsonElement("LastName")]
        public string LastName { get; set; }

        [Required]
        [BsonElement("Birthday")]
        public DateTime? Birthday { get; set; }
          

        [Required]
        [BsonElement("Age")]
        public int? Age { get; set; }

        [Required]
        [BsonElement("Phone")]
        
        public string Phone { get; set; }

        [Required]
        [BsonElement("Document")]
        public string Document { get; set; }

        [JsonIgnore]
        public string Error = string.Empty;
        
        public class ClientValidator : AbstractValidator<Client>
        {
            public ClientValidator()
            {
                RuleFor(x => x.Age).InclusiveBetween(18, 99).WithMessage("Idade do Cliente Invalida. Insira uma Idade entre 18 a 99 anos");
                RuleFor(x => x.Document)
                .Must(e => DocumentValidation.IsCPF(e))
                .WithMessage("O CPF informado é inválido!");

            }
        }

        public bool IsValid()
        {
            
            var validator = new ClientValidator();
            var validated =  validator.Validate(this);
            if (!validated.IsValid)
                Error = validated.Errors.First().ErrorMessage;

            if (this.Birthday == null)
            return validated.IsValid;

            if (CalculateBirthYearDiff(this.Birthday.Value.Year, DateTime.Now.Year, this.Age.Value))
            return false;

            return validated.IsValid;
        }

        public bool CalculateBirthYearDiff(int BirthdayYear, int CurrentYear, int Age)
        {
            var diff = CurrentYear - BirthdayYear; 
            if (diff != Age)
            {
                Error = "Idade não confere com data de nascimento enviada";
                return true;
            }
            
            return false;
        }

    }
}
