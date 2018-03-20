using DBTransacciones;

namespace ConsolaTransacciones
{
    class Program
    {
        static void Main(string[] args)
        {
            // Database de Transacciones
            using (TransaccionesContext dbTx = new TransaccionesContext())
            {
                dbTx.Database.EnsureCreated();
                ConsolaAux.Run(dbTx, args);
            }
        }
    }
}
