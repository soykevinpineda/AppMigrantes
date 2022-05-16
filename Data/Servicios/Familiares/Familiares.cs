﻿using Migrantes.Models.Entities;
using Migrantes.ViewModels;
using System.Threading.Tasks;

namespace Migrantes.Data.Servicios.Familiares
{
    public class Familiares : IFamiliares
    {

        //Declarando ApplicationDbContext
        private readonly ApplicationDbContext _context;

        public Familiares(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task AgregandoDatosFamiliares(DatosFamiliaresViewModel oDatosFamiliares)
        {

            DatosFamiliares ObjFamiliares = new DatosFamiliares();

            ObjFamiliares.per_codigo_id = oDatosFamiliares.per_codigo_id;   
            ObjFamiliares.nombres_madre = oDatosFamiliares.nombres_madre;
            ObjFamiliares.primer_apellido_madre = oDatosFamiliares.primer_apellido_madre;
            ObjFamiliares.segundo_apellido_madre = oDatosFamiliares.segundo_apellido_madre;
            ObjFamiliares.edad_madre = oDatosFamiliares.edad_madre;
            ObjFamiliares. profesion_madre = oDatosFamiliares.profesion_madre;

            ObjFamiliares.nombres_padre = oDatosFamiliares.nombres_padre;
            ObjFamiliares.primer_apellido_padre = oDatosFamiliares.primer_apellido_padre;
            ObjFamiliares.segundo_apellido_padre = oDatosFamiliares.segundo_apellido_padre;
            ObjFamiliares.edad_padre = oDatosFamiliares.edad_padre;
            ObjFamiliares.profesion_padre = oDatosFamiliares.profesion_padre;
            ObjFamiliares.estado_datosfamiliares = 1;

            this._context.DatosFamiliaresDb.Add(ObjFamiliares);
            await this._context.SaveChangesAsync();

        }

        public async Task ActualizarFamiliaresEditados(DatosFamiliaresViewModel DatosFamiliaresEditados)
        {

            DatosFamiliares oDatosFamiliares = new DatosFamiliares();

            oDatosFamiliares.id_datos_familiares = DatosFamiliaresEditados.id_datos_familiares;
            oDatosFamiliares.per_codigo_id = DatosFamiliaresEditados.per_codigo_id;
            oDatosFamiliares.nombres_madre = DatosFamiliaresEditados.nombres_madre;
            oDatosFamiliares.primer_apellido_madre = DatosFamiliaresEditados.primer_apellido_madre;
            oDatosFamiliares.segundo_apellido_madre = DatosFamiliaresEditados.segundo_apellido_madre;
            oDatosFamiliares.edad_madre = DatosFamiliaresEditados.edad_madre;
            oDatosFamiliares.profesion_madre = DatosFamiliaresEditados.profesion_madre;

            oDatosFamiliares.nombres_padre = DatosFamiliaresEditados.nombres_padre;
            oDatosFamiliares.primer_apellido_padre = DatosFamiliaresEditados.primer_apellido_padre;
            oDatosFamiliares.segundo_apellido_padre = DatosFamiliaresEditados.segundo_apellido_padre;
            oDatosFamiliares.edad_padre = DatosFamiliaresEditados.edad_padre;
            oDatosFamiliares.profesion_padre = DatosFamiliaresEditados.profesion_padre;
            oDatosFamiliares.estado_datosfamiliares = 1;

            this._context.DatosFamiliaresDb.Update(oDatosFamiliares);
            await this._context.SaveChangesAsync();

        }

        public async Task EliminarConfirmadoDatosFamiliares(DatosFamiliaresViewModel DatosFamiliaresEliminados)

        {

            DatosFamiliares ObjFamiliares = new DatosFamiliares();

            ObjFamiliares.id_datos_familiares = DatosFamiliaresEliminados.id_datos_familiares;
            ObjFamiliares.per_codigo_id = DatosFamiliaresEliminados.per_codigo_id;
            ObjFamiliares.nombres_madre = DatosFamiliaresEliminados.nombres_madre;
            ObjFamiliares.primer_apellido_madre = DatosFamiliaresEliminados.primer_apellido_madre;
            ObjFamiliares.segundo_apellido_madre = DatosFamiliaresEliminados.segundo_apellido_madre;
            ObjFamiliares.edad_madre = DatosFamiliaresEliminados.edad_madre;
            ObjFamiliares.profesion_madre = DatosFamiliaresEliminados.profesion_madre;

            ObjFamiliares.nombres_padre = DatosFamiliaresEliminados.nombres_padre;
            ObjFamiliares.primer_apellido_padre = DatosFamiliaresEliminados.primer_apellido_padre;
            ObjFamiliares.segundo_apellido_padre = DatosFamiliaresEliminados.segundo_apellido_padre;
            ObjFamiliares.edad_padre = DatosFamiliaresEliminados.edad_padre;
            ObjFamiliares.profesion_padre = DatosFamiliaresEliminados.profesion_padre;
            ObjFamiliares.estado_datosfamiliares = 1;

            this._context.DatosFamiliaresDb.Remove(ObjFamiliares);
            await this._context.SaveChangesAsync();

        }

    }
}
