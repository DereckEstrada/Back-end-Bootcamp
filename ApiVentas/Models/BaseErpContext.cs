﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApiVentas.Models;

public partial class BaseErpContext : DbContext
{
    public BaseErpContext()
    {
    }

    public BaseErpContext(DbContextOptions<BaseErpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bodega> Bodegas { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Ciudad> Ciudads { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<FormaPago> FormaPagos { get; set; }

    public virtual DbSet<Industrium> Industria { get; set; }

    public virtual DbSet<Modulo> Modulos { get; set; }

    public virtual DbSet<MovimientoCab> MovimientoCabs { get; set; }

    public virtual DbSet<MovimientoDetPago> MovimientoDetPagos { get; set; }

    public virtual DbSet<MovimientoDetProducto> MovimientoDetProductos { get; set; }

    public virtual DbSet<Opcion> Opcions { get; set; }

    public virtual DbSet<Pai> Pais { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<PuntoEmisionSri> PuntoEmisionSris { get; set; }

    public virtual DbSet<PuntoVentum> PuntoVenta { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<TarjetaCredito> TarjetaCreditos { get; set; }

    public virtual DbSet<TipoMovimiento> TipoMovimientos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }

    public virtual DbSet<UsuarioRol> UsuarioRols { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Modern_Spanish_CI_AS");

        modelBuilder.Entity<Bodega>(entity =>
        {
            entity.HasKey(e => e.BodegaId).HasName("_copy_3");

            entity.ToTable("BODEGA");

            entity.Property(e => e.BodegaId)
                .ValueGeneratedNever()
                .HasColumnName("bodega_id");
            entity.Property(e => e.BodegaDireccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("bodega_direccion");
            entity.Property(e => e.BodegaNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("bodega_nombre");
            entity.Property(e => e.BodegaTelefono)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("bodega_telefono");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.SucursalId).HasColumnName("sucursal_id");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Bodegas)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_bodega_sucursal");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.CategoriaId);

            entity.ToTable("CATEGORIA");

            entity.Property(e => e.CategoriaId)
                .ValueGeneratedNever()
                .HasColumnName("categoria_id");
            entity.Property(e => e.CategoriaDescrip)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("categoria_descrip");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");
        });

        modelBuilder.Entity<Ciudad>(entity =>
        {
            entity.HasKey(e => e.CiudadId).HasName("PK__CIUDAD__AA0ADB67714B1126");

            entity.ToTable("CIUDAD");

            entity.Property(e => e.CiudadId)
                .ValueGeneratedNever()
                .HasColumnName("ciudad_id");
            entity.Property(e => e.CiudadNombre)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("ciudad_nombre");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.PaisId).HasColumnName("pais_id");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Pais).WithMany(p => p.Ciudads)
                .HasForeignKey(d => d.PaisId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("kf_ciudad_pais");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.ClienteId).HasName("PK__CLIENTE__47E34D641DAB8E80");

            entity.ToTable("CLIENTE");

            entity.Property(e => e.ClienteId)
                .ValueGeneratedNever()
                .HasColumnName("cliente_id");
            entity.Property(e => e.ClienteApellido1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cliente_apellido1");
            entity.Property(e => e.ClienteApellido2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cliente_apellido2");
            entity.Property(e => e.ClienteDireccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cliente_direccion");
            entity.Property(e => e.ClienteEmail)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("cliente_email");
            entity.Property(e => e.ClienteNombre1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cliente_nombre1");
            entity.Property(e => e.ClienteNombre2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cliente_nombre2");
            entity.Property(e => e.ClienteRuc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("cliente_ruc");
            entity.Property(e => e.ClienteTelefono)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cliente_telefono");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct).HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.EmpresaId).HasName("_copy_5");

            entity.ToTable("EMPRESA");

            entity.Property(e => e.EmpresaId)
                .ValueGeneratedNever()
                .HasColumnName("empresa_id");
            entity.Property(e => e.CiudadId).HasColumnName("ciudad_id");
            entity.Property(e => e.EmpresaDireccionMatriz)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("empresa_direccion_matriz");
            entity.Property(e => e.EmpresaNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("empresa_nombre");
            entity.Property(e => e.EmpresaRazon)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("empresa_razon");
            entity.Property(e => e.EmpresaRuc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("empresa_ruc");
            entity.Property(e => e.EmpresaTelefonoMatriz)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("empresa_telefono_matriz");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Ciudad).WithMany(p => p.Empresas)
                .HasForeignKey(d => d.CiudadId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_empresa_ciudad");
        });

        modelBuilder.Entity<FormaPago>(entity =>
        {
            entity.HasKey(e => e.FpagoId).HasName("PK__FORMA_PA__5507F787112D24D2");

            entity.ToTable("FORMA_PAGO");

            entity.Property(e => e.FpagoId)
                .ValueGeneratedNever()
                .HasColumnName("fpago_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.FpagoDescripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("fpago_descripcion");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");
        });

        modelBuilder.Entity<Industrium>(entity =>
        {
            entity.HasKey(e => e.IndustriaId).HasName("PK__INDUSTRI__8558EAC6DC6BD79C");

            entity.ToTable("INDUSTRIA");

            entity.Property(e => e.IndustriaId)
                .ValueGeneratedNever()
                .HasColumnName("industria_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.IndustriaDescripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("industria_descripcion");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");
        });

        modelBuilder.Entity<Modulo>(entity =>
        {
            entity.HasKey(e => e.ModuloId).HasName("PK__MODULO__372E6251FD710E04");

            entity.ToTable("MODULO");

            entity.Property(e => e.ModuloId)
                .ValueGeneratedNever()
                .HasColumnName("modulo_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.ModuloDescripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("modulo_descripcion");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");
        });

        modelBuilder.Entity<MovimientoCab>(entity =>
        {
            entity.HasKey(e => e.MovicabId).HasName("PK__MOVIMIEN__57E1745CDAB11D8D");

            entity.ToTable("MOVIMIENTO_CAB");

            entity.Property(e => e.MovicabId)
                .ValueGeneratedNever()
                .HasColumnName("movicab_id");
            entity.Property(e => e.AutorizacionSri)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("autorizacion_sri");
            entity.Property(e => e.BodegaId).HasColumnName("bodega_id");
            entity.Property(e => e.ClaveAcceso)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("clave_acceso");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");
            entity.Property(e => e.PuntovtaId).HasColumnName("puntovta_id");
            entity.Property(e => e.SecuenciaFactura)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("secuencia_factura");
            entity.Property(e => e.SucursalId).HasColumnName("sucursal_id");
            entity.Property(e => e.TipomovId).HasColumnName("tipomov_id");
            entity.Property(e => e.TipomovIngEgr).HasColumnName("tipomov_ing_egr");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Cliente).WithMany(p => p.MovimientoCabs)
                .HasForeignKey(d => d.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_movim_cliente");

            entity.HasOne(d => d.Empresa).WithMany(p => p.MovimientoCabs)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_movim_empresa");

            entity.HasOne(d => d.Proveedor).WithMany(p => p.MovimientoCabs)
                .HasForeignKey(d => d.ProveedorId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_movim_proveedor");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.MovimientoCabs)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_movim_sucursal");

            entity.HasOne(d => d.Tipomov).WithMany(p => p.MovimientoCabs)
                .HasForeignKey(d => d.TipomovId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_movim_tipo");

            entity.HasOne(d => d.UsuIdRegNavigation).WithMany(p => p.MovimientoCabs)
                .HasForeignKey(d => d.UsuIdReg)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_movim_usu_reg");
        });

        modelBuilder.Entity<MovimientoDetPago>(entity =>
        {
            entity.HasKey(e => e.MovidetPagoId).HasName("PK__MOVIMIEN__4EA9F6FAEFB54B47");

            entity.ToTable("MOVIMIENTO_DET_PAGOS");

            entity.Property(e => e.MovidetPagoId)
                .ValueGeneratedNever()
                .HasColumnName("movidet_pago_id");
            entity.Property(e => e.BancoId).HasColumnName("banco_id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.ComprobanteId).HasColumnName("comprobante_id");
            entity.Property(e => e.FechaPago)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fecha_pago");
            entity.Property(e => e.FpagoId).HasColumnName("fpago_id");
            entity.Property(e => e.IndustriaId).HasColumnName("industria_id");
            entity.Property(e => e.Lote)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lote");
            entity.Property(e => e.MovicabId).HasColumnName("movicab_id");
            entity.Property(e => e.TarjetacredId).HasColumnName("tarjetacred_id");
            entity.Property(e => e.ValorPagado)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_pagado");
            entity.Property(e => e.Voucher)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("voucher");

            entity.HasOne(d => d.Fpago).WithMany(p => p.MovimientoDetPagos)
                .HasForeignKey(d => d.FpagoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_movidet_fpago");

            entity.HasOne(d => d.Movicab).WithMany(p => p.MovimientoDetPagos)
                .HasForeignKey(d => d.MovicabId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_movicab_detpagos");

            entity.HasOne(d => d.Tarjetacred).WithMany(p => p.MovimientoDetPagos)
                .HasForeignKey(d => d.TarjetacredId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_movidet_tarjeta");
        });

        modelBuilder.Entity<MovimientoDetProducto>(entity =>
        {
            entity.HasKey(e => e.MovidetProdId).HasName("PK__MOVIMIEN__39F20EB4E16F736D");

            entity.ToTable("MOVIMIENTO_DET_PRODUCTO");

            entity.Property(e => e.MovidetProdId)
                .ValueGeneratedNever()
                .HasColumnName("movidet_prod_id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.MovicabId).HasColumnName("movicab_id");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 4)")
                .HasColumnName("precio");
            entity.Property(e => e.ProductoId).HasColumnName("producto_id");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Movicab).WithMany(p => p.MovimientoDetProductos)
                .HasForeignKey(d => d.MovicabId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_movi_cab_detalle");

            entity.HasOne(d => d.Producto).WithMany(p => p.MovimientoDetProductos)
                .HasForeignKey(d => d.ProductoId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_movidet_prod");
        });

        modelBuilder.Entity<Opcion>(entity =>
        {
            entity.HasKey(e => e.OpcionId).HasName("PK__OPCION__FFA6A8F80DC3AF10");

            entity.ToTable("OPCION");

            entity.Property(e => e.OpcionId)
                .ValueGeneratedNever()
                .HasColumnName("opcion_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.ModuloId).HasColumnName("modulo_id");
            entity.Property(e => e.OpcionDescripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("opcion_descripcion");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Modulo).WithMany(p => p.Opcions)
                .HasForeignKey(d => d.ModuloId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_opcion_modulo");
        });

        modelBuilder.Entity<Pai>(entity =>
        {
            entity.HasKey(e => e.PaisId).HasName("PK__PAIS__C050735EE689B39E");

            entity.ToTable("PAIS");

            entity.Property(e => e.PaisId)
                .ValueGeneratedNever()
                .HasColumnName("pais_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.PaisNombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pais_nombre");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProdId).HasName("_copy_2");

            entity.ToTable("PRODUCTO");

            entity.Property(e => e.ProdId)
                .ValueGeneratedNever()
                .HasColumnName("prod_id");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.ProdDescripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("prod_descripcion");
            entity.Property(e => e.ProdUltPrecio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("prod_ult_precio");
            entity.Property(e => e.ProveedorId).HasColumnName("proveedor_id");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Categoria).WithMany(p => p.Productos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK_PRODUCTO_CATEGORIA");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.ProvId).HasName("PK__PROVEEDO__435F5326D50796A4");

            entity.ToTable("PROVEEDOR");

            entity.Property(e => e.ProvId)
                .ValueGeneratedNever()
                .HasColumnName("prov_id");
            entity.Property(e => e.CiudadId).HasColumnName("ciudad_id");
            entity.Property(e => e.Estado)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.ProvDireccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("prov_direccion");
            entity.Property(e => e.ProvNomComercial)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("prov_nom_comercial");
            entity.Property(e => e.ProvRazon)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("prov_razon");
            entity.Property(e => e.ProvRuc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("prov_ruc");
            entity.Property(e => e.ProvTelefono).HasColumnName("prov_telefono");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");
        });

        modelBuilder.Entity<PuntoEmisionSri>(entity =>
        {
            entity.HasKey(e => e.PuntoEmisionId).HasName("PK__PUNTO_EM__EC4275912F3F27E2");

            entity.ToTable("PUNTO_EMISION_SRI");

            entity.Property(e => e.PuntoEmisionId)
                .ValueGeneratedNever()
                .HasColumnName("punto_emision_id");
            entity.Property(e => e.CodEstablecimientoSri)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("cod_establecimiento_sri");
            entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.PuntoEmision)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("punto_emision");
            entity.Property(e => e.SucursalId).HasColumnName("sucursal_id");
            entity.Property(e => e.UltSecuencia).HasColumnName("ult_secuencia");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Empresa).WithMany(p => p.PuntoEmisionSris)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_puntoemi_empresa");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.PuntoEmisionSris)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_puntoemi_sucursal");
        });

        modelBuilder.Entity<PuntoVentum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PUNTO_VENTA");

            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.PuntoEmisionId).HasColumnName("punto_emision_id");
            entity.Property(e => e.PuntovtaId).HasColumnName("puntovta_id");
            entity.Property(e => e.PuntovtaNombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("puntovta_nombre");
            entity.Property(e => e.SucursalId).HasColumnName("sucursal_id");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Puntovta).WithMany()
                .HasForeignKey(d => d.PuntovtaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PUNTO_VENTA_PUNTO_EMISION_SRI");

            entity.HasOne(d => d.Sucursal).WithMany()
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_puntovta_sucursal");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__ROL__CF32E443BD04DEC4");

            entity.ToTable("ROL");

            entity.Property(e => e.RolId)
                .ValueGeneratedNever()
                .HasColumnName("rol_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.RolDescripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("rol_descripcion");
            entity.Property(e => e.UsuIdAct)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("PK__STOCK__E86668626908E46D");

            entity.ToTable("STOCK");

            entity.Property(e => e.StockId)
                .ValueGeneratedNever()
                .HasColumnName("stock_id");
            entity.Property(e => e.BodegaId).HasColumnName("bodega_id");
            entity.Property(e => e.CantidadStock).HasColumnName("cantidad_stock");
            entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.ProdId).HasColumnName("prod_id");
            entity.Property(e => e.SucursalId).HasColumnName("sucursal_id");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Empresa).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_stock_empresa");
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.SucursalId).HasName("_copy_4");

            entity.ToTable("SUCURSAL");

            entity.Property(e => e.SucursalId)
                .ValueGeneratedNever()
                .HasColumnName("sucursal_id");
            entity.Property(e => e.CodEstablecimientoSri)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cod_establecimiento_sri");
            entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.SucursalDireccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("sucursal_direccion");
            entity.Property(e => e.SucursalNombre)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("sucursal_nombre");
            entity.Property(e => e.SucursalRuc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("sucursal_ruc");
            entity.Property(e => e.SucursalTelefono)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sucursal_telefono");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");
        });

        modelBuilder.Entity<TarjetaCredito>(entity =>
        {
            entity.HasKey(e => e.TarjetacredId).HasName("PK__TARJETA___09F7CCDF1BCA52B6");

            entity.ToTable("TARJETA_CREDITO");

            entity.Property(e => e.TarjetacredId)
                .ValueGeneratedNever()
                .HasColumnName("tarjetacred_id");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.IndustriaId).HasColumnName("industria_id");
            entity.Property(e => e.TarjetacredDescripcion)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("tarjetacred_descripcion");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Industria).WithMany(p => p.TarjetaCreditos)
                .HasForeignKey(d => d.IndustriaId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_tarjeta_indus");
        });

        modelBuilder.Entity<TipoMovimiento>(entity =>
        {
            entity.HasKey(e => e.TipomovId).HasName("PK__TIPO_MOV__C15805169C7BC53A");

            entity.ToTable("TIPO_MOVIMIENTO");

            entity.Property(e => e.TipomovId)
                .ValueGeneratedNever()
                .HasColumnName("tipomov_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.TipomovDescrip)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("tipomov_descrip");
            entity.Property(e => e.TipomovIngEgr).HasColumnName("tipomov_ing_egr");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuId).HasName("PK__USUARIO__430A673C0C5B2B0D");

            entity.ToTable("USUARIO");

            entity.Property(e => e.UsuId)
                .ValueGeneratedNever()
                .HasColumnName("usu_id");
            entity.Property(e => e.EmpresaId).HasColumnName("empresa_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");
            entity.Property(e => e.UsuNombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("usu_nombre");
        });

        modelBuilder.Entity<UsuarioPermiso>(entity =>
        {
            entity.HasKey(e => e.PermisoId).HasName("PK__USUARIO___F765898B1A356FD6");

            entity.ToTable("USUARIO_PERMISO");

            entity.Property(e => e.PermisoId)
                .ValueGeneratedNever()
                .HasColumnName("permiso_id");
            entity.Property(e => e.EstadoPermiso).HasColumnName("estado_permiso");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.ModuloId).HasColumnName("modulo_id");
            entity.Property(e => e.OpcionId).HasColumnName("opcion_id");
            entity.Property(e => e.UsuId).HasColumnName("usu_id");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Opcion).WithMany(p => p.UsuarioPermisos)
                .HasForeignKey(d => d.OpcionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_usu_opcion_opcion");

            entity.HasOne(d => d.Usu).WithMany(p => p.UsuarioPermisos)
                .HasForeignKey(d => d.UsuId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_usu_opcion_usuario");
        });

        modelBuilder.Entity<UsuarioRol>(entity =>
        {
            entity.HasKey(e => e.UsuRolId).HasName("PK__USUARIO___7F1C7DEEAE607658");

            entity.ToTable("USUARIO_ROL");

            entity.Property(e => e.UsuRolId)
                .ValueGeneratedNever()
                .HasColumnName("usu_rol_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaHoraAct)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_act");
            entity.Property(e => e.FechaHoraReg)
                .HasColumnType("datetime")
                .HasColumnName("fecha_hora_reg");
            entity.Property(e => e.RolId).HasColumnName("rol_id");
            entity.Property(e => e.UsuId).HasColumnName("usu_id");
            entity.Property(e => e.UsuIdAct).HasColumnName("usu_id_act");
            entity.Property(e => e.UsuIdReg).HasColumnName("usu_id_reg");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_usuario_rol_rol");

            entity.HasOne(d => d.Usu).WithMany(p => p.UsuarioRols)
                .HasForeignKey(d => d.UsuId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_usuario_rol_usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}