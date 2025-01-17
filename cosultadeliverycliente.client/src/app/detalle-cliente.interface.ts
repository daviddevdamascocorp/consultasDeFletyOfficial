export interface DetalleCliente {
    numFactura:         string;
    codCliente:         string;
    nomCliente:         string;
    articulos:          Articulo[];
    status:             string;
    sucursal:           string;
    numeroTelefono:     string;
    fechaFactura:       Date;
    fechaActualizacion: Date;
    direccion:          string;
}

export interface Articulo {
    codArticulo:      string;
    descripcion:      string;
    cantidadArticulo: number;
}
