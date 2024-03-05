using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Ejercicio2_2.Models
{
    public class Firmas
    {
        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        [MaxLength(250)]
        public string nombre { get; set; }

        [MaxLength(250)]
        public string descripcion { get; set; }

        public byte[] imgM { get; set; }

        [Ignore] // Indica a SQLite que ignore esta propiedad al crear la tabla
        public ImageSource ImgM
        {
            get
            {
                if (imgM != null)
                {
                    return ImageSource.FromStream(() => new MemoryStream(imgM));
                }
                return null;
            }
        }
    }
}
