using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Goalsetter.Domains.ValueObjects;

namespace Goalsetter.Domains
{
    public sealed class Client: AggregateRoot
    {
        public ClientName ClientName { get; }
        public Email Email { get; }
        private readonly List<Rental> _rentals = new List<Rental>();
        public IReadOnlyList<Rental> Rentals => _rentals.ToList();
        private Client()
        {
        }

        public Client(Guid id, ClientName clientName, Email email, DateTime createdDate, DateTime updatedDate,
            bool isActive)
        {
            Id = (id == default) ? throw new ArgumentNullException(nameof(id)) : id;
            ClientName = clientName ?? throw new ArgumentNullException(nameof(clientName));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            CreatedDate = createdDate;
            UpdatedDate = updatedDate;
            IsActive = isActive;
        }


        public static Result<Client> Create(ClientName clientName, Email email, Guid id = default)
        {
            var guid = GetId(id);

            return Result.Success(new Client(guid, clientName, email, DateTimeNow,
                DateTimeNow, true));
        }
        public string CanRemove()
        {
            return (IsActive)
                ? string.Empty
                : "The vehicle is already removed.";
        }

        public void Remove()
        {
            var validation = CanRemove();
            if (validation != string.Empty)
            {
                throw new ValidationException(validation);
            }

            //Remove all active rentals
            if (_rentals.Any())
            {
                _rentals.ForEach(p =>
                {
                    if (p.IsActive)
                    {
                        p.Remove();
                    }
                });
            }

            IsActive = false;
        }
    }
}
