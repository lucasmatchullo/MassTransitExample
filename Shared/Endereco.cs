using System;

namespace Shared
{
    public class Endereco
    {
        public Guid Id { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public Guid IdExterno { get; set; }
    }
}