using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Ejercicio2_2.Models
{
    public class FirmaControllers
    {
        readonly SQLiteAsyncConnection _connection;

        // Firmas 
        public FirmaControllers()
        {
            SQLite.SQLiteOpenFlags extensiones = SQLite.SQLiteOpenFlags.ReadWrite |
                                                SQLite.SQLiteOpenFlags.Create |
                                                SQLite.SQLiteOpenFlags.SharedCache;

            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "Firmas.db3"), extensiones);

            _connection.CreateTableAsync<Firmas>();
        }

        // CREATE
        public async Task<int> AgregarFirma(Firmas firma)
        {
            return await _connection.InsertAsync(firma);

        }
        public Task<List<Firmas>> listaFirmas()
        {

            return _connection.Table<Firmas>().ToListAsync();
        }

        // Buscar Firma por su ID
        public Task<List<Firmas>> ObtenerFirma()
        {
            return _connection.Table<Firmas>().ToListAsync();
        }

        public Task<Firmas> obtenerFirma(int pid)
        {
            return _connection.Table<Firmas>()
                .Where(i => i.id == pid)
                .FirstOrDefaultAsync();
        }


        // Salvar o actualizar

        public Task<int> FirmaGuardar(Firmas firma)
        {
            if (firma.id != 0)
            {
                return _connection.UpdateAsync(firma);
            }
            else
            {
                return _connection.InsertAsync(firma);
            }
        }

        //Eliminar 

        public Task<int> FirmaBorrar(Firmas firma)
        {
            return _connection.DeleteAsync(firma);
        }
    }
}
