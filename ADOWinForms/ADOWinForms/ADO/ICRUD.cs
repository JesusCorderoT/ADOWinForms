﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOWinForms.ADO
{
    public interface ICRUD
    {
        List<Entidades.EstatusAlumno> Consultar();

        Entidades.EstatusAlumno Consultar(int id);

        int Agregar(Entidades.EstatusAlumno estatus);

        void Actualizar(Entidades.EstatusAlumno estatus);

        void Eliminar(Entidades.EstatusAlumno estatus);
    }
}
