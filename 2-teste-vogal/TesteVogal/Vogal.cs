using System;
using System.Collections.Generic;
using System.Linq;

namespace TesteVogal
{
    public class Vogal
    {
        public static char FirstChar(IStream input)
        {
            var anterior = default(char);
            var vogais = new List<char>();
            var candidatos = new List<char>();

            while (input.HasNext())
            {
                var character = input.GetNext();

                if (IsVogal(character))
                {
                    if (!vogais.Contains(character))
                    {
                        vogais.Add(character);
                        if (!IsVogal(anterior))
                        {
                            candidatos.Add(character);
                        }
                    }
                    else
                    {
                        candidatos.Remove(character);
                    }

                }

                anterior = character;
            }

            return candidatos.FirstOrDefault();
        }

        private static bool IsVogal(char c) => "aeiou".IndexOf(c.ToString(), StringComparison.InvariantCultureIgnoreCase) >= 0;
    }
}
