using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Ejercicio2_2.Models
{
    public class BDFirmas
    {
        readonly SQLiteAsyncConnection dbase;

        // BDFirmas 
        public BDFirmas(String dbpath)
        {
            try
            {
                dbase = new SQLiteAsyncConnection(dbpath);

                // Creación de tablas de base de datos
                dbase.CreateTableAsync<BDFirmas>().Wait(); // Añadido Wait para asegurar la creación antes de continuar
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear la tabla: {ex.Message}");
            }

        }

        public Task<List<BDFirmas>> listaempleados()
        {

            return dbase.Table<BDFirmas>().ToListAsync();
        }

        // Buscar empleado por su ID
        public Task<List<BDFirmas>> ObtenerEmpleado()
        {
            return dbase.Table<BDFirmas>().ToListAsync();
        }

        public Task<BDFirmas> obtenerEmple(int pid)
        {
            return dbase.Table<BDFirmas>()
                .Where(i => i.codigo == pid)
                .FirstOrDefaultAsync();
        }


        // Salvar o actualizar

        public Task<int> EmpleadoGuardar(BDFirmas emple)
        {
            if (emple.codigo != 0)
            {
                return dbase.UpdateAsync(emple);
            }
            else
            {
                return dbase.InsertAsync(emple);
            }
        }

        //Eliminar 

        public Task<int> EmpleadoBorrar(BDFirmas emple)
        {
            return dbase.DeleteAsync(emple);
        }
    }
}
