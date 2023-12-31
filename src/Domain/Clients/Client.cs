﻿using Domain.Validations;
using FluentValidation;
using FluentValidation.TestHelper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Domain.Costumers
{
    public class Client
    {
        [JsonIgnore]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

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


        [Required]
        [BsonElement("Email")]
        public string Email { get; set; }

        [JsonIgnore]
        [BsonIgnore]
        public Errors Error { get; set; }

        public class EmailValidation : AbstractValidator<string>
        {
            public EmailValidation()
            {
                RuleFor(x => x)
                .Must(x => x.IsEmail())
                .WithMessage("O Email informado é inválido!");
            }
        }

        public bool EmailIsValid()
        {
            var validator = new EmailValidation();
            var validated = validator.Validate(this.Email);
            if (!validated.IsValid)
                Error = new() { Message = validated.Errors.First().ErrorMessage };

            return validated.IsValid;


        }

        public class ClientValidator : AbstractValidator<Client>
        {
            public ClientValidator()
            {
                RuleFor(x => x.Age).InclusiveBetween(18, 99).WithMessage("Idade do Cliente Invalida. Cliente precisa ser maior de idade");
            }

        }

        public class IdValidador : AbstractValidator<string> 
        {
            public IdValidador() {

                RuleFor(x => x).Must(x => x.IsHexa())
                .WithMessage("Formato do Id invalido");
            }
        }

        public bool IdIsValid()
        {
            var validator = new IdValidador();
            var validated = validator.Validate(this._id);

            if (!validated.IsValid)
                Error = new() { Message = validated.Errors.First().ErrorMessage };

            return validated.IsValid;

        }

        public class DocumentValidation : AbstractValidator<string>
        {
            public DocumentValidation()
            {
                RuleFor(x => x)
                .Must(x => x.IsCPF())
                .WithMessage("O CPF informado é inválido!"); 
            }
        }

        public bool DocumentIsValid() 
        {
           var validator = new DocumentValidation();
           var validated = validator.Validate(this.Document);
            if (!validated.IsValid)
                Error = new() { Message = validated.Errors.First().ErrorMessage };

            return validated.IsValid;

        }

        public bool IsValid()
        {
            var validator = new ClientValidator();
            var validated =  validator.Validate(this);
           

            if (!validated.IsValid)
                Error = new() { Message = validated.Errors.First().ErrorMessage };

            if (this.Birthday == null)
            return validated.IsValid;

            if (CalculateBirthYearDiff(this.Birthday.Value.Year, DateTime.Now.Year, this.Age.Value))
            return false;

            return validated.IsValid;
        }

        public bool CalculateBirthYearDiff(int BirthdayYear, int CurrentYear, int Age)
        {
            var diff = CurrentYear - BirthdayYear; 
            if (diff != Age || diff == 0)
            {
                Error = new() { Message = "Idade não confere com data de nascimento enviada" };
                return true;
            }
            
            return false;
        }

        public class Errors
        {
            public string Message { get; set; }
        }

    }
}
