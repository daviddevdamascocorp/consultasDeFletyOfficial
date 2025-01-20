import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { ClientesFlety } from './clientes.interface';
import { DetalleCliente } from './detalle-cliente.interface';
import { Observable } from 'rxjs';
import { StatusActualizacion } from './actualizar-estatus.interface';
import { DamascoTiendas } from './tiendas.interface';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  private baseUrl = environment.baseUrl
  constructor(private httpClient:HttpClient) { }

  getClientesFlety(){
    return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/consultaflety`)
  }
  getClienteDetallado(factura:String, sucursal:string){
    return this.httpClient.get<DetalleCliente>(`${this.baseUrl}/api/consultaflety/getfactura/${factura}/sucursal/${sucursal}`)
  }

  updateStatus(bodyStatus:any,factura:string):Observable<any>{
    return this.httpClient.patch<any>(`${this.baseUrl}/api/consultaflety/factura/${factura}`,bodyStatus)
  }

  getTiendasDamasco(){
    return this.httpClient.get<DamascoTiendas[]>(`${this.baseUrl}/api/consultaflety/tiendas`)
  }
  getClientesTiendaFecha(startDate?: Date, endDate?: Date,  store?: string){
    let params: any = {};
    
    if (startDate) params.startDate = startDate.toISOString();
    if (endDate) params.endDate = endDate.toISOString();
   
    if (store) params.store = store;
    return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/consultaflety/tiendafecha`,{params})
  }
  getClientesStatusFecha(startDate?: Date, endDate?: Date, status?: string){
    let params: any = {};
    
    if (startDate) params.startDate = startDate.toISOString();
    if (endDate) params.endDate = endDate.toISOString();
    if (status) params.status = status;
   
    return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/consultaflety/statusfecha`,{params})
  }

  getClientesFecha(startDate?: Date, endDate?: Date){
    let params: any = {};
    
    if (startDate) params.startDate = startDate.toISOString();
    if (endDate) params.endDate = endDate.toISOString();
 
    return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/consultaflety/clientesfecha`,{params})
  }

  getClientesStatusTiendaFecha(startDate?: Date, endDate?: Date, status?: string, store?: string){
    let params: any = {};
    
    if (startDate) params.startDate = startDate.toISOString();
    if (endDate) params.endDate = endDate.toISOString();
    if (status) params.status = status;
    if (store) params.store = store;
    return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/consultaflety/statustiendafecha`,{params})
  }
}
