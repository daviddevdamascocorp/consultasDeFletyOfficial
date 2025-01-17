import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { ClientesFlety } from './clientes.interface';
import { DetalleCliente } from './detalle-cliente.interface';
import { Observable } from 'rxjs';
import { StatusActualizacion } from './actualizar-estatus.interface';

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
}
