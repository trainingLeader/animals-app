using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

    public class Ciudad
    {
        public string NombreCiudad { get; set; }

        public int IdDep { get; set; }
        public Departamento Departamento { get; set; }
        public ICollection<Cliente> Clientes { get; set; } 
    }
