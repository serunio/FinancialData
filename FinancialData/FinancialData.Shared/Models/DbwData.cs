using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinancialData.Shared.Models
{
    public class DbwData
    {
        public int IdZmienna { get; set; }
        public int IdPrzekroj { get; set; }
        public int? IdWymiar1 { get; set; }
        public int? IdPozycja1 { get; set; }
        public int? IdWymiar2 { get; set; }
        public int? IdPozycja2 { get; set; }
        public int? IdWymiar3 { get; set; }
        public int? IdPozycja3 { get; set; }
        public int? IdWymiar4 { get; set; }
        public int? IdPozycja4 { get; set; }
        public int IdOkres { get; set; }
        public int IdSposobPrezentacjiMiara { get; set; }
        public int IdDaty { get; set; }
        public decimal? Wartosc { get; set; }

        public string? NazwaPrzekroj { get; set; }
        public string? NazwaZmienna { get; set; }

        public bool Matches(DbwData other)
        {
            if (other is null) throw new ArgumentNullException(nameof(other));

            return
                (other.IdWymiar1  == null || IdWymiar1  == other.IdWymiar1) &&
                (other.IdPozycja1 == null || IdPozycja1 == other.IdPozycja1) &&
                (other.IdWymiar2  == null || IdWymiar2  == other.IdWymiar2) &&
                (other.IdPozycja2 == null || IdPozycja2 == other.IdPozycja2) &&
                (other.IdWymiar3  == null || IdWymiar3  == other.IdWymiar3) &&
                (other.IdPozycja3 == null || IdPozycja3 == other.IdPozycja3) &&
                (other.IdWymiar4  == null || IdWymiar4  == other.IdWymiar4) &&
                (other.IdPozycja4 == null || IdPozycja4 == other.IdPozycja4) &&
                IdSposobPrezentacjiMiara == other.IdSposobPrezentacjiMiara;
        }
    }

    public class Root
    {
        public List<DbwData> Data { get; set; }
    }

    public class JsonKebabCasePolicy : JsonNamingPolicy
    {
        public override string ConvertName(string name)
        {
            var newName = new List<char>();
            for (var i = 0; i < name.Length; i++)
            {
                if (i != 0 && (char.IsUpper(name[i]) || char.IsDigit(name[i])))
                {
                    newName.Add('-');
                }
                newName.Add(char.ToLower(name[i]));
            }

            return new string(newName.ToArray());
        }
    }

}
