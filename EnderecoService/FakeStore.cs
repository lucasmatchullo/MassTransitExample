using System;
using System.Collections.Generic;
using Shared;

namespace EnderecoService
{
    public class FakeStore
    {
        public List<Endereco> Enderecos { get; set; }

        public FakeStore()
        {
            Enderecos = new List<Endereco>
            {
                new Endereco
                {
                    Id = Guid.Parse("ee0f3c62-106e-4bce-bc34-47aae1420ff9"),
                    IdExterno = Guid.Parse("72561015-2b15-4346-b259-a62740cd5645"),
                    Estado = "Bahia",
                    Cidade = "LEM",
                    Bairro = "Centro",
                    Logradouro = "Av. Brumado",
                    Numero = "24B",
                    Cep = "47850000"
                },
                new Endereco
                {
                    Id = Guid.Parse("efafe09f-a5fc-4686-a6f9-f7a7ce551054"),
                    IdExterno = Guid.Parse("1195657d-4f1f-4480-9f55-1ce3f09a5d1b"),
                    Estado = "Tocantins",
                    Cidade = "DNO",
                    Bairro = "Centro",
                    Logradouro = "Av. Brumado",
                    Numero = "24B",
                    Cep = "77300000"
                }
            };
        }
    }
}