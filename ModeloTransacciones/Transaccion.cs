using System;

namespace ModeloTransacciones
{
    public class Transaccion
    {
        public int TransaccionId { get; set; }
        public int TipoTransaccionId { get; set; }
        public TipoTransaccion TipoTransaccion { get; set; }
        public int FormaTransaccionId { get; set; }
        public FormaTransaccion FormaTransaccion { get; set; }

        public int CuentaId { get; set; }
        public Cuenta Cuenta { get; set; }
        public decimal Monto { get; set; }
        public decimal MontoMonedaBase { get; set; }
        public int MonedaId { get; set; }
        public Moneda Moneda { get; set; }
        public int? BeneficiarioId { get; set; }
        public Beneficiario Beneficiario { get; set; }

        public int? ChequeNro { get; set; }
        public int? DocumentoNro { get; set; }
        public int? NroReferencia { get; set; }

        public int? CategoriaGastoId { get; set; }
        public CategoriaGasto CategoriaGasto { get; set; }
        public int? CategoriaIngresoId { get; set; }
        public CategoriaIngreso CategoriaIngreso { get; set; }
        public int? ConceptoId { get; set; }
        public Concepto Concepto { get; set; }

        public DateTime? FechaSolicitud { get; set; }
        public DateTime? FechaAnulacion { get; set; }
        public DateTime? FechaContable { get; set; }

        public int SituacionId { get; set; }
        public Situacion Situacion { get; set; }
    }
}