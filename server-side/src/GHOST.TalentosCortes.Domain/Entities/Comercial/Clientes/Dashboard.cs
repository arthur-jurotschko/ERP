using GHOST.TalentosCortes.Domain.Core.Models;
using System;

namespace GHOST.TalentosCortes.Domain.Entities.Comercial.Clientes
{
    public class Dashboard : Entity<Dashboard>
    {
       public int YearDate { get;  set; }
       public int Janeiro { get;  set; }
       public int Fevereiro { get;  set; }
       public int Marco { get;  set; }
       public int Abril { get;  set; }
       public int Maio { get;  set; }
       public int Junho { get;  set; }
       public int Julho { get;  set; }
       public int Agosto { get;  set; }
       public int Setembro { get;  set; }
       public int Outubro { get;  set; }
       public int Novembro { get;  set; }
       public int Dezembro { get;  set; }

       // Construtor para o EF
       protected Dashboard() { }

        public Dashboard(int yearDate, int janeiro, int fevereiro, int marco, int abril, int maio, int junho, int julho, int agosto, int setembro, int outubro, int novembro, int dezembro)
        {
 
            YearDate = yearDate;
            Janeiro = janeiro;
            Fevereiro = fevereiro;
            Marco = marco;
            Abril = abril;
            Maio = maio;
            Junho = junho;
            Julho = julho;
            Agosto = agosto;
            Setembro = setembro;
            Outubro = outubro;
            Novembro = novembro;
            Dezembro = dezembro;
        }

        public override bool EhValido()
        {
            return true;
        }
    }
}
