using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aplicacion.Repository;
using Dominio.Interfaces;
using Persistencia;

namespace Aplicacion.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly JardineriaContext context;
        private UserRepository _usuarios;
        private RolRepository _roles;
        private ClienteRepository _clientes;
        private DetallePedidoRepository _detallePedidos;
        private EmpleadoRepository _empleados;
        private GamaProductoRepository _gamaProductos;
        private OficinaRepository _oficinas;
        private PagoRepository _pagos;
        private PedidoRepository _pedidos;
        private ProductoRepository _productos;

        public UnitOfWork(JardineriaContext _context)
        {
            context = _context;
        }

        public IUser Users
        {
            get
            {
                if (_usuarios == null)
                {
                    _usuarios = new UserRepository(context);
                }
                return _usuarios;
            }
        }

        public IRol Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RolRepository(context);
                }
                return _roles;
            }
        }

        public ICliente Clientes
        {
            get
            {
                if (_clientes == null)
                {
                    _clientes = new ClienteRepository(context);
                }
                return _clientes;
            }
        }

        public IDetallePedido DetallePedidos
        {
            get
            {
                if (_detallePedidos == null)
                {
                    _detallePedidos = new DetallePedidoRepository(context);
                }
                return _detallePedidos;
            }
        }

        public IEmpleado Empleados
        {
            get
            {
                if (_empleados == null)
                {
                    _empleados = new EmpleadoRepository(context);
                }
                return _empleados;
            }
        }

        public IGamaProducto GamaProductos
        {
            get
            {
                if (_gamaProductos == null)
                {
                    _gamaProductos = new GamaProductoRepository(context);
                }
                return _gamaProductos;
            }
        }

        public IOficina Oficinas
        {
            get
            {
                if (_oficinas == null)
                {
                    _oficinas = new OficinaRepository(context);
                }
                return _oficinas;
            }
        }

        public IPago Pagos
        {
            get
            {
                if (_pagos == null)
                {
                    _pagos = new PagoRepository(context);
                }
                return _pagos;
            }
        }

        public IPedido Pedidos
        {
            get
            {
                if (_pedidos == null)
                {
                    _pedidos = new PedidoRepository(context);
                }
                return _pedidos;
            }
        }

        public IProducto Productos
        {
            get
            {
                if (_productos == null)
                {
                    _productos = new ProductoRepository(context);
                }
                return _productos;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}