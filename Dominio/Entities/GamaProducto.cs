﻿using System;
using System.Collections.Generic;

namespace Dominio.Entities;

public partial class GamaProducto
{
    public string Gama { get; set; }

    public string DescripcionTexto { get; set; }

    public string DescripcionHtml { get; set; }

    public string Image { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
