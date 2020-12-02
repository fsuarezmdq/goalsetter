using Goalsetter.Domains;
using System;

namespace Goalsetter.AppServices.Dtos
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public ClientDto(Client client)
        {
            Id = client.Id;
            ClientName = client.ClientName;
            Email = client.Email;
            CreatedDate = client.CreatedDate;
            UpdatedDate = client.UpdatedDate;
        }
    }
}
