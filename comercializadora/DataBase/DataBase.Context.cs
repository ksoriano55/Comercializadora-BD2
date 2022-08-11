﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace comercializadora.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Core.Objects.DataClasses;
    using System.Linq;
    
    public partial class ComercializadoraDBEntities : DbContext
    {
        public ComercializadoraDBEntities()
            : base("name=ComercializadoraDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<CuentaBancaria> CuentaBancaria { get; set; }
        public DbSet<Finca> Finca { get; set; }
        public DbSet<Insumo> Insumo { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Productor> Productor { get; set; }
        public DbSet<ListaPrecio> ListaPrecio { get; set; }
        public DbSet<Bodega> Bodega { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<Lotes> Lotes { get; set; }
        public DbSet<vInventarioInsumos> vInventarioInsumos { get; set; }
        public DbSet<vInventarioProductos> vInventarioProductos { get; set; }
        public DbSet<TipoPago> TipoPago { get; set; }
        public DbSet<Compra> Compra { get; set; }
        public DbSet<CompraDetalle> CompraDetalle { get; set; }
        public DbSet<Cosecha> Cosecha { get; set; }
        public DbSet<PrecioCompra> PrecioCompra { get; set; }
        public DbSet<vPrecioCompraInsumos> vPrecioCompraInsumos { get; set; }
        public DbSet<vPrecioCompraProductos> vPrecioCompraProductos { get; set; }
        public DbSet<FacturaDetalle> FacturaDetalle { get; set; }
        public DbSet<Factura> Factura { get; set; }
        public DbSet<CosechaDetalle> CosechaDetalle { get; set; }
        public DbSet<PrecioVenta> PrecioVenta { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }
        public DbSet<Pagos> Pagos { get; set; }
        public DbSet<Cobros> Cobros { get; set; }
        public DbSet<vPrecioVentaInsumos> vPrecioVentaInsumos { get; set; }
        public DbSet<vPrecioVentaProductos> vPrecioVentaProductos { get; set; }
        public DbSet<ArqueoCaja> ArqueoCaja { get; set; }
        public DbSet<ArqueoCajaDetalle> ArqueoCajaDetalle { get; set; }
        public DbSet<Banco> Banco { get; set; }
        public DbSet<Cajero> Cajero { get; set; }
        public DbSet<ChequeArqueo> ChequeArqueo { get; set; }
        public DbSet<Denominacion> Denominacion { get; set; }
        public DbSet<EfectivoArqueo> EfectivoArqueo { get; set; }
        public DbSet<vFincas> vFincas { get; set; }
    
        public virtual ObjectResult<SP_InsertListaPrecio_Result> SP_InsertListaPrecio(string codigo, string descripcion)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertListaPrecio_Result>("SP_InsertListaPrecio", codigoParameter, descripcionParameter);
        }
    
        public virtual ObjectResult<SP_InsertInventario_Result> SP_InsertInventario(Nullable<int> bodega, Nullable<int> productoId, Nullable<int> insumoId, Nullable<int> disponible)
        {
            var bodegaParameter = bodega.HasValue ?
                new ObjectParameter("bodega", bodega) :
                new ObjectParameter("bodega", typeof(int));
    
            var productoIdParameter = productoId.HasValue ?
                new ObjectParameter("productoId", productoId) :
                new ObjectParameter("productoId", typeof(int));
    
            var insumoIdParameter = insumoId.HasValue ?
                new ObjectParameter("insumoId", insumoId) :
                new ObjectParameter("insumoId", typeof(int));
    
            var disponibleParameter = disponible.HasValue ?
                new ObjectParameter("disponible", disponible) :
                new ObjectParameter("disponible", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertInventario_Result>("SP_InsertInventario", bodegaParameter, productoIdParameter, insumoIdParameter, disponibleParameter);
        }
    
        public virtual ObjectResult<SP_InsertTipoPago_Result> SP_InsertTipoPago(string descripcion)
        {
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertTipoPago_Result>("SP_InsertTipoPago", descripcionParameter);
        }
    
        public virtual ObjectResult<SP_DeleteTipoPago_Result> SP_DeleteTipoPago(Nullable<int> tipoPago)
        {
            var tipoPagoParameter = tipoPago.HasValue ?
                new ObjectParameter("TipoPago", tipoPago) :
                new ObjectParameter("TipoPago", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_DeleteTipoPago_Result>("SP_DeleteTipoPago", tipoPagoParameter);
        }
    
        public virtual ObjectResult<SP_UpdateTipoPago_Result> SP_UpdateTipoPago(Nullable<int> tipoPago, string descripcion)
        {
            var tipoPagoParameter = tipoPago.HasValue ?
                new ObjectParameter("TipoPago", tipoPago) :
                new ObjectParameter("TipoPago", typeof(int));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_UpdateTipoPago_Result>("SP_UpdateTipoPago", tipoPagoParameter, descripcionParameter);
        }
    
        public virtual ObjectResult<SP_InsertPrecioCompra_Result> SP_InsertPrecioCompra(string listaPrecio, Nullable<int> productoId, Nullable<int> insumoId, Nullable<decimal> precio, Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta)
        {
            var listaPrecioParameter = listaPrecio != null ?
                new ObjectParameter("listaPrecio", listaPrecio) :
                new ObjectParameter("listaPrecio", typeof(string));
    
            var productoIdParameter = productoId.HasValue ?
                new ObjectParameter("productoId", productoId) :
                new ObjectParameter("productoId", typeof(int));
    
            var insumoIdParameter = insumoId.HasValue ?
                new ObjectParameter("insumoId", insumoId) :
                new ObjectParameter("insumoId", typeof(int));
    
            var precioParameter = precio.HasValue ?
                new ObjectParameter("Precio", precio) :
                new ObjectParameter("Precio", typeof(decimal));
    
            var fechaDesdeParameter = fechaDesde.HasValue ?
                new ObjectParameter("fechaDesde", fechaDesde) :
                new ObjectParameter("fechaDesde", typeof(System.DateTime));
    
            var fechaHastaParameter = fechaHasta.HasValue ?
                new ObjectParameter("fechaHasta", fechaHasta) :
                new ObjectParameter("fechaHasta", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertPrecioCompra_Result>("SP_InsertPrecioCompra", listaPrecioParameter, productoIdParameter, insumoIdParameter, precioParameter, fechaDesdeParameter, fechaHastaParameter);
        }
    
        public virtual ObjectResult<SP_InsertCompra_Result> SP_InsertCompra(Nullable<int> proveedorId, Nullable<decimal> valorCompra, Nullable<decimal> saldoPendiente)
        {
            var proveedorIdParameter = proveedorId.HasValue ?
                new ObjectParameter("proveedorId", proveedorId) :
                new ObjectParameter("proveedorId", typeof(int));
    
            var valorCompraParameter = valorCompra.HasValue ?
                new ObjectParameter("ValorCompra", valorCompra) :
                new ObjectParameter("ValorCompra", typeof(decimal));
    
            var saldoPendienteParameter = saldoPendiente.HasValue ?
                new ObjectParameter("SaldoPendiente", saldoPendiente) :
                new ObjectParameter("SaldoPendiente", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertCompra_Result>("SP_InsertCompra", proveedorIdParameter, valorCompraParameter, saldoPendienteParameter);
        }
    
        public virtual ObjectResult<SP_InsertCompraDetalle_Result> SP_InsertCompraDetalle(Nullable<int> compraId, Nullable<int> insumoId, Nullable<int> cantidad, Nullable<int> proveedorId)
        {
            var compraIdParameter = compraId.HasValue ?
                new ObjectParameter("compraId", compraId) :
                new ObjectParameter("compraId", typeof(int));
    
            var insumoIdParameter = insumoId.HasValue ?
                new ObjectParameter("insumoId", insumoId) :
                new ObjectParameter("insumoId", typeof(int));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(int));
    
            var proveedorIdParameter = proveedorId.HasValue ?
                new ObjectParameter("proveedorId", proveedorId) :
                new ObjectParameter("proveedorId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertCompraDetalle_Result>("SP_InsertCompraDetalle", compraIdParameter, insumoIdParameter, cantidadParameter, proveedorIdParameter);
        }
    
        public virtual ObjectResult<SP_UpdateListaPrecio_Result> SP_UpdateListaPrecio(string codigo, string descripcion)
        {
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_UpdateListaPrecio_Result>("SP_UpdateListaPrecio", codigoParameter, descripcionParameter);
        }
    
        public virtual ObjectResult<SP_InsertBodega_Result> SP_InsertBodega(string nombre)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertBodega_Result>("SP_InsertBodega", nombreParameter);
        }
    
        public virtual ObjectResult<SP_UpdateBodega_Result> SP_UpdateBodega(Nullable<int> bodegaid, string codigo, string nombre)
        {
            var bodegaidParameter = bodegaid.HasValue ?
                new ObjectParameter("bodegaid", bodegaid) :
                new ObjectParameter("bodegaid", typeof(int));
    
            var codigoParameter = codigo != null ?
                new ObjectParameter("codigo", codigo) :
                new ObjectParameter("codigo", typeof(string));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_UpdateBodega_Result>("SP_UpdateBodega", bodegaidParameter, codigoParameter, nombreParameter);
        }
    
        public virtual ObjectResult<SP_InsertProductos_Result> SP_InsertProductos(Nullable<int> loteId, string descripcion)
        {
            var loteIdParameter = loteId.HasValue ?
                new ObjectParameter("loteId", loteId) :
                new ObjectParameter("loteId", typeof(int));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertProductos_Result>("SP_InsertProductos", loteIdParameter, descripcionParameter);
        }
    
        public virtual ObjectResult<SP_InsertFactura_Result> SP_InsertFactura(Nullable<int> clienteId, Nullable<int> productorId)
        {
            var clienteIdParameter = clienteId.HasValue ?
                new ObjectParameter("clienteId", clienteId) :
                new ObjectParameter("clienteId", typeof(int));
    
            var productorIdParameter = productorId.HasValue ?
                new ObjectParameter("productorId", productorId) :
                new ObjectParameter("productorId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertFactura_Result>("SP_InsertFactura", clienteIdParameter, productorIdParameter);
        }
    
        public virtual ObjectResult<SP_InsertFacturaDetalle_Result> SP_InsertFacturaDetalle(Nullable<int> facturaId, Nullable<int> productoId, Nullable<int> insumoId, Nullable<int> cantidad, Nullable<int> clienteId)
        {
            var facturaIdParameter = facturaId.HasValue ?
                new ObjectParameter("facturaId", facturaId) :
                new ObjectParameter("facturaId", typeof(int));
    
            var productoIdParameter = productoId.HasValue ?
                new ObjectParameter("productoId", productoId) :
                new ObjectParameter("productoId", typeof(int));
    
            var insumoIdParameter = insumoId.HasValue ?
                new ObjectParameter("insumoId", insumoId) :
                new ObjectParameter("insumoId", typeof(int));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(int));
    
            var clienteIdParameter = clienteId.HasValue ?
                new ObjectParameter("clienteId", clienteId) :
                new ObjectParameter("clienteId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertFacturaDetalle_Result>("SP_InsertFacturaDetalle", facturaIdParameter, productoIdParameter, insumoIdParameter, cantidadParameter, clienteIdParameter);
        }
    
        public virtual ObjectResult<SP_InsertInsumos_Result> SP_InsertInsumos(string descripcion)
        {
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertInsumos_Result>("SP_InsertInsumos", descripcionParameter);
        }
    
        public virtual ObjectResult<SP_UpdateInsumo_Result> SP_UpdateInsumo(Nullable<int> insumoId, string descripcion)
        {
            var insumoIdParameter = insumoId.HasValue ?
                new ObjectParameter("InsumoId", insumoId) :
                new ObjectParameter("InsumoId", typeof(int));
    
            var descripcionParameter = descripcion != null ?
                new ObjectParameter("Descripcion", descripcion) :
                new ObjectParameter("Descripcion", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_UpdateInsumo_Result>("SP_UpdateInsumo", insumoIdParameter, descripcionParameter);
        }
    
        public virtual ObjectResult<SP_InsertProductores_Result> SP_InsertProductores(string nombre, string identidad, string rtn, string telefono, string email, Nullable<decimal> saldodisponible, Nullable<int> diascredito, Nullable<int> cuentabancaria, string listaprecio)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var identidadParameter = identidad != null ?
                new ObjectParameter("identidad", identidad) :
                new ObjectParameter("identidad", typeof(string));
    
            var rtnParameter = rtn != null ?
                new ObjectParameter("rtn", rtn) :
                new ObjectParameter("rtn", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("telefono", telefono) :
                new ObjectParameter("telefono", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var saldodisponibleParameter = saldodisponible.HasValue ?
                new ObjectParameter("saldodisponible", saldodisponible) :
                new ObjectParameter("saldodisponible", typeof(decimal));
    
            var diascreditoParameter = diascredito.HasValue ?
                new ObjectParameter("diascredito", diascredito) :
                new ObjectParameter("diascredito", typeof(int));
    
            var cuentabancariaParameter = cuentabancaria.HasValue ?
                new ObjectParameter("cuentabancaria", cuentabancaria) :
                new ObjectParameter("cuentabancaria", typeof(int));
    
            var listaprecioParameter = listaprecio != null ?
                new ObjectParameter("listaprecio", listaprecio) :
                new ObjectParameter("listaprecio", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertProductores_Result>("SP_InsertProductores", nombreParameter, identidadParameter, rtnParameter, telefonoParameter, emailParameter, saldodisponibleParameter, diascreditoParameter, cuentabancariaParameter, listaprecioParameter);
        }
    
        public virtual ObjectResult<SP_UpdateProductores_Result> SP_UpdateProductores(Nullable<int> productorid, string nombre, string identidad, string rtn, string telefono, string email, Nullable<decimal> saldodisponible, Nullable<int> diascredito, Nullable<int> cuentabancaria, string listaprecio)
        {
            var productoridParameter = productorid.HasValue ?
                new ObjectParameter("productorid", productorid) :
                new ObjectParameter("productorid", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("nombre", nombre) :
                new ObjectParameter("nombre", typeof(string));
    
            var identidadParameter = identidad != null ?
                new ObjectParameter("identidad", identidad) :
                new ObjectParameter("identidad", typeof(string));
    
            var rtnParameter = rtn != null ?
                new ObjectParameter("rtn", rtn) :
                new ObjectParameter("rtn", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("telefono", telefono) :
                new ObjectParameter("telefono", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            var saldodisponibleParameter = saldodisponible.HasValue ?
                new ObjectParameter("saldodisponible", saldodisponible) :
                new ObjectParameter("saldodisponible", typeof(decimal));
    
            var diascreditoParameter = diascredito.HasValue ?
                new ObjectParameter("diascredito", diascredito) :
                new ObjectParameter("diascredito", typeof(int));
    
            var cuentabancariaParameter = cuentabancaria.HasValue ?
                new ObjectParameter("cuentabancaria", cuentabancaria) :
                new ObjectParameter("cuentabancaria", typeof(int));
    
            var listaprecioParameter = listaprecio != null ?
                new ObjectParameter("listaprecio", listaprecio) :
                new ObjectParameter("listaprecio", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_UpdateProductores_Result>("SP_UpdateProductores", productoridParameter, nombreParameter, identidadParameter, rtnParameter, telefonoParameter, emailParameter, saldodisponibleParameter, diascreditoParameter, cuentabancariaParameter, listaprecioParameter);
        }
    
        public virtual ObjectResult<string> spDeleteCliente(Nullable<int> clienteID)
        {
            var clienteIDParameter = clienteID.HasValue ?
                new ObjectParameter("ClienteID", clienteID) :
                new ObjectParameter("ClienteID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("spDeleteCliente", clienteIDParameter);
        }
    
        public virtual ObjectResult<spInsertCliente_Result> spInsertCliente(string nombre, string rTN, string telefono, string direccion, string listaPrecio)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var rTNParameter = rTN != null ?
                new ObjectParameter("RTN", rTN) :
                new ObjectParameter("RTN", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("Telefono", telefono) :
                new ObjectParameter("Telefono", typeof(string));
    
            var direccionParameter = direccion != null ?
                new ObjectParameter("Direccion", direccion) :
                new ObjectParameter("Direccion", typeof(string));
    
            var listaPrecioParameter = listaPrecio != null ?
                new ObjectParameter("ListaPrecio", listaPrecio) :
                new ObjectParameter("ListaPrecio", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spInsertCliente_Result>("spInsertCliente", nombreParameter, rTNParameter, telefonoParameter, direccionParameter, listaPrecioParameter);
        }
    
        public virtual ObjectResult<spInsertFinca_Result> spInsertFinca(string nombre, Nullable<int> productorID)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var productorIDParameter = productorID.HasValue ?
                new ObjectParameter("ProductorID", productorID) :
                new ObjectParameter("ProductorID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spInsertFinca_Result>("spInsertFinca", nombreParameter, productorIDParameter);
        }
    
        public virtual ObjectResult<spInsertLote_Result> spInsertLote(Nullable<int> fincaID, string extension, string tipoSuelo, string tipoRiego, Nullable<int> cantidadCosecha)
        {
            var fincaIDParameter = fincaID.HasValue ?
                new ObjectParameter("FincaID", fincaID) :
                new ObjectParameter("FincaID", typeof(int));
    
            var extensionParameter = extension != null ?
                new ObjectParameter("Extension", extension) :
                new ObjectParameter("Extension", typeof(string));
    
            var tipoSueloParameter = tipoSuelo != null ?
                new ObjectParameter("TipoSuelo", tipoSuelo) :
                new ObjectParameter("TipoSuelo", typeof(string));
    
            var tipoRiegoParameter = tipoRiego != null ?
                new ObjectParameter("TipoRiego", tipoRiego) :
                new ObjectParameter("TipoRiego", typeof(string));
    
            var cantidadCosechaParameter = cantidadCosecha.HasValue ?
                new ObjectParameter("CantidadCosecha", cantidadCosecha) :
                new ObjectParameter("CantidadCosecha", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spInsertLote_Result>("spInsertLote", fincaIDParameter, extensionParameter, tipoSueloParameter, tipoRiegoParameter, cantidadCosechaParameter);
        }
    
        public virtual ObjectResult<spInsertProveedor_Result> spInsertProveedor(string nombre, string rTN, string telefono, string direccion, string eMail)
        {
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var rTNParameter = rTN != null ?
                new ObjectParameter("RTN", rTN) :
                new ObjectParameter("RTN", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("Telefono", telefono) :
                new ObjectParameter("Telefono", typeof(string));
    
            var direccionParameter = direccion != null ?
                new ObjectParameter("Direccion", direccion) :
                new ObjectParameter("Direccion", typeof(string));
    
            var eMailParameter = eMail != null ?
                new ObjectParameter("EMail", eMail) :
                new ObjectParameter("EMail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spInsertProveedor_Result>("spInsertProveedor", nombreParameter, rTNParameter, telefonoParameter, direccionParameter, eMailParameter);
        }
    
        public virtual ObjectResult<spUpdateProveedor_Result> spUpdateProveedor(Nullable<int> proveedorID, string nombre, string rTN, string telefono, string eMail)
        {
            var proveedorIDParameter = proveedorID.HasValue ?
                new ObjectParameter("ProveedorID", proveedorID) :
                new ObjectParameter("ProveedorID", typeof(int));
    
            var nombreParameter = nombre != null ?
                new ObjectParameter("Nombre", nombre) :
                new ObjectParameter("Nombre", typeof(string));
    
            var rTNParameter = rTN != null ?
                new ObjectParameter("RTN", rTN) :
                new ObjectParameter("RTN", typeof(string));
    
            var telefonoParameter = telefono != null ?
                new ObjectParameter("Telefono", telefono) :
                new ObjectParameter("Telefono", typeof(string));
    
            var eMailParameter = eMail != null ?
                new ObjectParameter("EMail", eMail) :
                new ObjectParameter("EMail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spUpdateProveedor_Result>("spUpdateProveedor", proveedorIDParameter, nombreParameter, rTNParameter, telefonoParameter, eMailParameter);
        }
    
        public virtual ObjectResult<SP_InsertPagos_Result> SP_InsertPagos(Nullable<int> productorId, Nullable<int> proveedorId, Nullable<int> compraId, Nullable<int> tipoPagoId, string concepto, Nullable<System.DateTime> fecha, Nullable<decimal> monto)
        {
            var productorIdParameter = productorId.HasValue ?
                new ObjectParameter("productorId", productorId) :
                new ObjectParameter("productorId", typeof(int));
    
            var proveedorIdParameter = proveedorId.HasValue ?
                new ObjectParameter("proveedorId", proveedorId) :
                new ObjectParameter("proveedorId", typeof(int));
    
            var compraIdParameter = compraId.HasValue ?
                new ObjectParameter("compraId", compraId) :
                new ObjectParameter("compraId", typeof(int));
    
            var tipoPagoIdParameter = tipoPagoId.HasValue ?
                new ObjectParameter("tipoPagoId", tipoPagoId) :
                new ObjectParameter("tipoPagoId", typeof(int));
    
            var conceptoParameter = concepto != null ?
                new ObjectParameter("concepto", concepto) :
                new ObjectParameter("concepto", typeof(string));
    
            var fechaParameter = fecha.HasValue ?
                new ObjectParameter("fecha", fecha) :
                new ObjectParameter("fecha", typeof(System.DateTime));
    
            var montoParameter = monto.HasValue ?
                new ObjectParameter("monto", monto) :
                new ObjectParameter("monto", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertPagos_Result>("SP_InsertPagos", productorIdParameter, proveedorIdParameter, compraIdParameter, tipoPagoIdParameter, conceptoParameter, fechaParameter, montoParameter);
        }
    
        public virtual ObjectResult<SP_InsertPrecioVenta_Result> SP_InsertPrecioVenta(Nullable<int> productoID, Nullable<decimal> precio, Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta, string listaPrecio, Nullable<int> insumoid)
        {
            var productoIDParameter = productoID.HasValue ?
                new ObjectParameter("ProductoID", productoID) :
                new ObjectParameter("ProductoID", typeof(int));
    
            var precioParameter = precio.HasValue ?
                new ObjectParameter("Precio", precio) :
                new ObjectParameter("Precio", typeof(decimal));
    
            var fechaDesdeParameter = fechaDesde.HasValue ?
                new ObjectParameter("FechaDesde", fechaDesde) :
                new ObjectParameter("FechaDesde", typeof(System.DateTime));
    
            var fechaHastaParameter = fechaHasta.HasValue ?
                new ObjectParameter("FechaHasta", fechaHasta) :
                new ObjectParameter("FechaHasta", typeof(System.DateTime));
    
            var listaPrecioParameter = listaPrecio != null ?
                new ObjectParameter("ListaPrecio", listaPrecio) :
                new ObjectParameter("ListaPrecio", typeof(string));
    
            var insumoidParameter = insumoid.HasValue ?
                new ObjectParameter("insumoid", insumoid) :
                new ObjectParameter("insumoid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_InsertPrecioVenta_Result>("SP_InsertPrecioVenta", productoIDParameter, precioParameter, fechaDesdeParameter, fechaHastaParameter, listaPrecioParameter, insumoidParameter);
        }
    
        public virtual ObjectResult<SP_UpdatePrecioVenta_Result> SP_UpdatePrecioVenta(Nullable<int> productoID, Nullable<decimal> precio, Nullable<System.DateTime> fechaDesde, Nullable<System.DateTime> fechaHasta, string listaPrecio, Nullable<int> insumoid)
        {
            var productoIDParameter = productoID.HasValue ?
                new ObjectParameter("ProductoID", productoID) :
                new ObjectParameter("ProductoID", typeof(int));
    
            var precioParameter = precio.HasValue ?
                new ObjectParameter("Precio", precio) :
                new ObjectParameter("Precio", typeof(decimal));
    
            var fechaDesdeParameter = fechaDesde.HasValue ?
                new ObjectParameter("FechaDesde", fechaDesde) :
                new ObjectParameter("FechaDesde", typeof(System.DateTime));
    
            var fechaHastaParameter = fechaHasta.HasValue ?
                new ObjectParameter("FechaHasta", fechaHasta) :
                new ObjectParameter("FechaHasta", typeof(System.DateTime));
    
            var listaPrecioParameter = listaPrecio != null ?
                new ObjectParameter("ListaPrecio", listaPrecio) :
                new ObjectParameter("ListaPrecio", typeof(string));
    
            var insumoidParameter = insumoid.HasValue ?
                new ObjectParameter("insumoid", insumoid) :
                new ObjectParameter("insumoid", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SP_UpdatePrecioVenta_Result>("SP_UpdatePrecioVenta", productoIDParameter, precioParameter, fechaDesdeParameter, fechaHastaParameter, listaPrecioParameter, insumoidParameter);
        }
    }
}
