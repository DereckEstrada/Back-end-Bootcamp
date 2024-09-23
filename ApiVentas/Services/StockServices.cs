using ApiVentas.DTOs;
using ApiVentas.Interfaces;
using ApiVentas.Models;
using ApiVentas.Utilitarios;
using ApiVentas.Utilitarios.Dictionaries;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace ApiVentas.Services
{
    public class StockServices : IStockServices, IServices<Stock>
    {
        private BaseErpContext _context;
        private ControlError log = new ControlError();
        private DynamicEmpty dynamicEmpty = new DynamicEmpty();
        public StockServices(BaseErpContext context)
        {
            this._context = context;
        }

        public async Task<Respuesta> GetStock(DataQuery dataQuery)
        {
            var result = new Respuesta();
            try
            {

                result.Data = await _context.Stocks
                                            .Include(stock => stock.Empresa)
                                            .Include(stock => stock.Sucursal)
                                            .Include(stock => stock.Bodega)
                                            .Include(stock => stock.Prod)
                                            .Include(stock => stock.Estado)
                                            .Where(StockDictionary.GetExpression(dataQuery))
                                            .Select(stock => new StockDTO
                                            {
                                                StockId = stock.StockId,
                                                EmpresaId = stock.EmpresaId,
                                                EmpresaDescripcion = stock.Empresa.EmpresaNombre,
                                                SucursalId = stock.SucursalId,
                                                SucursalDescripcion = stock.Sucursal.SucursalNombre,
                                                BodegaId = stock.BodegaId,
                                                BodegaDescripcion = stock.Bodega.BodegaNombre,
                                                ProdId = stock.ProdId,
                                                ProdDescripcion = stock.Prod.ProdDescripcion,
                                                CantidadStock = stock.CantidadStock,
                                                EstadoId = stock.EstadoId,
                                                EstadoDescripcion = stock.Estado.EstadoDescrip,
                                                FechaHoraReg = stock.FechaHoraReg,
                                                UsuIdReg = stock.UsuIdReg,
                                                UsuRegName = stock.UsuIdRegNavigation.UsuNombre,
                                            }).ToListAsync();

                result.Code = dynamicEmpty.IsEmpty(result.Data) ? "204" : "200";
                result.Message = dynamicEmpty.IsEmpty(result.Data) ? $"No se encontro registro con opcion:'{dataQuery.OpcionData}' con data: '{dataQuery.DataFirstQuery}'" : "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "GetStock", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> PostStock(Stock stock)
        {
            var result = new Respuesta();
            try
            {
                var query = await _context.Stocks.OrderByDescending(stockDB => stockDB.StockId)
                                                    .Select(idDB => idDB.StockId).FirstOrDefaultAsync() + 1;
                stock.StockId = query;
                stock.FechaHoraReg = DateTime.Now;
                stock.EstadoId = 1;
                await UpdateStocks(stock);                
                _context.Stocks.Add(stock);
                await _context.SaveChangesAsync();

                result.Code = "200";
                result.Message = "Ok";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PostStock", ex.Message);

            }
            return result;
        }

        public async Task<Respuesta> PutStock(Stock stock)
        {
            var result = new Respuesta();
            try
            {
                var existStock = await _context.Stocks.AnyAsync(stockDB => stockDB.StockId == stock.StockId);

                if (existStock)
                {
                    stock.FechaHoraAct = DateTime.Now;

                    _context.Stocks.Update(stock);
                    await _context.SaveChangesAsync();
                    result.Data = stock;
                }
                result.Code = existStock ? "200" : "204";
                result.Message = existStock ? "Ok" : $"No existe registro con id: '{stock.StockId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "PutStock", ex.Message);
            }
            return result;
        }
        public async Task<Respuesta> DeleteStock(Stock stock)
        {
            var result = new Respuesta();
            try
            {
                var existStock = await _context.Stocks.AnyAsync(stockDB => stockDB.StockId == stock.StockId);
                if (existStock)
                {
                    stock.EstadoId = 2;
                    _context.Stocks.Update(stock);
                    await _context.SaveChangesAsync();
                }
                result.Code = existStock ? "200" : "204";
                result.Message = existStock ? "Ok" : $"No existe registro con id: '{stock.StockId}'";
            }
            catch (Exception ex)
            {
                result.Code = "400";
                result.Message = "Se ha presentado un exception por favor comunicarse con sistemas";
                log.LogErrorMetodos(this.GetType().Name, "DeleteStock", ex.Message);
            }
            return result;
        }
        private async Task UpdateStocks(Stock stock)
        {
            var oldStocks = await _context.Stocks.Where(oldStock => oldStock.ProdId == stock.ProdId && oldStock.SucursalId == stock.SucursalId
                            && oldStock.BodegaId == stock.BodegaId && oldStock.EmpresaId == stock.EmpresaId && oldStock.EstadoId == 1).ToListAsync();
            foreach (var item in oldStocks)
            {
                item.EstadoId = 2;
                item.FechaHoraAct = DateTime.Now;
                _context.Update(item);
            }
            await _context.SaveChangesAsync();
        }
    }
}
