import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { DamascoTiendas } from '../tiendas.interface';
import { ClientesFlety } from '../clientes.interface';

@Injectable({
  providedIn: 'root'
})
export class PendienteServiceService {
  private baseUrl = environment.baseUrl

    constructor(private httpClient:HttpClient) { }

    
      getTiendasPendientesDamasco(){
        return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/pendiente`)
      }

      getClientesPendientesTienda(  store?: string){
        let params: any = {};
        if (store) params.store = store;
        return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/pendiente/portienda`,{params})
      }
       getClientesPendientesTiendaFecha(startDate?: Date, endDate?: Date,  store?: string){
          let params: any = {};
          
          if (startDate) params.startDate = startDate.toISOString();
          if (endDate) params.endDate = endDate.toISOString();
         
          if (store) params.store = store;
          return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/pendiente/statustiendafecha`,{params})
        }

        getClientesPendientesFecha(startDate?: Date, endDate?: Date){
          let params: any = {};
          
          if (startDate) params.startDate = startDate.toISOString();
          if (endDate) params.endDate = endDate.toISOString();
         
         
          return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/pendiente/clientespendientesfecha`,{params})
        }



}
