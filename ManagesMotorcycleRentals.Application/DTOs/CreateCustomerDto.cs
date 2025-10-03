namespace ManagesMotorcycleRentals.Application.DTOs
{
    public class CreateCustomerDto
    {
        

        public string Name { get; set; }
        public string Cnpj { get; set; }
        public DateTime BirthDate { get; set; }
        public string CnhNumber { get; set; }
        public string CnhType { get; set; }
        public string CnhImage { get; set; }

        private static readonly int[] Pesos1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        private static readonly int[] Pesos2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        public void ValidateCnpj()
        {
            if (Cnpj.Length != 14 || !Cnpj.All(char.IsDigit))
            {
                throw new ArgumentException("CNPJ inválido. Deve conter 14 dígitos numéricos.");
            }

            if (!IsValid(Cnpj))
            {
                throw new ArgumentException("CNPJ inválido.");
            }

            bool IsValid(string? input)
            {
                if (string.IsNullOrWhiteSpace(input)) return false;
                var d = new string(input.Where(char.IsDigit).ToArray());
                if (d.Length != 14) return false;
                if (new string(d[0], 14) == d) return false;

                return d[12] == DV(d[..12], Pesos1) &&
                       d[13] == DV(d[..13], Pesos2);
            };

            char DV(string n, int[] pesos)
            {
                int soma = 0;
                for (int i = 0; i < pesos.Length; i++)
                    soma += (n[i] - '0') * pesos[i];
                int resto = soma % 11;
                int dig = resto < 2 ? 0 : 11 - resto;
                return (char)('0' + dig);
            }

    }
    }
}
